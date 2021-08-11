
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TigerAdmin.Permissions;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Caching;
using Volo.Abp.Domain.Repositories;

namespace TigerAdmin.Books
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
        private readonly ILogger<BookAppService> _logger;
        private readonly IRepository<Book, Guid> _repository;
        //BookAppService注入IRepository <Book,Guid>,这是Book实体的默认仓储. ABP自动为每个聚合根(或实体)创建默认仓储. 
        public BookAppService(IRepository<Book, Guid> repository, IDistributedCache<BookCacheItem> cache, ILogger<BookAppService> logger) : base(repository)
        {
            //使用权限
            GetPolicyName = TigerAdminPermissions.Books.Default;
            GetListPolicyName = TigerAdminPermissions.Books.Default;
            CreatePolicyName = TigerAdminPermissions.Books.Create;
            UpdatePolicyName = TigerAdminPermissions.Books.Edit;
            DeletePolicyName = TigerAdminPermissions.Books.Delete;
            _repository = repository;

            _cache = cache;
            _logger = logger;
        }

        public void Set(Guid bookId)
        {

            //IDistributedCache 默认已经实现了很多缓存处理的方法 保存 取消基本够使用。
            // Task<Book>  怎么转换为 Task<BookCacheItem>


            _logger.LogTrace("trace");
            _logger.LogDebug("debug");
            _logger.LogInformation("info");
            _logger.LogWarning("warn");
            _logger.LogError("error");
            _logger.LogCritical("critical");
            using (_logger.BeginScope("测试日志的Scope"))
            {
                _logger.LogTrace("scope-trace");
                _logger.LogDebug("scope-debug");
                _logger.LogInformation("scope-info");
                _logger.LogWarning("scope-warn");
                _logger.LogError("scope-error");
                _logger.LogCritical("scope-critical");
            }
            _logger.LogTrace("Scope已经结束了。。。");


            _logger.LogInformation("LogInformation:11111" );
            _logger.LogInformation("LogInformation:" + DateTime.Now.ToString());
            _logger.LogWarning("LogWarning:" + DateTime.Now.ToString());
            _logger.LogError("LogError:" + DateTime.Now.ToString());
            _logger.LogDebug("LogDebug:" + DateTime.Now.ToString());

            _cache.Set("test1", new BookCacheItem() { Name = "名字1",  Price = 10.9f });
            //return await _cache.GetOrAdd(
            //    bookId.ToString(), //Cache key
            //    async () => await GetBookFromDatabaseAsync(bookId),
            //    () => new DistributedCacheEntryOptions
            //    {
            //        AbsoluteExpiration = DateTimeOffset.Now.AddHours(1)
            //    }
            //);
        }

        //private Task<BookCacheItem> GetBookFromDatabaseAsync(Guid bookId)
        //{
        //    return _repository.GetAsync(bookId);
        //    //TODO: get from database
        //}
    }
}
