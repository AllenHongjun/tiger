using System;
using Tiger.Permissions;
using Tiger.Stock.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tiger.Stock
{
    public class InventoryHistoryAppService : CrudAppService<InventoryHistory, InventoryHistoryDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateInventoryHistoryDto, CreateUpdateInventoryHistoryDto>,
        IInventoryHistoryAppService
    {
        protected override string GetPolicyName { get; set; } = TigerPermissions.InventoryHistory.Default;
        protected override string GetListPolicyName { get; set; } = TigerPermissions.InventoryHistory.Default;
        protected override string CreatePolicyName { get; set; } = TigerPermissions.InventoryHistory.Create;
        protected override string UpdatePolicyName { get; set; } = TigerPermissions.InventoryHistory.Update;
        protected override string DeletePolicyName { get; set; } = TigerPermissions.InventoryHistory.Delete;

        private readonly IInventoryHistoryRepository _repository;
        
        public InventoryHistoryAppService(IInventoryHistoryRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
