using System;
using Tiger.Business.Members.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tiger.Business.Members
{
    public interface IMemberReceiveAddressAppService :
        ICrudAppService< 
            MemberReceiveAddressDto, 
            Guid, 
            PagedAndSortedResultRequestDto,
            CreateUpdateMemberReceiveAddressDto,
            CreateUpdateMemberReceiveAddressDto>
    {

    }
}