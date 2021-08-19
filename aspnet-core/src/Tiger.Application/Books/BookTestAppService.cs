using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Tiger.Books
{   
    /// <summary>
    /// 测试基础crud demo
    /// </summary>
    [ApiExplorerSettings(GroupName = "admin")]
    public class BookTestAppService :
        CrudAppService<
            Book, //The Book entity
            BookDto, //Used to show books
            Guid, //Primary key of the book entity
            PagedAndSortedResultRequestDto, //Used for paging/sorting
            CreateUpdateBookDto>, //Used to create/update a book
        IBookTestAppService //implement the IBookAppService
    {
        public BookTestAppService(IRepository<Book, Guid> repository)
            : base(repository)
        {

        }

        //public Task<ListResultDto<AuthorLookupDto>> GetAuthorLookupAsync()
        //{
        //    throw new NotImplementedException();
        //}

        //public void Set(Guid bookId)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
