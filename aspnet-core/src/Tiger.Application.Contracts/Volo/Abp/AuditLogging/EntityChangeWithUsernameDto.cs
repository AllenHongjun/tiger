using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.AuditLogging;

namespace Tiger.Volo.Abp.AuditLogging
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
