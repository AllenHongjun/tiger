using System;
using Tiger.Stock.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tiger.Stock
{
    public interface ITransferHeaderAppService :
        ICrudAppService< 
            TransferHeaderDto, 
            Guid, 
            PagedAndSortedResultRequestDto,
            CreateUpdateTransferHeaderDto,
            CreateUpdateTransferHeaderDto>
    {

    }
}