using System;
using Tiger.Permissions;
using Tiger.Business.Purchases.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Tiger.Domain.CoreModule.Utilities;
using System.Threading.Tasks;
using System.Linq;
using Volo.Abp;

namespace Tiger.Business.Purchases
{
    [RemoteService(false)]
    public class PurchaseHeaderAppService : CrudAppService<PurchaseHeader, PurchaseHeaderDto, Guid, PagedAndSortedResultRequestDto, CreateUpdatePurchaseHeaderDto, CreateUpdatePurchaseHeaderDto>,
        IPurchaseHeaderAppService
    {
        //protected override string GetPolicyName { get; set; } = TigerPermissions.PurchaseHeader.Default;
        //protected override string GetListPolicyName { get; set; } = TigerPermissions.PurchaseHeader.Default;
        //protected override string CreatePolicyName { get; set; } = TigerPermissions.PurchaseHeader.Create;
        //protected override string UpdatePolicyName { get; set; } = TigerPermissions.PurchaseHeader.Update;
        //protected override string DeletePolicyName { get; set; } = TigerPermissions.PurchaseHeader.Delete;

        private readonly IPurchaseHeaderRepository _repository;
        private readonly IPurchaseDetailRepository _purchaseDetailRepository;


        public PurchaseHeaderAppService(
            IPurchaseHeaderRepository repository,
            IPurchaseDetailRepository purchaseDetailRepository
            ) : base(repository)
        {
            _repository = repository;
            _purchaseDetailRepository = purchaseDetailRepository;
        }

        public override async Task<PurchaseHeaderDto> CreateAsync(CreateUpdatePurchaseHeaderDto input)
        {
            input.Code = Utility.CreateOrderID("CGRK");
            foreach (var item in input.PurchaseDetails)
            {
                item.Id = GuidGenerator.Create();
            }

            input.TotalQty = input.PurchaseDetails.Sum(x => x.TotalQty);
            //input.TotalWeight = input.PurchaseDetails.Sum(x => x.TotalQty);
            return await base.CreateAsync(input);
        }

        public override async Task<PurchaseHeaderDto> UpdateAsync(Guid id, CreateUpdatePurchaseHeaderDto input)
        {
            await DeleteDetail(id);
            foreach (var item in input.PurchaseDetails)
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
            var PurchaseHeader = await _repository.GetAsync(id);
            foreach (var receiptDetail in PurchaseHeader.PurchaseDetails)
            {
                await _purchaseDetailRepository.DeleteAsync(receiptDetail.Id, true);
            }
        }

        public override async Task<PurchaseHeaderDto> GetAsync(Guid id)
        {
            var query = await _repository.GetAsync(id, false);
            var details = _purchaseDetailRepository.WithDetails(x => x.Product).Where(x => x.PurchaseHeaderId == id);
            foreach (var detail in details)
            {
                query.PurchaseDetails.Add(detail);
            }
            
            return ObjectMapper.Map<PurchaseHeader, PurchaseHeaderDto>(query);
        }
    }
}
