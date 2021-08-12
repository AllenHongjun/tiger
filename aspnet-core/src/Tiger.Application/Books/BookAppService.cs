
using log4net;
using log4net.Repository;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace Tiger.Books
{
    /// <summary>
    /// 书籍管理
    /// </summary>
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
        //BookAppService注入IRepository <Book,Guid>,这是Book实体的默认仓储. ABP自动为每个聚合根(或实体)创建默认仓储. 
        public BookAppService(IRepository<Book, Guid> repository, 
            IDistributedCache<BookCacheItem> cache, 
            IBackgroundJobManager backgroundJobManager
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
        }

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
    }
}
