using System;
using Tiger.Permissions;
using Tiger.Business.Members.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp;

namespace Tiger.Business.Members
{
    [RemoteService(false)]
    public class MemberLevelAppService : CrudAppService<MemberLevel, MemberLevelDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateMemberLevelDto, CreateUpdateMemberLevelDto>,
        IMemberLevelAppService
    {
        //protected override string GetPolicyName { get; set; } = TigerPermissions.MemberLevel.Default;
        //protected override string GetListPolicyName { get; set; } = TigerPermissions.MemberLevel.Default;
        //protected override string CreatePolicyName { get; set; } = TigerPermissions.MemberLevel.Create;
        //protected override string UpdatePolicyName { get; set; } = TigerPermissions.MemberLevel.Update;
        //protected override string DeletePolicyName { get; set; } = TigerPermissions.MemberLevel.Delete;

        private readonly IMemberLevelRepository _repository;
        
        public MemberLevelAppService(IMemberLevelRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
