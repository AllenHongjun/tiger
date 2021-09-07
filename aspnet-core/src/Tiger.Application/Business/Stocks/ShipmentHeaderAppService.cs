using System;
using Tiger.Permissions;
using Tiger.Business.Stocks.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp;
using System.Threading.Tasks;
using Tiger.Domain.CoreModule.Utilities;

namespace Tiger.Business.Stocks
{   
    /// <summary>
    /// Èë¿âµ¥
    /// </summary>
    [RemoteService(false)]
    public class ShipmentHeaderAppService : CrudAppService<ShipmentHeader, ShipmentHeaderDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateShipmentHeaderDto, CreateUpdateShipmentHeaderDto>,
        IShipmentHeaderAppService
    {
        //protected override string GetPolicyName { get; set; } = TigerPermissions.ShipmentHeader.Default;
        //protected override string GetListPolicyName { get; set; } = TigerPermissions.ShipmentHeader.Default;
        //protected override string CreatePolicyName { get; set; } = TigerPermissions.ShipmentHeader.Create;
        //protected override string UpdatePolicyName { get; set; } = TigerPermissions.ShipmentHeader.Update;
        //protected override string DeletePolicyName { get; set; } = TigerPermissions.ShipmentHeader.Delete;

        private readonly IShipmentHeaderRepository _repository;
        private readonly IShipmentDetailRepository _shipmentDetailRepository;


        public ShipmentHeaderAppService(
            IShipmentHeaderRepository repository,
            IShipmentDetailRepository shipmentDetailRepository
            ) : base(repository)
        {
            _repository = repository;
            _shipmentDetailRepository = shipmentDetailRepository;
        }

        public override async Task<ShipmentHeaderDto> GetAsync(Guid id)
        {
            var query = await _repository.GetAsync(id);
            return ObjectMapper.Map<ShipmentHeader, ShipmentHeaderDto>(query);
        }

        public override async Task<ShipmentHeaderDto> CreateAsync(CreateUpdateShipmentHeaderDto input)
        {
            input.Code = Utility.CreateOrderID("CK");
            foreach (var item in input.ShipmentDetails)
            {
                item.Id = GuidGenerator.Create();
            }

            //input.TotalQty = input.ShipmentDetails.Sum(x => x.TotalQty);
            //input.TotalWeight = input.ShipmentDetails.Sum(x => x.TotalQty);
            return await base.CreateAsync(input);
        }

        public override async Task<ShipmentHeaderDto> UpdateAsync(Guid id, CreateUpdateShipmentHeaderDto input)
        {
            await DeleteDetail(id);
            foreach (var item in input.ShipmentDetails)
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
            var ShipmentHeader = await _repository.GetAsync(id);
            foreach (var receiptDetail in ShipmentHeader.ShipmentDetails)
            {
                await _shipmentDetailRepository.DeleteAsync(receiptDetail.Id, true);
            }
        }

        

    }
}
