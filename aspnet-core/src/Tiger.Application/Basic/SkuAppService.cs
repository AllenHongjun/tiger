using System;
using Tiger.Permissions;
using Tiger.Basic.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp;

namespace Tiger.Basic
{
    [RemoteService(false)]
    public class SkuAppService : CrudAppService<Sku, SkuDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateSkuDto, CreateUpdateSkuDto>,
        ISkuAppService
    {
        //protected override string GetPolicyName { get; set; } = TigerPermissions.Sku.Default;
        //protected override string GetListPolicyName { get; set; } = TigerPermissions.Sku.Default;
        //protected override string CreatePolicyName { get; set; } = TigerPermissions.Sku.Create;
        //protected override string UpdatePolicyName { get; set; } = TigerPermissions.Sku.Update;
        //protected override string DeletePolicyName { get; set; } = TigerPermissions.Sku.Delete;

        private readonly ISkuRepository _repository;
        
        public SkuAppService(ISkuRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
