using System;
using Tiger.Permissions;
using Tiger.Business.Orders.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tiger.Business.Orders
{
    public class OrderReturnHeaderAppService : CrudAppService<OrderReturnHeader, OrderReturnHeaderDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateOrderReturnHeaderDto, CreateUpdateOrderReturnHeaderDto>,
        IOrderReturnHeaderAppService
    {
        protected override string GetPolicyName { get; set; } = TigerPermissions.OrderReturnHeader.Default;
        protected override string GetListPolicyName { get; set; } = TigerPermissions.OrderReturnHeader.Default;
        protected override string CreatePolicyName { get; set; } = TigerPermissions.OrderReturnHeader.Create;
        protected override string UpdatePolicyName { get; set; } = TigerPermissions.OrderReturnHeader.Update;
        protected override string DeletePolicyName { get; set; } = TigerPermissions.OrderReturnHeader.Delete;

        private readonly IOrderReturnHeaderRepository _repository;
        
        public OrderReturnHeaderAppService(IOrderReturnHeaderRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
