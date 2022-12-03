using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Volo.Abp.AuditLogging
{
    public class GetAuditLogDto : PagedAndSortedResultRequestDto
    {
        public GetAuditLogDto()
        {
        }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }
        /// <summary>
        /// 请求方式
        /// </summary>
        public string HttpMethod { get; set; }
        /// <summary>
        /// 请求URL
        /// </summary>
        public string Url { get; set; }
        public string UserName { get; set; }
        public string ApplicationName { get; set; }
        public string CorrelationId { get; set; }
        /// <summary>
        /// 最大执行时间
        /// </summary>
        public int? MaxExecutionDuration { get; set; }
        public int? MinExecutionDuration { get; set; }
        /// <summary>
        /// 是否有异常
        /// </summary>
        public bool? HasException { get; set; }
        /// <summary>
        /// 请求状态码
        /// </summary>
        public HttpStatusCode? HttpStatusCode { get; set; }
        /// <summary>
        /// 是否包含明细
        /// </summary>
        public bool IncludeDetails { get; set; }
    }
}
