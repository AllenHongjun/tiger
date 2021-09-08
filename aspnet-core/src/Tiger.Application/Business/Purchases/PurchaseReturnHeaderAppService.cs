using System;
using Tiger.Permissions;
using Tiger.Business.Purchases.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tiger.Business.Purchases
{
    public class PurchaseReturnHeaderAppService : CrudAppService<PurchaseReturnHeader, PurchaseReturnHeaderDto, Guid, PagedAndSortedResultRequestDto, CreateUpdatePurchaseReturnHeaderDto, CreateUpdatePurchaseReturnHeaderDto>,
        IPurchaseReturnHeaderAppService
    {
        protected override string GetPolicyName { get; set; } = TigerPermissions.PurchaseReturnHeader.Default;
        protected override string GetListPolicyName { get; set; } = TigerPermissions.PurchaseReturnHeader.Default;
        protected override string CreatePolicyName { get; set; } = TigerPermissions.PurchaseReturnHeader.Create;
        protected override string UpdatePolicyName { get; set; } = TigerPermissions.PurchaseReturnHeader.Update;
        protected override string DeletePolicyName { get; set; } = TigerPermissions.PurchaseReturnHeader.Delete;

        private readonly IPurchaseReturnHeaderRepository _repository;
        
        public PurchaseReturnHeaderAppService(IPurchaseReturnHeaderRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
