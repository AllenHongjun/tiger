using System;
using Tiger.Permissions;
using Tiger.Basic.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tiger.Basic
{
    public class SupplyAppService : CrudAppService<Supply, SupplyDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateSupplyDto, CreateUpdateSupplyDto>,
        ISupplyAppService
    {
        //protected override string GetPolicyName { get; set; } = TigerPermissions.Supply.Default;
        //protected override string GetListPolicyName { get; set; } = TigerPermissions.Supply.Default;
        //protected override string CreatePolicyName { get; set; } = TigerPermissions.Supply.Create;
        //protected override string UpdatePolicyName { get; set; } = TigerPermissions.Supply.Update;
        //protected override string DeletePolicyName { get; set; } = TigerPermissions.Supply.Delete;

        private readonly ISupplyRepository _repository;
        
        public SupplyAppService(ISupplyRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
