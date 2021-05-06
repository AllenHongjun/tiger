using System;
using System.Collections.Generic;
using System.Text;
using TigerAdmin.Permissions;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace TigerAdmin.Books
{
    public class BookAppService:
        CrudAppService<
            Book, //The Book entity
            BookDto, //Used to show books
            Guid, //Primary key of the book entity
            PagedAndSortedResultRequestDto, //Used for paging/sorting
            CreateUpdateBookDto>, //Used to create/update a book
        IBookAppService //implement the IBookAppService
    {
        public BookAppService(IRepository<Book, Guid> repository) : base(repository)
        {
            //使用权限
            GetPolicyName = TigerAdminPermissions.Books.Default;
            GetListPolicyName = TigerAdminPermissions.Books.Default;
            CreatePolicyName = TigerAdminPermissions.Books.Create;
            UpdatePolicyName = TigerAdminPermissions.Books.Edit;
            DeletePolicyName = TigerAdminPermissions.Books.Delete;
        }
    }
}
