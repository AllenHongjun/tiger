/**
 * 名    称：IPersonRepository   
 * 作    者：花生了什么树       
 * 创建时间：2021/8/16 12:56:36       
 * 说    明: 
 * 
 */


using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Tiger.Business.Demo
{
    /// <summary>
    /// 自定义仓储示例  自定义仓储接口
    ///ABP不会强制你实现任何接口或从存储库的任何基类继承. 它可以只是一个简单的POCO类. 但是建议继承现有的仓储接口和类, 获得开箱即用的标准方法使你的工作更轻松.
    ///
    /// IRepository 继承自 IQueryable,这意味着你可以直接使用LINQ扩展方法. 如上面的泛型仓储示例.
    /// 
    /// 这里异步方法不是标准LINQ方法,它们定义在Microsoft.EntityFrameworkCoreNuget包中.
    ///标准模板应用层与领域层不引用EF Core 包以实现数据库提供程序独立.
    /// 
    /// 强烈建议使用异步方法! 在执行数据库查询时不要使用同步LINQ方法,以便能够开发可伸缩的应用程序.
    /// 
    /// tolistasync 一步的EF查询方法 有些泛型里面没有的时候 建议写在 在自定义的仓储里面。
    /// </summary>
    public interface IPersonRepository : IRepository<Person, Guid>
    {
        Task<Person> FindByNameAsync(string name);
    }

}
