using System;
using Tiger.Business.Members.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tiger.Business.Members
{
    public interface IMemberAppService :
        ICrudAppService< 
            MemberDto, 
            Guid, 
            PagedAndSortedResultRequestDto,
            CreateUpdateMemberDto,
            CreateUpdateMemberDto>
    {

    }
}