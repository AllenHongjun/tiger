using System;
using Tiger.Permissions;
using Tiger.Business.Purchases.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tiger.Business.Purchases
{
    public class PurchaseDetailAppService : CrudAppService<PurchaseDetail, PurchaseDetailDto, Guid, PagedAndSortedResultRequestDto, CreateUpdatePurchaseDetailDto, CreateUpdatePurchaseDetailDto>,
        IPurchaseDetailAppService
    {
        protected override string GetPolicyName { get; set; } = TigerPermissions.PurchaseDetail.Default;
        protected override string GetListPolicyName { get; set; } = TigerPermissions.PurchaseDetail.Default;
        protected override string CreatePolicyName { get; set; } = TigerPermissions.PurchaseDetail.Create;
        protected override string UpdatePolicyName { get; set; } = TigerPermissions.PurchaseDetail.Update;
        protected override string DeletePolicyName { get; set; } = TigerPermissions.PurchaseDetail.Delete;

        private readonly IPurchaseDetailRepository _repository;
        
        public PurchaseDetailAppService(IPurchaseDetailRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
