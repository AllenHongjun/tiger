
using log4net;
using log4net.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Qiniu.Http;
using Qiniu.Storage;
using Qiniu.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Tiger.BackgroundJob;
using Tiger.Business.Demo;
using Tiger.Domain.Log;
using Tiger.Permissions;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Caching;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Emailing;
using Volo.Abp.Features;
using Volo.Abp.Settings;

namespace Tiger.Books
{
    /// <summary>
    /// 书籍管理
    /// </summary>
    /// 
    [RemoteService(true)]
    [ApiExplorerSettings(GroupName = "admin")]  
    // 增加授权
    //[Authorize(BookStorePermissions.Books.Default)]
    public class BookAppService :
        CrudAppService<
            Book, //The Book entity
            BookDto, //Used to show books
            Guid, //Primary key of the book entity
            PagedAndSortedResultRequestDto, //Used for paging/sorting
            CreateUpdateBookDto>, //Used to create/update a book
        IBookAppService //implement the IBookAppService
    {
        private readonly IAuthorRepository _authorRepository;
        // 自定义的书籍仓库可以在任意APP层的服务注入和使用
        private readonly IRepository<Book, Guid> _repository;
        private readonly IBookRepository _bookRepository;


        private readonly IDistributedCache<BookCacheItem> _cache;
        private readonly ILog _logger = LogManager.GetLogger(typeof(BookAppService));
        private readonly IBackgroundJobManager _backgroundJobManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;
        private readonly IEmailSender _emailSender;
        private readonly ISettingEncryptionService _settingEncryptionService;
        private readonly IFeatureChecker _featureChecker;


        //BookAppService注入IRepository <Book,Guid>,这是Book实体的默认仓储. ABP自动为每个聚合根(或实体)创建默认仓储. 
        public BookAppService(
            IAuthorRepository authorRepository,
            IRepository<Book, Guid> repository,
            IBookRepository bookRepository,

            IDistributedCache<BookCacheItem> cache,
            IBackgroundJobManager backgroundJobManager,
            IWebHostEnvironment webHostEnvironment,
            IConfiguration configuration,
            IEmailSender emailSender,
            ISettingEncryptionService settingEncryptionService
,
            IFeatureChecker featureChecker
        ) : base(repository)
        {
            #region 授权-在构造函数中使用
            //授权 在构造函数中时候 或者使用特性 声明式的授权  基类中的 `CrudAppService` 自动在CRUD操作中使用这些权限. 
            GetPolicyName = TigerPermissions.Books.Default;
            GetListPolicyName = TigerPermissions.Books.Default;
            CreatePolicyName = TigerPermissions.Books.Create;
            UpdatePolicyName = TigerPermissions.Books.Edit;
            DeletePolicyName = TigerPermissions.Books.Delete;
            #endregion

            _authorRepository = authorRepository;
            _repository = repository;
            _bookRepository=bookRepository;

            _cache = cache;
            _backgroundJobManager = backgroundJobManager;
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
            _emailSender = emailSender;
            _settingEncryptionService = settingEncryptionService;
            _featureChecker=featureChecker;
        }

        #region 书籍模块crud基础业务
        /// <summary>
        /// 查询一条书籍数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <remarks>
        /// 重写父类的 GetAsync方法
        /// </remarks>
        public override async Task<BookDto> GetAsync(Guid id)
        {
            await CheckGetPolicyAsync();

            //Prepare a query to join books and authors
            var query = from book in Repository
                        join author in _authorRepository on book.AuthorId equals author.Id
                        where book.Id == id
                        select new { book, author };

            //Execute the query and get the book with author
            // 使用 `AsyncExecuter.FirstOrDefaultAsync(...)` 执行查询并得到一个结果. 这是一种无需依赖database provider API, 使用异步LINQ扩展的方法. 
            var queryResult = await AsyncExecuter.FirstOrDefaultAsync(query);
            if (queryResult == null)
            {
                throw new EntityNotFoundException(typeof(Book), id);
            }

            var bookDto = ObjectMapper.Map<Book, BookDto>(queryResult.book);
            bookDto.AuthorName = queryResult.author.Name;
            return bookDto;
        }

        /// <summary>
        /// 分页查询书籍的数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public override async Task<PagedResultDto<BookDto>>
            GetListAsync(PagedAndSortedResultRequestDto input)
        {
            await CheckGetListPolicyAsync();

            //Prepare a query to join books and authors
            var query = from book in Repository
                        join author in _authorRepository on book.AuthorId equals author.Id
                        orderby input.Sorting
                        select new { book, author };

            query = query
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount);

            //Execute the query and get a list
            var queryResult = await AsyncExecuter.ToListAsync(query);

            //Convert the query result to a list of BookDto objects
            var bookDtos = queryResult.Select(x =>
            {
                var bookDto = ObjectMapper.Map<Book, BookDto>(x.book);
                bookDto.AuthorName = x.author.Name;
                return bookDto;
            }).ToList();

            //Get the total count with another query
            var totalCount = await Repository.GetCountAsync();

            return new PagedResultDto<BookDto>(
                totalCount,
                bookDtos
            );
        }

