using System;
using Tiger.Permissions;
using Tiger.Stock.Dtos;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tiger.Stock
{
    [RemoteService(false)]
    public class ReverseDetailAppService : CrudAppService<ReverseDetail, ReverseDetailDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateReverseDetailDto, CreateUpdateReverseDetailDto>,
        IReverseDetailAppService
    {
        //protected override string GetPolicyName { get; set; } = TigerPermissions.ReverseDetail.Default;
        //protected override string GetListPolicyName { get; set; } = TigerPermissions.ReverseDetail.Default;
        //protected override string CreatePolicyName { get; set; } = TigerPermissions.ReverseDetail.Create;
        //protected override string UpdatePolicyName { get; set; } = TigerPermissions.ReverseDetail.Update;
        //protected override string DeletePolicyName { get; set; } = TigerPermissions.ReverseDetail.Delete;

        private readonly IReverseDetailRepository _repository;
        
        public ReverseDetailAppService(IReverseDetailRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
