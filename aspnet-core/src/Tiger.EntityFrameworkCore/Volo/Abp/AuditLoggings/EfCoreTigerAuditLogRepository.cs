//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;
//using Volo.Abp.AuditLogging.EntityFrameworkCore;
//using Volo.Abp.EntityFrameworkCore;

//namespace Tiger.Volo.Abp.AuditLoggings
//{
//    public class TigerEfCoreAuditLogRepository<IAuditLoggingDbContext, AuditLog, Guid> : EfCoreAuditLogRepository
//    {
//        public TigerEfCoreAuditLogRepository(IDbContextProvider<global::Volo.Abp.AuditLogging.EntityFrameworkCore.IAuditLoggingDbContext> dbContextProvider) : base(dbContextProvider)
//        {
//        }



//        public virtual async Task<List<AuditLog>> GetListAsync(
//        string sorting = null,
//        int maxResultCount = 50,
//        int skipCount = 0,
//        DateTime? startTime = null,
//        DateTime? endTime = null,
//        string httpMethod = null,
//        string url = null,
//        Guid? userId = null,
//        string userName = null,
//        string applicationName = null,
//        string clientIpAddress = null,
//        string correlationId = null,
//        int? maxExecutionDuration = null,
//        int? minExecutionDuration = null,
//        bool? hasException = null,
//        HttpStatusCode? httpStatusCode = null,
//        bool includeDetails = false,
//        CancellationToken cancellationToken = default)
//        {
//            var query = await GetListQueryAsync(
//                startTime,
//                endTime,
//                httpMethod,
//                url,
//                userId,
//                userName,
//                applicationName,
//                clientIpAddress,
//                correlationId,
//                maxExecutionDuration,
//                minExecutionDuration,
//                hasException,
//                httpStatusCode,
//                includeDetails
//            );

//            var auditLogs = await query
//                .OrderBy(sorting.IsNullOrWhiteSpace() ? (nameof(AuditLog.ExecutionTime) + " DESC") : sorting)
//                .PageBy(skipCount, maxResultCount)
//                .ToListAsync(GetCancellationToken(cancellationToken));

//            return auditLogs;
//        }


//        protected virtual async Task<IQueryable<AuditLog>> GetListQueryAsync(
//        DateTime? startTime = null,
//        DateTime? endTime = null,
//        string httpMethod = null,
//        string url = null,
//        Guid? userId = null,
//        string userName = null,
//        string applicationName = null,
//        string clientIpAddress = null,
//        string correlationId = null,
//        int? maxExecutionDuration = null,
//        int? minExecutionDuration = null,
//        bool? hasException = null,
//        HttpStatusCode? httpStatusCode = null,
//        bool includeDetails = false)
//        {
//            var nHttpStatusCode = (int?)httpStatusCode;
//            return await DbSet
//                //.IncludeDetails(includeDetails)
//                .WhereIf(startTime.HasValue, auditLog => auditLog.ExecutionTime >= startTime)
//                .WhereIf(endTime.HasValue, auditLog => auditLog.ExecutionTime <= endTime)
//                .WhereIf(hasException.HasValue && hasException.Value, auditLog => auditLog.Exceptions != null && auditLog.Exceptions != "")
//                .WhereIf(hasException.HasValue && !hasException.Value, auditLog => auditLog.Exceptions == null || auditLog.Exceptions == "")
//                .WhereIf(httpMethod != null, auditLog => auditLog.HttpMethod == httpMethod)
//                .WhereIf(url != null, auditLog => auditLog.Url != null && auditLog.Url.Contains(url))
//                .WhereIf(userId != null, auditLog => auditLog.UserId == userId)
//                .WhereIf(userName != null, auditLog => auditLog.UserName == userName)
//                .WhereIf(applicationName != null, auditLog => auditLog.ApplicationName == applicationName)
//                .WhereIf(clientIpAddress != null, auditLog => auditLog.ClientIpAddress != null && auditLog.ClientIpAddress == clientIpAddress)
//                .WhereIf(correlationId != null, auditLog => auditLog.CorrelationId == correlationId)
//                .WhereIf(httpStatusCode != null && httpStatusCode > 0, auditLog => auditLog.HttpStatusCode == nHttpStatusCode)
//                .WhereIf(maxExecutionDuration != null && maxExecutionDuration.Value > 0, auditLog => auditLog.ExecutionDuration <= maxExecutionDuration)
//                .WhereIf(minExecutionDuration != null && minExecutionDuration.Value > 0, auditLog => auditLog.ExecutionDuration >= minExecutionDuration);
//        }

//    }
//}