        /// <summary>
        /// 获取作者筛选列表
        /// </summary>
        /// <returns></returns>
        public async Task<ListResultDto<AuthorLookupDto>> GetAuthorLookupAsync()
        {
            var authors = await _authorRepository.GetListAsync();

            // 可以map一个list的数据
            return new ListResultDto<AuthorLookupDto>(
                ObjectMapper.Map<List<Author>, List<AuthorLookupDto>>(authors)
            );
        }

        private static string NormalizeSorting(string sorting)
        {
            if (sorting.IsNullOrEmpty())
            {
                return $"book.{nameof(Book.Name)}";
            }

            if (sorting.Contains("authorName", StringComparison.OrdinalIgnoreCase))
            {
                return sorting.Replace(
                    "authorName",
                    "author.Name",
                    StringComparison.OrdinalIgnoreCase
                );
            }

            return $"book.{sorting}";
        }


        /// <summary>
        /// 根据类型删除书籍
        /// </summary>
        /// <param name="bookType"></param>
        /// <returns></returns>
        public async Task DeleteBooksByType(BookType bookType)
        {   
            // 可以聚合多个数据库操作
            await _bookRepository.DeleteBooksByType(bookType);
        }


        public  void TestAsync()
        {
            //var dbContext = await _authorRepository.GetDbContextAsync();
            //var dbSet = await _authorRepository.GetDbSetAsync();
            //var dbSet = dbContext.Set<Order>(); //Alternative, when you have the DbContext
            return ;
        }


        #endregion

        #region 日志 缓存使用demo

        public void Set(Guid bookId)
        {



            #region 使用 log4net来写日志
            _logger.Info("NETCorelog4net log");
            _logger.Info("test log");
            _logger.Error("error");
            _logger.Info("linezero");
            //LoggerHelper.WriteToFile("测试log4net");
            _logger.Info("Info使用住的对象来写日志");
            _logger.Debug("Debug使用住的对象来写日志");
            #endregion


            #region 使用dot.net core 自带的方法来写日志
            //IDistributedCache 默认已经实现了很多缓存处理的方法 保存 取消基本够使用。
            // Task<Book>  怎么转换为 Task<BookCacheItem>
            //Logger.LogInformation("测试微软自带的日志功能");

            //_logger.LogTrace("trace");
            //_logger.LogDebug("debug");
            //_logger.LogInformation("info");
            //_logger.LogWarning("warn");
            //_logger.LogError("error");
            //_logger.LogCritical("critical");
            //using (_logger.BeginScope("测试日志的Scope"))
            //{
            //    _logger.LogTrace("scope-trace");
            //    _logger.LogDebug("scope-debug");
            //    _logger.LogInformation("scope-info");
            //    _logger.LogWarning("scope-warn");
            //    _logger.LogError("scope-error");
            //    _logger.LogCritical("scope-critical");
            //}
            //_logger.LogTrace("Scope已经结束了。。。");


            //_logger.LogInformation("LogInformation:11111" );
            //_logger.LogInformation("LogInformation:" + DateTime.Now.ToString());
            //_logger.LogWarning("LogWarning:" + DateTime.Now.ToString());
            //_logger.LogError("LogError:" + DateTime.Now.ToString());
            //_logger.LogDebug("LogDebug:" + DateTime.Now.ToString()); 
            #endregion

            #region 测试用redis缓存的功能
            _cache.Set("test1", new BookCacheItem() { Name = "名字1", Price = 10.9f });
            //return await _cache.GetOrAdd(
            //    bookId.ToString(), //Cache key
            //    async () => await GetBookFromDatabaseAsync(bookId),
            //    () => new DistributedCacheEntryOptions
            //    {
            //        AbsoluteExpiration = DateTimeOffset.Now.AddHours(1)
            //    }
            //);
            #endregion
        }
        #endregion

        #region 电子邮件测试
        //private Task<BookCacheItem> GetBookFromDatabaseAsync(Guid bookId)
        //{
        //    return _repository.GetAsync(bookId);
        //    //TODO: get from database
        //}



