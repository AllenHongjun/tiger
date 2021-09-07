using System;
using Tiger.Permissions;
using Tiger.Business.Stocks.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp;

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
        
        public ShipmentHeaderAppService(IShipmentHeaderRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
