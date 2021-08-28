using System;
using Tiger.Permissions;
using Tiger.Business.Members.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp;

namespace Tiger.Business.Members
{
    [RemoteService(false)]
    public class MemberReceiveAddressAppService : CrudAppService<MemberReceiveAddress, MemberReceiveAddressDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateMemberReceiveAddressDto, CreateUpdateMemberReceiveAddressDto>,
        IMemberReceiveAddressAppService
    {
        //protected override string GetPolicyName { get; set; } = TigerPermissions.MemberReceiveAddress.Default;
        //protected override string GetListPolicyName { get; set; } = TigerPermissions.MemberReceiveAddress.Default;
        //protected override string CreatePolicyName { get; set; } = TigerPermissions.MemberReceiveAddress.Create;
        //protected override string UpdatePolicyName { get; set; } = TigerPermissions.MemberReceiveAddress.Update;
        //protected override string DeletePolicyName { get; set; } = TigerPermissions.MemberReceiveAddress.Delete;

        private readonly IMemberReceiveAddressRepository _repository;
        
        public MemberReceiveAddressAppService(IMemberReceiveAddressRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
