using System;
using System.Collections.Generic;
using System.Text;

namespace Tiger.Volo.Abp.AuditLogging.Dto
{
    public class EntityChangeWithUsernameDto
    {
        /// <summary>
        /// 操作人
        /// </summary>
        public string Username { get; set; }

        public EntityChangeDto EntityChange { get; set; }

    }
}
