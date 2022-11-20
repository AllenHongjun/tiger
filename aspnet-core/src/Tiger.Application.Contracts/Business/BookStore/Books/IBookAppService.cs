using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tiger.Books
{
    public interface IBookAppService:
        ICrudAppService<
            BookDto, // Used to show books
            Guid,
            PagedAndSortedResultRequestDto,
            CreateUpdateBookDto> // Used to create/update a book
    {

        void Set(Guid bookId);


        // ADD the NEW METHOD
        /// <summary>
        /// 获取下拉框选中作者的列表数据
        /// </summary>
        /// <returns></returns>
        Task<ListResultDto<AuthorLookupDto>> GetAuthorLookupAsync();
    }
}
