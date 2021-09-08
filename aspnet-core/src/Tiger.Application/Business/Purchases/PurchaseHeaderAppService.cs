using System;
using Tiger.Permissions;
using Tiger.Business.Purchases.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tiger.Business.Purchases
{
    public class PurchaseHeaderAppService : CrudAppService<PurchaseHeader, PurchaseHeaderDto, Guid, PagedAndSortedResultRequestDto, CreateUpdatePurchaseHeaderDto, CreateUpdatePurchaseHeaderDto>,
        IPurchaseHeaderAppService
    {
        protected override string GetPolicyName { get; set; } = TigerPermissions.PurchaseHeader.Default;
        protected override string GetListPolicyName { get; set; } = TigerPermissions.PurchaseHeader.Default;
        protected override string CreatePolicyName { get; set; } = TigerPermissions.PurchaseHeader.Create;
        protected override string UpdatePolicyName { get; set; } = TigerPermissions.PurchaseHeader.Update;
        protected override string DeletePolicyName { get; set; } = TigerPermissions.PurchaseHeader.Delete;

        private readonly IPurchaseHeaderRepository _repository;
        
        public PurchaseHeaderAppService(IPurchaseHeaderRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
