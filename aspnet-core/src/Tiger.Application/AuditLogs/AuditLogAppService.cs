using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.AuditLogging;
using Volo.Abp.Domain.Repositories;

namespace TigerAdmin.AuditLogs
{
    public class AuditLogAppService: CrudAppService<
            AuditLog, //The Book entity
            AuditLogDto, //Used to show books
            Guid, //Primary key of the book entity
            PagedAndSortedResultRequestDto //Used for paging/sorting
            >, //Used to create/update a book
        IAuditLogAppService //implement the IBookAppService
    {
        public AuditLogAppService(IRepository<AuditLog, Guid> repository) : base(repository)
        {
            
        }

    }
}
