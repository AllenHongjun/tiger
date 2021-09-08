using System;
using Tiger.Permissions;
using Tiger.Basic.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using System.Linq;
using System.Collections.Generic;

namespace Tiger.Basic
{
    [RemoteService(false)]
    public class WarehouseAppService : CrudAppService<Warehouse, WarehouseDto, Guid, PagedAndSortedResultRequestDto, WarehouseCreateDto, WarehouseUpdateDto>,
        IWarehouseAppService
    {
        //protected override string GetPolicyName { get; set; } = TigerPermissions.Warehouse.Default;
        //protected override string GetListPolicyName { get; set; } = TigerPermissions.Warehouse.Default;
        //protected override string CreatePolicyName { get; set; } = TigerPermissions.Warehouse.Create;
        //protected override string UpdatePolicyName { get; set; } = TigerPermissions.Warehouse.Update;
        //protected override string DeletePolicyName { get; set; } = TigerPermissions.Warehouse.Delete;

        private readonly IWarehouseRepository _repository;

        protected override string GetPolicyName { get => base.GetPolicyName; set => base.GetPolicyName = value; }
        protected override string GetListPolicyName { get => base.GetListPolicyName; set => base.GetListPolicyName = value; }
        protected override string CreatePolicyName { get => base.CreatePolicyName; set => base.CreatePolicyName = value; }
        protected override string UpdatePolicyName { get => base.UpdatePolicyName; set => base.UpdatePolicyName = value; }
        protected override string DeletePolicyName { get => base.DeletePolicyName; set => base.DeletePolicyName = value; }

        public WarehouseAppService(IWarehouseRepository repository) : base(repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// 获取所有仓库
        /// </summary>
        /// <returns></returns>
        public async Task<List<WarehouseDto>> GetAllAsync()
        {
            var warehouses = await _repository.GetListAsync();
            return ObjectMapper.Map<List<Warehouse>, List<WarehouseDto>>(warehouses);
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }

        protected override Task CheckPolicyAsync(string policyName)
        {
            return base.CheckPolicyAsync(policyName);
        }

        protected override IStringLocalizer CreateLocalizer()
        {
            return base.CreateLocalizer();
        }

        public override Task<WarehouseDto> GetAsync(Guid id)
        {
            return base.GetAsync(id);
        }

        public override Task<PagedResultDto<WarehouseDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            return base.GetListAsync(input);
        }

        protected override Task CheckGetPolicyAsync()
        {
            return base.CheckGetPolicyAsync();
        }

        protected override Task CheckGetListPolicyAsync()
        {
            return base.CheckGetListPolicyAsync();
        }

        protected override IQueryable<Warehouse> ApplySorting(IQueryable<Warehouse> query, PagedAndSortedResultRequestDto input)
        {
            return base.ApplySorting(query, input);
        }

        protected override IQueryable<Warehouse> ApplyPaging(IQueryable<Warehouse> query, PagedAndSortedResultRequestDto input)
        {
            return base.ApplyPaging(query, input);
        }

        protected override IQueryable<Warehouse> CreateFilteredQuery(PagedAndSortedResultRequestDto input)
        {
            return base.CreateFilteredQuery(input);
        }

        protected override Task<WarehouseDto> MapToGetOutputDtoAsync(Warehouse entity)
        {
            return base.MapToGetOutputDtoAsync(entity);
        }

        protected override WarehouseDto MapToGetOutputDto(Warehouse entity)
        {
            return base.MapToGetOutputDto(entity);
        }

        protected override Task<List<WarehouseDto>> MapToGetListOutputDtosAsync(List<Warehouse> entities)
        {
            return base.MapToGetListOutputDtosAsync(entities);
        }

        public override Task<WarehouseDto> CreateAsync(WarehouseCreateDto input)
        {
            return base.CreateAsync(input);
        }

        public override Task<WarehouseDto> UpdateAsync(Guid id, WarehouseUpdateDto input)
        {
            return base.UpdateAsync(id, input);
        }

        public override Task DeleteAsync(Guid id)
        {
            return base.DeleteAsync(id);
        }

        protected override Task CheckCreatePolicyAsync()
        {
            return base.CheckCreatePolicyAsync();
        }

        protected override Task CheckUpdatePolicyAsync()
        {
            return base.CheckUpdatePolicyAsync();
        }

        protected override Task CheckDeletePolicyAsync()
        {
            return base.CheckDeletePolicyAsync();
        }

        protected override Task<Warehouse> MapToEntityAsync(WarehouseCreateDto createInput)
        {
            return base.MapToEntityAsync(createInput);
        }

        protected override Warehouse MapToEntity(WarehouseCreateDto createInput)
        {
            return base.MapToEntity(createInput);
        }

        protected override void SetIdForGuids(Warehouse entity)
        {
            base.SetIdForGuids(entity);
        }

        protected override Task MapToEntityAsync(WarehouseUpdateDto updateInput, Warehouse entity)
        {
            return base.MapToEntityAsync(updateInput, entity);
        }

        protected override void TryToSetTenantId(Warehouse entity)
        {
            base.TryToSetTenantId(entity);
        }

        protected override bool HasTenantIdProperty(Warehouse entity)
        {
            return base.HasTenantIdProperty(entity);
        }

        protected override Task DeleteByIdAsync(Guid id)
        {
            return base.DeleteByIdAsync(id);
        }

        protected override Task<Warehouse> GetEntityByIdAsync(Guid id)
        {
            return base.GetEntityByIdAsync(id);
        }

        protected override void MapToEntity(WarehouseUpdateDto updateInput, Warehouse entity)
        {
            base.MapToEntity(updateInput, entity);
        }

        protected override IQueryable<Warehouse> ApplyDefaultSorting(IQueryable<Warehouse> query)
        {
            return base.ApplyDefaultSorting(query);
        }

        protected override Task<WarehouseDto> MapToGetListOutputDtoAsync(Warehouse entity)
        {
            return base.MapToGetListOutputDtoAsync(entity);
        }

        protected override WarehouseDto MapToGetListOutputDto(Warehouse entity)
        {
            return base.MapToGetListOutputDto(entity);
        }
    }
}
