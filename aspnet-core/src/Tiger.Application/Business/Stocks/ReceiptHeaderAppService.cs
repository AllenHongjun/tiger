using System;
using Tiger.Permissions;
using Tiger.Stock.Dtos;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tiger.Stock
{
    [RemoteService(false)]
    public class ReceiptHeaderAppService : CrudAppService<ReceiptHeader, ReceiptHeaderDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateReceiptHeaderDto, CreateUpdateReceiptHeaderDto>,
        IReceiptHeaderAppService
    {
        //protected override string GetPolicyName { get; set; } = TigerPermissions.ReceiptHeader.Default;
        //protected override string GetListPolicyName { get; set; } = TigerPermissions.ReceiptHeader.Default;
        //protected override string CreatePolicyName { get; set; } = TigerPermissions.ReceiptHeader.Create;
        //protected override string UpdatePolicyName { get; set; } = TigerPermissions.ReceiptHeader.Update;
        //protected override string DeletePolicyName { get; set; } = TigerPermissions.ReceiptHeader.Delete;

        private readonly IReceiptHeaderRepository _repository;
        
        public ReceiptHeaderAppService(IReceiptHeaderRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
