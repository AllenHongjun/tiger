using System;
using Tiger.Permissions;
using Tiger.Stock.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tiger.Stock
{
    public class InventoryAppService : CrudAppService<Inventory, InventoryDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateInventoryDto, CreateUpdateInventoryDto>,
        IInventoryAppService
    {
        protected override string GetPolicyName { get; set; } = TigerPermissions.Inventory.Default;
        protected override string GetListPolicyName { get; set; } = TigerPermissions.Inventory.Default;
        protected override string CreatePolicyName { get; set; } = TigerPermissions.Inventory.Create;
        protected override string UpdatePolicyName { get; set; } = TigerPermissions.Inventory.Update;
        protected override string DeletePolicyName { get; set; } = TigerPermissions.Inventory.Delete;

        private readonly IInventoryRepository _repository;
        
        public InventoryAppService(IInventoryRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
