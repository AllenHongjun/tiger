using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tiger.SecurityLogs
{
    public interface ISecurityLogAppService:ICrudAppService<
            SecurityLogDto, // Used to show securityLogs
            Guid,
            PagedAndSortedResultRequestDto
            > 
    {
    }
}
