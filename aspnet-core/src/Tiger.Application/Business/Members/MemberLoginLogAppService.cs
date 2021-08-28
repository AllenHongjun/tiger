using System;
using Tiger.Permissions;
using Tiger.Business.Members.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tiger.Business.Members
{
    public class MemberLoginLogAppService : CrudAppService<MemberLoginLog, MemberLoginLogDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateMemberLoginLogDto, CreateUpdateMemberLoginLogDto>,
        IMemberLoginLogAppService
    {
        protected override string GetPolicyName { get; set; } = TigerPermissions.MemberLoginLog.Default;
        protected override string GetListPolicyName { get; set; } = TigerPermissions.MemberLoginLog.Default;
        protected override string CreatePolicyName { get; set; } = TigerPermissions.MemberLoginLog.Create;
        protected override string UpdatePolicyName { get; set; } = TigerPermissions.MemberLoginLog.Update;
        protected override string DeletePolicyName { get; set; } = TigerPermissions.MemberLoginLog.Delete;

        private readonly IMemberLoginLogRepository _repository;
        
        public MemberLoginLogAppService(IMemberLoginLogRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
