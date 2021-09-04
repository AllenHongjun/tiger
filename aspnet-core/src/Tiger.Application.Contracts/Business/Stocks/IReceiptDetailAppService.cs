using System;
using Tiger.Business.Stocks.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tiger.Business.Stocks
{
    public interface IReceiptDetailAppService :
        ICrudAppService< 
            ReceiptDetailDto, 
            Guid, 
            PagedAndSortedResultRequestDto,
            CreateUpdateReceiptDetailDto,
            CreateUpdateReceiptDetailDto>
    {

    }
}