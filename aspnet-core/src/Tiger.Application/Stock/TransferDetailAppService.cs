using System;
using Tiger.Permissions;
using Tiger.Stock.Dtos;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tiger.Stock
{
    [RemoteService(false)]
    public class TransferDetailAppService : CrudAppService<TransferDetail, TransferDetailDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateTransferDetailDto, CreateUpdateTransferDetailDto>,
        ITransferDetailAppService
    {
        protected override string GetPolicyName { get; set; } = TigerPermissions.TransferDetail.Default;
        protected override string GetListPolicyName { get; set; } = TigerPermissions.TransferDetail.Default;
        protected override string CreatePolicyName { get; set; } = TigerPermissions.TransferDetail.Create;
        protected override string UpdatePolicyName { get; set; } = TigerPermissions.TransferDetail.Update;
        protected override string DeletePolicyName { get; set; } = TigerPermissions.TransferDetail.Delete;

        private readonly ITransferDetailRepository _repository;
        
        public TransferDetailAppService(ITransferDetailRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
