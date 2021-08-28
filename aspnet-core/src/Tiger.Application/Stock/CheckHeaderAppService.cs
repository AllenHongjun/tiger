using System;
using Tiger.Permissions;
using Tiger.Stock.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tiger.Stock
{
    public class CheckHeaderAppService : CrudAppService<CheckHeader, CheckHeaderDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateCheckHeaderDto, CreateUpdateCheckHeaderDto>,
        ICheckHeaderAppService
    {
        protected override string GetPolicyName { get; set; } = TigerPermissions.CheckHeader.Default;
        protected override string GetListPolicyName { get; set; } = TigerPermissions.CheckHeader.Default;
        protected override string CreatePolicyName { get; set; } = TigerPermissions.CheckHeader.Create;
        protected override string UpdatePolicyName { get; set; } = TigerPermissions.CheckHeader.Update;
        protected override string DeletePolicyName { get; set; } = TigerPermissions.CheckHeader.Delete;

        private readonly ICheckHeaderRepository _repository;
        
        public CheckHeaderAppService(ICheckHeaderRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
