using System;
using Tiger.Permissions;
using Tiger.Business.Purchases.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using System.Threading.Tasks;
using System.Linq;
using Tiger.Domain.CoreModule.Utilities;

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
        private readonly IPurchaseReturnDetailRepository _purchaseReturnDetailRepository;


        public PurchaseReturnHeaderAppService(
            IPurchaseReturnHeaderRepository repository,
            IPurchaseReturnDetailRepository purchaseReturnDetailRepository
            ) : base(repository)
        {
            _repository = repository;
            _purchaseReturnDetailRepository = purchaseReturnDetailRepository;
        }

        public override async Task<PurchaseReturnHeaderDto> CreateAsync(CreateUpdatePurchaseReturnHeaderDto input)
        {
            input.Code = Utility.CreateOrderID("CGRK");
            foreach (var item in input.PurchaseReturnDetails)
            {
                item.Id = GuidGenerator.Create();
            }

            input.TotalQty = input.PurchaseReturnDetails.Sum(x => x.TotalQty);
            //input.TotalWeight = input.PurchaseReturnDetails.Sum(x => x.TotalQty);
            return await base.CreateAsync(input);
        }

        public override async Task<PurchaseReturnHeaderDto> UpdateAsync(Guid id, CreateUpdatePurchaseReturnHeaderDto input)
        {
            await DeleteDetail(id);
            foreach (var item in input.PurchaseReturnDetails)
            {
                item.Id = GuidGenerator.Create();
            }
            return await base.UpdateAsync(id, input);
        }


        public override async Task DeleteAsync(Guid id)
        {
            await DeleteDetail(id);
            await base.DeleteAsync(id);
        }

        private async Task DeleteDetail(Guid id)
        {
            var PurchaseReturnHeader = await _repository.GetAsync(id);
            foreach (var receiptDetail in PurchaseReturnHeader.PurchaseReturnDetails)
            {
                await _purchaseReturnDetailRepository.DeleteAsync(receiptDetail.Id, true);
            }
        }

        public override async Task<PurchaseReturnHeaderDto> GetAsync(Guid id)
        {
            var query = await _repository.GetAsync(id);
            return ObjectMapper.Map<PurchaseReturnHeader, PurchaseReturnHeaderDto>(query);
        }
    }
}
