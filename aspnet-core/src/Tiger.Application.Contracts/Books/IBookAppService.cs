using System;
using System.Collections.Generic;
using System.Text;
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
    }
}
