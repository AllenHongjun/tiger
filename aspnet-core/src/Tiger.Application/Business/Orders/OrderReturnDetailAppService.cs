using System;
using Tiger.Permissions;
using Tiger.Business.Orders.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp;

namespace Tiger.Business.Orders
{
    [RemoteService(false)]
    public class OrderReturnDetailAppService : CrudAppService<OrderReturnDetail, OrderReturnDetailDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateOrderReturnDetailDto, CreateUpdateOrderReturnDetailDto>,
        IOrderReturnDetailAppService
    {
        protected override string GetPolicyName { get; set; } = TigerPermissions.OrderReturnDetail.Default;
        protected override string GetListPolicyName { get; set; } = TigerPermissions.OrderReturnDetail.Default;
        protected override string CreatePolicyName { get; set; } = TigerPermissions.OrderReturnDetail.Create;
        protected override string UpdatePolicyName { get; set; } = TigerPermissions.OrderReturnDetail.Update;
        protected override string DeletePolicyName { get; set; } = TigerPermissions.OrderReturnDetail.Delete;

        private readonly IOrderReturnDetailRepository _repository;
        
        public OrderReturnDetailAppService(IOrderReturnDetailRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
