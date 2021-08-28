using System;
using Tiger.Business.Members.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tiger.Business.Members
{
    public interface IMemberLevelAppService :
        ICrudAppService< 
            MemberLevelDto, 
            Guid, 
            PagedAndSortedResultRequestDto,
            CreateUpdateMemberLevelDto,
            CreateUpdateMemberLevelDto>
    {

    }
}