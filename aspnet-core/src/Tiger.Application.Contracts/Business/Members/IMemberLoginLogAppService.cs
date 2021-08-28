using System;
using Tiger.Business.Members.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tiger.Business.Members
{
    public interface IMemberLoginLogAppService :
        ICrudAppService< 
            MemberLoginLogDto, 
            Guid, 
            PagedAndSortedResultRequestDto,
            CreateUpdateMemberLoginLogDto,
            CreateUpdateMemberLoginLogDto>
    {

    }
}