        public async Task RegisterAsync(string userName, string emailAddress, string password)
        {
            //TODO: 创建一个新用户到数据库中...

            await _backgroundJobManager.EnqueueAsync(
                new EmailSendingArgs
                {
                    EmailAddress = emailAddress,
                    Subject = "You've successfully registered!",
                    Body = "..."
                }
            );
        }


        /*
        Provides IEmailSender service that is used to send emails.
       Defines settings to configure email sending.
       Integrates to the background job system to send emails via background jobs.
       Provides MailKit integration package.
       1. 定义发送邮件的同一接口
       2. 集成后台定时发送消息的任务
       3. 集成第三方发送邮件的服务

        */
        public async Task DoItAsync()
        {
            await _emailSender.SendAsync(
               "652971723@163.com",     // target email address
               "Email subject",         // subject
               "This is email body..."  // email body
           );

            //Task.CompletedTask;
        }

        public JsonResult GetEncryptionSecret()
        {
            SettingDefinition rd = new SettingDefinition("Settings:Abp.Mailing.Smtp.Password", "123456");
            var res = _settingEncryptionService.Encrypt(rd, _configuration.GetValue<string>("Settings:Abp.Mailing.Smtp.Password"));

            return new JsonResult(res);
        } 
        #endregion

        #region 文件上传
        /// <summary>
        /// 上传 文件,并返回相对url(不包含 host port wwwroot)
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [Route("upload-file")]
        [HttpPost]
        public async Task<string> Upload(IFormFile file)
        {
            // 网站根目录
            string content_path = _webHostEnvironment.ContentRootPath;
            // wwwroot 静态资源根目录
            string web_path = _webHostEnvironment.WebRootPath;


            string webRootPath = web_path; // wwwroot 文件夹
            string uploadPath = Path.Combine("uploads", DateTime.Now.ToString("yyyyMMdd"));
            string dirPath = Path.Combine(webRootPath, uploadPath);
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }
            string fileExt = Path.GetExtension(file.FileName).Trim('.'); //文件扩展名，不含“.”
            string newFileName = Guid.NewGuid().ToString().Replace("-", "") + "." + fileExt; //随机生成新的文件名
            var filePath = Path.Combine(dirPath, newFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            var url = $@"\{uploadPath}\{newFileName}";

            return url;
        }


        /// <summary>
        ///  多文件上传
        /// </summary>
        /// <param name="formCollection">表单集合值</param>
        /// <returns>服务器存储的文件信息</returns>
        /// <summary>
        /// 上传 文件,并返回相对url(不包含 host port wwwroot)
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        [Route("upload-files")]
        [HttpPost]
        public JsonResult MultiFileUpload(IFormFileCollection files)
        {
            var currentDate = DateTime.Now;
            var webRootPath = _webHostEnvironment.WebRootPath;//>>>相当于HttpContext.Current.Server.MapPath("") 
            var uploadFileRequestList = new List<UploadFileRequest>();
            try
            {
                //FormCollection转化为FormFileCollection
                //var files = (FormFileCollection)formCollection.Files;

                if (files.Any())
                {
                    foreach (var file in files)
                    {
                        var uploadFileRequest = new UploadFileRequest();

                        var filePath = $"/UploadFile/{currentDate:yyyyMMdd}/";

                        //创建每日存储文件夹
                        if (!Directory.Exists(webRootPath + filePath))
                        {
                            Directory.CreateDirectory(webRootPath + filePath);
                        }

                        //文件后缀
                        var fileExtension = Path.GetExtension(file.FileName);//获取文件格式，拓展名

                        //判断文件大小
                        var fileSize = file.Length;

                        if (fileSize > 1024 * 1024 * 10) //10M TODO:(1mb=1024X1024b)
                        {
                            continue;
                        }

                        //保存的文件名称(以名称和保存时间命名)
                        var saveName = file.FileName.Substring(0, file.FileName.LastIndexOf('.')) + "_" + currentDate.ToString("HHmmss") + fileExtension;

                        //文件保存
                        using (var fs = System.IO.File.Create(webRootPath + filePath + saveName))
                        {
                            file.CopyTo(fs);
                            fs.Flush();
                        }

                        //完整的文件路径
                        var completeFilePath = Path.Combine(filePath, saveName);

                        uploadFileRequestList.Add(new UploadFileRequest()
                        {
                            FileName = saveName,
                            FilePath = completeFilePath
                        });
                    }
                }
                else
                {
                    return new JsonResult(new { isSuccess = false, resultMsg = "上传失败，未检测上传的文件信息~" });
                }
            }
            catch (Exception ex)
            {
                return new JsonResult(new { isSuccess = false, resultMsg = "文件保存失败，异常信息为：" + ex.Message });
            }

            if (uploadFileRequestList.Any())
            {
                return new JsonResult(new { isSuccess = true, returnMsg = "上传成功", filePathArray = uploadFileRequestList });
            }
            else
            {
                return new JsonResult(new { isSuccess = false, resultMsg = "网络打瞌睡了，文件保存失败" });
            }
        }



