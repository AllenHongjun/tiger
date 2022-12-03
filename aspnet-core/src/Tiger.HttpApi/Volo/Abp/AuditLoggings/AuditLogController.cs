using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tiger.Controllers;
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
    public class AuditLogController : TigerController, IAuditLogAppService
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
        public virtual async Task DeleteAsync(Guid id)
        {
             await AuditLogAppService.DeleteAsync(id);
        }

        /// <summary>
        /// 删除多条
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("delete-many")]
        public virtual async Task DeleteManyAsync(Guid[] ids)
        {
             await AuditLogAppService.DeleteManyAsync(ids);
        }

        /// <summary>
        /// 获取单条日志
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public virtual async Task<AuditLogDto> GetAsync(Guid id)
        {
            return await AuditLogAppService.GetAsync(id);
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

        /// <summary>
        /// 每日请求平均执行时间
        /// </summary>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns></returns>
        [HttpGet]
        [Route("average-execution-duration-per-day")]
        public Task<Dictionary<DateTime, double>> GetAverageExecutionDurationPerDayAsync(DateTime startDate, DateTime endDate)
        {
            return AuditLogAppService.GetAverageExecutionDurationPerDayAsync(startDate, endDate);
        }


        /// <summary>
        /// 获取实体变更明细
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("entity-change/{id}")]
        public async Task<EntityChangeDto> GetEntityChangeAsync(Guid id)
        {
            return await AuditLogAppService.GetEntityChangeAsync(id);
        }

        /// <summary>
        /// 获取实体变更列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("entity-changes")]
        public async Task<PagedResultDto<EntityChangeDto>> GetEntityChangeListAsync(GetEntityChangeDto input)
        {
            return await AuditLogAppService.GetEntityChangeListAsync(input);
        }
    }
}
