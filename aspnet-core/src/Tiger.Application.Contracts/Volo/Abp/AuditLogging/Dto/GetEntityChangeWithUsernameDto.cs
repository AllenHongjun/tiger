using System;
using System.Collections.Generic;
using System.Text;

namespace Tiger.Volo.Abp.AuditLogging.Dto
{
    public class GetEntityChangeWithUsernameDto
    {
        public string EntityId { get; set; }

        public string EntityTypeFullName { get; set; }
    }
}
