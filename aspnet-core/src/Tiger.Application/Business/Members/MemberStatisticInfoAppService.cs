using System;
using Tiger.Permissions;
using Tiger.Business.Members.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp;

namespace Tiger.Business.Members
{
    [RemoteService(false)]
    public class MemberStatisticInfoAppService : CrudAppService<MemberStatisticInfo, MemberStatisticInfoDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateMemberStatisticInfoDto, CreateUpdateMemberStatisticInfoDto>,
        IMemberStatisticInfoAppService
    {
        //protected override string GetPolicyName { get; set; } = TigerPermissions.MemberStatisticInfo.Default;
        //protected override string GetListPolicyName { get; set; } = TigerPermissions.MemberStatisticInfo.Default;
        //protected override string CreatePolicyName { get; set; } = TigerPermissions.MemberStatisticInfo.Create;
        //protected override string UpdatePolicyName { get; set; } = TigerPermissions.MemberStatisticInfo.Update;
        //protected override string DeletePolicyName { get; set; } = TigerPermissions.MemberStatisticInfo.Delete;

        private readonly IMemberStatisticInfoRepository _repository;
        
        public MemberStatisticInfoAppService(IMemberStatisticInfoRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
