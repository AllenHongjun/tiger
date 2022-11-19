using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Volo.Abp.AuditLogging
{   
    /// <summary>
    /// 系统日志
    /// </summary>
    public interface IAuditLogAppService : IReadOnlyAppService<AuditLogDto, Guid, GetAuditLogDto>, IDeleteAppService<Guid>
    {

        /// <summary>
        /// 删除多条数据
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task DeleteManyAsync(params Guid[] ids);


        /// <summary>
        /// 每日请求平均执行时间
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        Task<Dictionary<DateTime, double>> GetAverageExecutionDurationPerDayAsync(DateTime startDate, DateTime endDate);
    }
}
