using System;
using Tiger.Permissions;
using Tiger.Basic.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tiger.Basic
{
    public class WarehouseAppService : CrudAppService<Warehouse, WarehouseDto, Guid, PagedAndSortedResultRequestDto, WarehouseCreateDto, WarehouseUpdateDto>,
        IWarehouseAppService
    {
        //protected override string GetPolicyName { get; set; } = TigerPermissions.Warehouse.Default;
        //protected override string GetListPolicyName { get; set; } = TigerPermissions.Warehouse.Default;
        //protected override string CreatePolicyName { get; set; } = TigerPermissions.Warehouse.Create;
        //protected override string UpdatePolicyName { get; set; } = TigerPermissions.Warehouse.Update;
        //protected override string DeletePolicyName { get; set; } = TigerPermissions.Warehouse.Delete;

        private readonly IWarehouseRepository _repository;
        
        public WarehouseAppService(IWarehouseRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
