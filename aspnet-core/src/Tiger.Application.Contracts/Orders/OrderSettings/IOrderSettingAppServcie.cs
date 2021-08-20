using System;
using System.Collections.Generic;
using System.Text;
using Tiger.Books;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tiger.Orders.OrderSettings
{
    public interface IOrderSettingAppServcie:
        ICrudAppService<
            OrderSettingDto, // Used to show books
            Guid,
            PagedAndSortedResultRequestDto,
            CreateUpdateBookDto> // Used to create/update a book
    {
    }
}
