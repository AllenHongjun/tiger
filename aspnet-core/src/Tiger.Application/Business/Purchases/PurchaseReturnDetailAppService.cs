using System;
using Tiger.Permissions;
using Tiger.Business.Purchases.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tiger.Business.Purchases
{
    public class PurchaseReturnDetailAppService : CrudAppService<PurchaseReturnDetail, PurchaseReturnDetailDto, Guid, PagedAndSortedResultRequestDto, CreateUpdatePurchaseReturnDetailDto, CreateUpdatePurchaseReturnDetailDto>,
        IPurchaseReturnDetailAppService
    {
        protected override string GetPolicyName { get; set; } = TigerPermissions.PurchaseReturnDetail.Default;
        protected override string GetListPolicyName { get; set; } = TigerPermissions.PurchaseReturnDetail.Default;
        protected override string CreatePolicyName { get; set; } = TigerPermissions.PurchaseReturnDetail.Create;
        protected override string UpdatePolicyName { get; set; } = TigerPermissions.PurchaseReturnDetail.Update;
        protected override string DeletePolicyName { get; set; } = TigerPermissions.PurchaseReturnDetail.Delete;

        private readonly IPurchaseReturnDetailRepository _repository;
        
        public PurchaseReturnDetailAppService(IPurchaseReturnDetailRepository repository) : base(repository)
        {
            _repository = repository;
        }

        
    }
}
