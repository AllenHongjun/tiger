using System;
using Tiger.Permissions;
using Tiger.Business.Members.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tiger.Business.Members
{
    public class MemberAppService : CrudAppService<Member, MemberDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateMemberDto, CreateUpdateMemberDto>,
        IMemberAppService
    {
        //protected override string GetPolicyName { get; set; } = TigerPermissions.Member.Default;
        //protected override string GetListPolicyName { get; set; } = TigerPermissions.Member.Default;
        //protected override string CreatePolicyName { get; set; } = TigerPermissions.Member.Create;
        //protected override string UpdatePolicyName { get; set; } = TigerPermissions.Member.Update;
        //protected override string DeletePolicyName { get; set; } = TigerPermissions.Member.Delete;

        private readonly IMemberRepository _repository;
        
        public MemberAppService(IMemberRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
