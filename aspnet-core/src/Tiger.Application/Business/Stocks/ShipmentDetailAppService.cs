using System;
using Tiger.Permissions;
using Tiger.Business.Stocks.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp;

namespace Tiger.Business.Stocks
{
    [RemoteService(false)]
    public class ShipmentDetailAppService : CrudAppService<ShipmentDetail, ShipmentDetailDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateShipmentDetailDto, CreateUpdateShipmentDetailDto>,
        IShipmentDetailAppService
    {
        protected override string GetPolicyName { get; set; } = TigerPermissions.ShipmentDetail.Default;
        protected override string GetListPolicyName { get; set; } = TigerPermissions.ShipmentDetail.Default;
        protected override string CreatePolicyName { get; set; } = TigerPermissions.ShipmentDetail.Create;
        protected override string UpdatePolicyName { get; set; } = TigerPermissions.ShipmentDetail.Update;
        protected override string DeletePolicyName { get; set; } = TigerPermissions.ShipmentDetail.Delete;

        private readonly IShipmentDetailRepository _repository;
        
        public ShipmentDetailAppService(IShipmentDetailRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
