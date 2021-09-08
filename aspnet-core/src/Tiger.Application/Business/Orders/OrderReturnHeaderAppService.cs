using System;
using Tiger.Permissions;
using Tiger.Business.Orders.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using System.Threading.Tasks;
using Tiger.Domain.CoreModule.Utilities;
using System.Linq;
using Volo.Abp;

namespace Tiger.Business.Orders
{
    [RemoteService(false)]
    public class OrderReturnHeaderAppService : CrudAppService<OrderReturnHeader, OrderReturnHeaderDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateOrderReturnHeaderDto, CreateUpdateOrderReturnHeaderDto>,
        IOrderReturnHeaderAppService
    {
        //protected override string GetPolicyName { get; set; } = TigerPermissions.OrderReturnHeader.Default;
        //protected override string GetListPolicyName { get; set; } = TigerPermissions.OrderReturnHeader.Default;
        //protected override string CreatePolicyName { get; set; } = TigerPermissions.OrderReturnHeader.Create;
        //protected override string UpdatePolicyName { get; set; } = TigerPermissions.OrderReturnHeader.Update;
        //protected override string DeletePolicyName { get; set; } = TigerPermissions.OrderReturnHeader.Delete;

        private readonly IOrderReturnHeaderRepository _repository;
        private readonly IOrderReturnDetailRepository _orderReturnDetailRepository;


        public OrderReturnHeaderAppService(
            IOrderReturnHeaderRepository repository,
            IOrderReturnDetailRepository orderReturnDetailRepository
            ) : base(repository)
        {
            _repository = repository;
            _orderReturnDetailRepository = orderReturnDetailRepository;
        }


        public override async Task<OrderReturnHeaderDto> CreateAsync(CreateUpdateOrderReturnHeaderDto input)
        {
            input.Code = Utility.CreateOrderID("CGRK");
            foreach (var item in input.OrderReturnDetails)
            {
                item.Id = GuidGenerator.Create();
            }

            //input.TotalQty = input.OrderReturnDetails.Sum(x => x.ReturnAmount);
            //input.TotalWeight = input.OrderReturnDetails.Sum(x => x.TotalQty);
            return await base.CreateAsync(input);
        }

        public override async Task<OrderReturnHeaderDto> UpdateAsync(Guid id, CreateUpdateOrderReturnHeaderDto input)
        {
            await DeleteDetail(id);
            foreach (var item in input.OrderReturnDetails)
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
            var OrderReturnHeader = await _repository.GetAsync(id);
            foreach (var receiptDetail in OrderReturnHeader.OrderReturnDetails)
            {
                await _orderReturnDetailRepository.DeleteAsync(receiptDetail.Id, true);
            }
        }

        public override async Task<OrderReturnHeaderDto> GetAsync(Guid id)
        {
            var query = await _repository.GetAsync(id);
            return ObjectMapper.Map<OrderReturnHeader, OrderReturnHeaderDto>(query);
        }
    }
}
