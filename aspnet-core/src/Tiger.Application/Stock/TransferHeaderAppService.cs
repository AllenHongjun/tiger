using System;
using Tiger.Permissions;
using Tiger.Stock.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tiger.Stock
{
    public class TransferHeaderAppService : CrudAppService<TransferHeader, TransferHeaderDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateTransferHeaderDto, CreateUpdateTransferHeaderDto>,
        ITransferHeaderAppService
    {
        protected override string GetPolicyName { get; set; } = TigerPermissions.TransferHeader.Default;
        protected override string GetListPolicyName { get; set; } = TigerPermissions.TransferHeader.Default;
        protected override string CreatePolicyName { get; set; } = TigerPermissions.TransferHeader.Create;
        protected override string UpdatePolicyName { get; set; } = TigerPermissions.TransferHeader.Update;
        protected override string DeletePolicyName { get; set; } = TigerPermissions.TransferHeader.Delete;

        private readonly ITransferHeaderRepository _repository;
        
        public TransferHeaderAppService(ITransferHeaderRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
