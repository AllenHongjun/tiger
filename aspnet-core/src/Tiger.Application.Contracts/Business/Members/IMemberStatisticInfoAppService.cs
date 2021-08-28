using System;
using Tiger.Business.Members.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tiger.Business.Members
{
    public interface IMemberStatisticInfoAppService :
        ICrudAppService< 
            MemberStatisticInfoDto, 
            Guid, 
            PagedAndSortedResultRequestDto,
            CreateUpdateMemberStatisticInfoDto,
            CreateUpdateMemberStatisticInfoDto>
    {

    }
}