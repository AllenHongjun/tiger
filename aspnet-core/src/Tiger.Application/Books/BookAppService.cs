
using log4net;
using log4net.Repository;
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
using Tiger.Domain.Log;
using Tiger.Permissions;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Caching;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Emailing;
using Volo.Abp.Settings;

namespace Tiger.Books
{
    /// <summary>
    /// 书籍管理
    /// </summary>
    /// 
    [ApiExplorerSettings(GroupName = "api")]
    public class BookAppService:
        CrudAppService<
            Book, //The Book entity
            BookDto, //Used to show books
            Guid, //Primary key of the book entity
            PagedAndSortedResultRequestDto, //Used for paging/sorting
            CreateUpdateBookDto>, //Used to create/update a book
        IBookAppService //implement the IBookAppService
    {
        private readonly IDistributedCache<BookCacheItem> _cache;
        
        private readonly ILog _logger = LogManager.GetLogger(typeof(BookAppService));
        private readonly IRepository<Book, Guid> _repository;
        private readonly IBackgroundJobManager _backgroundJobManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;
        private readonly IEmailSender _emailSender;
        private readonly ISettingEncryptionService _settingEncryptionService;


        //BookAppService注入IRepository <Book,Guid>,这是Book实体的默认仓储. ABP自动为每个聚合根(或实体)创建默认仓储. 
        public BookAppService(IRepository<Book, Guid> repository, 
            IDistributedCache<BookCacheItem> cache, 
            IBackgroundJobManager backgroundJobManager,
            IWebHostEnvironment webHostEnvironment,
            IConfiguration configuration,
            IEmailSender emailSender,
            ISettingEncryptionService settingEncryptionService
        ) : base(repository)
        {
            //使用权限
            GetPolicyName = TigerPermissions.Books.Default;
            GetListPolicyName = TigerPermissions.Books.Default;
            CreatePolicyName = TigerPermissions.Books.Create;
            UpdatePolicyName = TigerPermissions.Books.Edit;
            DeletePolicyName = TigerPermissions.Books.Delete;


            _repository = repository;
            _cache = cache;
            _backgroundJobManager = backgroundJobManager;
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
            _emailSender = emailSender;
            _settingEncryptionService = settingEncryptionService;
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
        /// <param name="file"></param>
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
    }
}