        /// <summary>
        /// 对文件上传响应模型
        /// </summary>
        public class UploadFileRequest
        {
            /// <summary>
            /// 文件名称
            /// </summary>
            public string FileName { get; set; }

            /// <summary>
            /// 文件路径
            /// </summary>
            public string FilePath { get; set; }
        }
        #endregion

        #region 七牛文件上传
        /// <summary>
        /// 七牛单文件上传
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [Route("upload-file-qn")]
        [HttpPost]
        public List<Object> UploadQiniu(IFormFile file)
        {

            var configurationSection = _configuration.GetSection("Qiniu");
            //_configuration.GetValue("");

            Mac mac = new Mac(configurationSection["AccessKey"], configurationSection["SecretKey"]);// AK SK使用
            PutPolicy putPolicy = new PutPolicy();
            putPolicy.Scope = configurationSection["Bucket"];
            string token = Auth.CreateUploadToken(mac, putPolicy.ToJsonString());//token生成
            Config config = new Config()
            {
                Zone = Zone.ZONE_CN_South,
                UseHttps = true
            };

            FormUploader upload = new FormUploader(config);
            HttpResult result = new HttpResult();
            List<Object> list = new List<Object>();
            if (file.Length > 0)
            {
                var _fileName = ContentDispositionHeaderValue
                                .Parse(file.ContentDisposition)
                                .FileName
                                .Trim('"');
                var _qiniuName = "qiniu" + "/" + DateTime.Now.ToString("yyyyMMddHHmmssffffff") + _fileName;//重命名文件加上时间戳
                Stream stream = file.OpenReadStream();
                result = upload.UploadStream(stream, _qiniuName, token, null);
                if (result.Code == 200)
                {
                    list.Add(new { fileName = _fileName, qiniuName = _qiniuName, uploadTime = DateTime.Now, Remark = "" });
                }
                else
                {
                    throw new Exception(result.RefText);//上传失败错误信息
                }
            }
            return list;
        }


        /// <summary>
        /// 七牛多文件上传
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        [Route("upload-files-qn")]
        [HttpPost]
        public List<Object> UploadQiniu(IList<IFormFile> files)
        {

            var configurationSection = _configuration.GetSection("Qiniu");
            //_configuration.GetValue("");

            Mac mac = new Mac(configurationSection["AccessKey"], configurationSection["SecretKey"]);// AK SK使用
            PutPolicy putPolicy = new PutPolicy();
            putPolicy.Scope = configurationSection["Bucket"];
            string token = Auth.CreateUploadToken(mac, putPolicy.ToJsonString());//token生成
            Config config = new Config()
            {
                Zone = Zone.ZONE_CN_East,
                UseHttps = true
            };

            FormUploader upload = new FormUploader(config);
            HttpResult result = new HttpResult();
            List<Object> list = new List<Object>();
            foreach (IFormFile file in files)//获取多个文件列表集合
            {
                if (file.Length > 0)
                {
                    var _fileName = ContentDispositionHeaderValue
                                    .Parse(file.ContentDisposition)
                                    .FileName
                                    .Trim('"');
                    var _qiniuName = "qiniu" + "/" + DateTime.Now.ToString("yyyyMMddHHmmssffffff") + _fileName;//重命名文件加上时间戳
                    Stream stream = file.OpenReadStream();
                    result = upload.UploadStream(stream, _qiniuName, token, null);
                    if (result.Code == 200)
                    {
                        list.Add(new { fileName = _fileName, qiniuName = _qiniuName, uploadTime = DateTime.Now, Remark = "" });
                    }
                    else
                    {
                        throw new Exception(result.RefText);//上传失败错误信息
                    }
                }
            }
            return list;
        }
        #endregion


        public async Task<object> GetPdfReportAsync()
        {
            if (await _featureChecker.IsEnabledAsync("MyApp.PdfReporting"))
            {
                return new
                {
                    code = 100,
                };
            }
            else
            {
                return new
                {
                    code = -100,
                };
            }
        }
    }
}
