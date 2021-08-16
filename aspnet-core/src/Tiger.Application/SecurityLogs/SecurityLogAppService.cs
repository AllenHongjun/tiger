using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.SecurityLog;

namespace Tiger.SecurityLogs
{
    //public class SecurityLogAppService : CrudAppService<
    //        SecurityLogInfo, //The Book entity
    //        SecurityLogDto, //Used to show books
    //        Guid, //Primary key of the book entity
    //        PagedAndSortedResultRequestDto //Used for paging/sorting
    //        >, //Used to create/update a book
    //    ISecurityLogAppService //implement the IBookAppService
    //{
    //    public SecurityLogAppService(IRepository<SecurityLogInfo, Guid> repository) : base(repository)
    //    {
    //        //使用权限
    //        //GetPolicyName = TigerPermissions.Books.Default;
    //        //GetListPolicyName = TigerPermissions.Books.Default;
    //        //CreatePolicyName = TigerPermissions.Books.Create;
    //        //UpdatePolicyName = TigerPermissions.Books.Edit;
    //        //DeletePolicyName = TigerPermissions.Books.Delete;
    //    }

        
    //}
}
