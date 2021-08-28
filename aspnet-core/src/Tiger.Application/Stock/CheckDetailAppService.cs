using System;
using Tiger.Permissions;
using Tiger.Stock.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tiger.Stock
{
    public class CheckDetailAppService : CrudAppService<CheckDetail, CheckDetailDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateCheckDetailDto, CreateUpdateCheckDetailDto>,
        ICheckDetailAppService
    {
        protected override string GetPolicyName { get; set; } = TigerPermissions.CheckDetail.Default;
        protected override string GetListPolicyName { get; set; } = TigerPermissions.CheckDetail.Default;
        protected override string CreatePolicyName { get; set; } = TigerPermissions.CheckDetail.Create;
        protected override string UpdatePolicyName { get; set; } = TigerPermissions.CheckDetail.Update;
        protected override string DeletePolicyName { get; set; } = TigerPermissions.CheckDetail.Delete;

        private readonly ICheckDetailRepository _repository;
        
        public CheckDetailAppService(ICheckDetailRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
