/**
 * 类    名：MyBookAppService   
 * 作    者：花生了什么树       
 * 创建时间：2021/8/17 0:35:42       
 * 说    明: 
 * 
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp;

namespace Tiger.Books.Demo
{   

    /// <summary>
    /// 注入 IDataFilter 服务到你的类中.
    ///在 using 语句中使用 Disable 方法创建一个代码块, 其中禁用了 ISoftDelete 过滤器(始终与 using 搭配使用,确保代码块执行后将过滤重置为之前的状态).
    ///IDataFilter.Enable 方法可以启用过滤.可以嵌套使用 Enable 和 Disable 方法定义内部作用域.
    ///
    /// 
    /// 
    /// 
    /// 
    /// </summary>
    public class MyBookService : ITransientDependency
    {
        private readonly IDataFilter _dataFilter;
        private readonly IRepository<Book, Guid> _bookRepository;

        public MyBookService(
            IDataFilter dataFilter,
            IRepository<Book, Guid> bookRepository)
        {
            _dataFilter = dataFilter;
            _bookRepository = bookRepository;
        }

        public async Task<List<Book>> GetAllBooksIncludingDeletedAsync()
        {
            //Temporary disable the ISoftDelete filter
            using (_dataFilter.Disable<ISoftDelete>())
            {
                return await _bookRepository.GetListAsync();
            }
        }
    }
}
