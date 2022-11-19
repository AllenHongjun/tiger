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
    public interface IAuditLogAppService : IReadOnlyAppService<AuditLogDto, Guid, GetAuditLogDto>, IDeleteAppService<Guid>
    {

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
