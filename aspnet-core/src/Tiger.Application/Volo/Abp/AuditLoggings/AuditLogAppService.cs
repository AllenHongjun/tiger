using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tiger;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Auditing;
using Volo.Abp.AuditLogging;
using Volo.Abp.Data;
using Volo.Abp.Users;

namespace Volo.Abp.AuditLogging
{       
    /// <summary>
    /// 系统日志功能
    /// </summary>
    [RemoteService(false)]
    //[DisableAuditing]  // 禁用请求日志
    [Authorize(AuditLogPermissions.AuditLogs.Default)]
    public class AuditLogAppService : TigerAppService, IAuditLogAppService
    {
        protected IAuditLogRepository AuditLogRepository { get; }

        //用于获取有关当前活动的用户信息  Appservice 有这个属性（其他自定义的类可以注入来使用）
        private readonly ICurrentUser _currentUser;

        //IDataFilter 服务: 启用/禁用 数据过滤
        private readonly IDataFilter _dataFilter;
        public AuditLogAppService(IAuditLogRepository auditLogRepository, IDataFilter dataFilter, ICurrentUser currentUser)
        {
            AuditLogRepository = auditLogRepository;
            _dataFilter=dataFilter;
            _currentUser=currentUser;
        }

        /// <summary>
        /// 获取一条
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<AuditLogDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<AuditLog, AuditLogDto>(await AuditLogRepository.GetAsync(id));
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
        /// <param name="id">主键</param>
        /// <returns>测试字符串</returns> 
        public virtual async Task<PagedResultDto<AuditLogDto>> GetListAsync(GetAuditLogDto input)
        {
            var userId =  CurrentUser.Id; 

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

                // 必须先定义映射,然后才能映射对象
                // 将对象映射到自己自定的对象
                List<AuditLogDto> auditLogs = new List<AuditLogDto>();
                ObjectMapper.Map<List<AuditLog>, List<AuditLogDto>>(list, auditLogs);

                return new PagedResultDto<AuditLogDto>(
                    count,
                    // 自动用List<AuditLog> 创建一个List<AuditLogDto>类型的对象
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

    }
}
