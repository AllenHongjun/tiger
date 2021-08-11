using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TigerAdmin.Controllers;
using Volo.Abp.Application.Dtos;


namespace Volo.Abp.AuditLogging
{   
    /// <summary>
    /// 系统日志
    /// </summary>
    [RemoteService(Name = AuditLogRemoteServiceConsts.RemoteServiceName)]
    [ControllerName("AuditLogging")]
    [Area("auditlogging")]
    [Route("/api/audit-logging/audit-logs")]
    public class AuditLogController : TigerAdminController, IAuditLogAppService
    {
        protected IAuditLogAppService AuditLogAppService { get; }
        public AuditLogController(IAuditLogAppService auditLogAppService)
        {
            AuditLogAppService = auditLogAppService;
        }

        /// <summary>
        /// 删除一条
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return AuditLogAppService.DeleteAsync(id);
        }

        /// <summary>
        /// 删除多条
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("delete-many")]
        public virtual Task DeleteManyAsync(Guid[] ids)
        {
            return AuditLogAppService.DeleteManyAsync(ids);
        }

        /// <summary>
        /// 获取单条日志
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public virtual Task<AuditLogDto> GetAsync(Guid id)
        {
            return AuditLogAppService.GetAsync(id);
        }

        /// <summary>
        /// 获取系统日志列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        public virtual Task<PagedResultDto<AuditLogDto>> GetListAsync(GetAuditLogDto input)
        {
            return AuditLogAppService.GetListAsync(input);
        }
    }
}
