using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tiger;
using Tiger.Volo.Abp.AuditLogging.Dto;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Auditing;
using Volo.Abp.Data;
using Volo.Abp.Identity;
using Volo.Abp.ObjectMapping;
using Volo.Abp.Users;

namespace Volo.Abp.AuditLogging
{
    /// <summary>
    /// 系统审计日志功能
    /// </summary>
    /// <remarks>
    /// 审计日志 https://docs.abp.io/zh-Hans/abp/7.0/Audit-Logging
    /// </remarks>
    [RemoteService(false)]
    [Authorize(AuditLogPermissions.AuditLogs.Default)]
    public class AuditLogAppService : TigerAppService, IAuditLogAppService
    {
        protected IAuditLogRepository AuditLogRepository { get; }
        protected IAuditingHelper AuditingHelper { get; }
        protected IAuditingManager AuditingManager { get; }
        protected IIdentitySecurityLogRepository IdentitySecurityLogRepository { get; }

        private readonly IDataFilter _dataFilter;
        public AuditLogAppService(IAuditLogRepository auditLogRepository, IDataFilter dataFilter)
        {
            AuditLogRepository = auditLogRepository;
            _dataFilter=dataFilter;
        }

        #region AuditLog
        /// <summary>
        /// 获取一条
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<AuditLogDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<AuditLog, AuditLogDto>(await AuditLogRepository.GetAsync(id, true));
        }

        /// <summary>
        /// 审核日志列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <remarks>
        /// 例子:
        /// Get api/Values/1
        /// </remarks>
        /// <returns>测试字符串</returns> 
        public virtual async Task<PagedResultDto<AuditLogDto>> GetListAsync(GetAuditLogDto input)
        {
            //临时禁用软删除的数据过滤
            using (_dataFilter.Disable<ISoftDelete>())
            {
                var count = await AuditLogRepository.GetCountAsync(
                startTime: input.StartTime,
                endTime: input.EndTime,
                httpMethod: input.HttpMethod,
                url: input.Url,
                userName: input.UserName,
                applicationName: input.ApplicationName,
                correlationId: input.CorrelationId,
                maxExecutionDuration: input.MaxExecutionDuration,
                minExecutionDuration: input.MinExecutionDuration,
                hasException: input.HasException,
                httpStatusCode: input.HttpStatusCode
            );
                var list = await AuditLogRepository.GetListAsync(
                    sorting: input.Sorting,
                    maxResultCount: input.MaxResultCount,
                    skipCount: input.SkipCount,
                    startTime: input.StartTime,
                    endTime: input.EndTime,
                    httpMethod: input.HttpMethod,
                    url: input.Url,
                    userName: input.UserName,
                    applicationName: input.ApplicationName,
                    correlationId: input.CorrelationId,
                    maxExecutionDuration: input.MaxExecutionDuration,
                    minExecutionDuration: input.MinExecutionDuration,
                    hasException: input.HasException,
                    httpStatusCode: input.HttpStatusCode,
                    includeDetails: input.IncludeDetails
                );

                List<AuditLogDto> auditLogs = new List<AuditLogDto>();
                ObjectMapper.Map<List<AuditLog>, List<AuditLogDto>>(list, auditLogs);

                return new PagedResultDto<AuditLogDto>(
                    count,
                    ObjectMapper.Map<List<AuditLog>, List<AuditLogDto>>(list)
                );
            }

        }

        /// <summary>
        /// 删除一条日志
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(AuditLogPermissions.AuditLogs.Delete)]
        //[DisableAuditing] // 在控制器级别禁用日志
        public async Task DeleteAsync(Guid id)
        {
            // 获取一条数据
            var auditLog = await AuditLogRepository.GetAsync(id);
            // 清空关联外键表数据
            auditLog.EntityChanges.Clear();
            auditLog.Actions.Clear();
            await AuditLogRepository.DeleteAsync(id);
        }

        /// <summary>
        /// 删除多条日志
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [Authorize(AuditLogPermissions.AuditLogs.Delete)]
        public virtual async Task DeleteManyAsync(params Guid[] ids)
        {
            foreach (var id in ids)
            {
                var auditLog = await AuditLogRepository.GetAsync(id);
                auditLog.EntityChanges.Clear();
                auditLog.Actions.Clear();
                await AuditLogRepository.DeleteAsync(id);
            }
        }


        /// <summary>
        /// 获取日志中的错误率
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        [Authorize(AuditLogPermissions.AuditLogs.Default)]
        public virtual async Task<List<ErrorRateDto>> GetErrorRate(DateTime startDate, DateTime endDate)
        {
            var errorCount = await AuditLogRepository.GetCountAsync(startTime: startDate, endTime: endDate,hasException:true);
            var successCount = await AuditLogRepository.GetCountAsync( startTime: startDate, endTime: endDate, hasException: false);
            List<ErrorRateDto> result = new List<ErrorRateDto>
            {
                new ErrorRateDto { name = "Success", value = successCount },
                new ErrorRateDto { name = "Error", value = errorCount }
            };
            return result;
        }

        

        /// <summary>
        /// 每日请求平均执行时间
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        [Authorize(AuditLogPermissions.AuditLogs.Default)]
        public virtual async Task<Dictionary<DateTime, double>> GetAverageExecutionDurationPerDayAsync(DateTime startDate, DateTime endDate)
        {
            return await AuditLogRepository.GetAverageExecutionDurationPerDayAsync(startDate, endDate);
        }
        #endregion

        #region EntityChange
        /// <summary>
        /// 根据id获取实体变更
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<EntityChangeDto> GetEntityChangeAsync(Guid id)
        {
            return ObjectMapper.
                Map<EntityChange, EntityChangeDto>(
                await AuditLogRepository.GetEntityChange(id)
                );
        }


        /// <summary>
        /// 获取实体变更列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public virtual async Task<PagedResultDto<EntityChangeDto>> GetEntityChangeListAsync(GetEntityChangeDto input)
        {
            var entityChangeCount = await AuditLogRepository.GetEntityChangeCountAsync(
                input.AuditLogId,
                input.StartTime,
                input.EndTime,
                input.ChangeType,
                input.EntityId,
                input.EntityTypeFullName);

            var entityChanges = await AuditLogRepository.GetEntityChangeListAsync(
                input.Sorting,
                input.MaxResultCount,
                input.SkipCount,
                input.AuditLogId,
                input.StartTime,
                input.EndTime,
                input.ChangeType,
                input.EntityId,
                input.EntityTypeFullName,
                includeDetails: true);

            return new PagedResultDto<EntityChangeDto>(entityChangeCount,
                ObjectMapper.Map<List<EntityChange>, List<EntityChangeDto>>(entityChanges));
        }

        /// <summary>
        /// 获取实体变更及操作人
        /// </summary>
        /// <param name="entityChangeId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public virtual async Task<EntityChangeWithUsernameDto> GetEntityChangeWithUsernameAsync(Guid entityChangeId)
        {
            return ObjectMapper.Map<EntityChangeWithUsername, EntityChangeWithUsernameDto>(await AuditLogRepository.GetEntityChangeWithUsernameAsync(entityChangeId));
        }

        /// <summary>
        /// 获取实体变更及操作人列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public virtual async Task<List<EntityChangeWithUsernameDto>> GetEntityChangesWithUsernameAsync(GetEntityChangeWithUsernameDto input)
        {
            return ObjectMapper.Map<List<EntityChangeWithUsername>, List<EntityChangeWithUsernameDto>>(
                await AuditLogRepository.GetEntityChangesWithUsernameAsync(input.EntityId, input.EntityTypeFullName));
        } 
        #endregion

    }
}
