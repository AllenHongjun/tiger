using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;
using Volo.Abp.MultiTenancy;

namespace Tiger.Volo.Abp.Identity.IdentitySecurityLogs.Dto
{   
    /// <summary>
    /// 安全日志
    /// </summary>
    public class IdentitySecurityLogDto: EntityDto<Guid>
    {
        public Guid? TenantId { get;  set; }

        /// <summary>
        /// 应用名称
        /// </summary>
        public string ApplicationName { get;  set; }

        /// <summary>
        /// 
        /// </summary>
        public string Identity { get;  set; }

        public string Action { get;  set; }

        public Guid? UserId { get;  set; }

        public string UserName { get;  set; }

        public string TenantName { get;  set; }

        public string ClientId { get;  set; }

        /// <summary>
        /// 
        /// </summary>
        public string CorrelationId { get;  set; }

        /// <summary>
        /// 客户端ip地址
        /// </summary>
        public string ClientIpAddress { get;  set; }

        /// <summary>
        /// 浏览器信息
        /// </summary>
        public string BrowserInfo { get;  set; }

        public DateTime CreationTime { get;  set; }

    }
}
