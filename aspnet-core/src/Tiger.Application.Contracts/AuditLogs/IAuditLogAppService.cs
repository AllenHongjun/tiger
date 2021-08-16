using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace TigerAdmin.AuditLogs
{
    public interface IAuditLogAppService: ICrudAppService<
            AuditLogDto, // Used to show books
            Guid,
            PagedAndSortedResultRequestDto
            > // Used to create/update a book
    {
    }
}
