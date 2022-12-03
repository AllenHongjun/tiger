using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace Volo.Abp.AuditLogging
{
    public class AuditLogDto : ExtensibleFullAuditedEntityDto<Guid>
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public int? HttpStatusCode { get; set; }
        public string Comments { get; set; }
        /// <summary>
        /// 异常信息
        /// </summary>
        public string Exceptions { get; set; }
        /// <summary>
        /// 请求url
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 请求方式
        /// </summary>
        public string HttpMethod { get; set; }
        /// <summary>
        /// 浏览器信息
        /// </summary>
        public string BrowserInfo { get; set; }
        public string CorrelationId { get; set; }
        public string ClientId { get; set; }
        public string ClientName { get; set; }
        /// <summary>
        /// 客户端ip
        /// </summary>
        public string ClientIpAddress { get; set; }
        /// <summary>
        /// 执行时间
        /// </summary>
        public int ExecutionDuration { get; set; }
        public DateTime ExecutionTime { get; set; }
        public Guid? ImpersonatorTenantId { get; set; }
        public Guid? ImpersonatorUserId { get; set; }
        /// <summary>
        /// 租户名称
        /// </summary>
        public string TenantName { get; set; }
        /// <summary>
        /// 租户id
        /// </summary>
        public Guid? TenantId { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        public Guid? UserId { get; set; }
        public string ApplicationName { get; set; }
        //public List<EntityChangeDto> EntityChanges { get; set; }

        public List<AuditLogActionDto> Actions { get; set; }
    }
}
