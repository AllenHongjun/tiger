using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Reflection;

namespace Volo.Abp.AuditLogging
{   
    /// <summary>
    /// 审计日志权限配置
    /// </summary>
    public static class AuditLogPermissions
    {
        public const string GroupName = "Auditing";
        public static class AuditLogs
        {
            public const string Default = GroupName + ".AuditingLog";
            
            public const string Create = Default + ".Create";
            public const string Update = Default + ".Update";
            public const string Delete = Default + ".Delete";
            public const string ManagePermissions = Default + ".ManagePermissions";

            public const string Import = Default + ".Import";
            public const string Export = Default + ".Export";
            public const string Audit  = Default + ".Audit";
            public const string Print  = Default + ".Print";
        }

        public static class AuditLogActions
        {
            public const string Default = GroupName + ".AuditingLogAction";
            public const string Delete = Default + ".Delete";
        }

        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(AuditLogPermissions));
        }
    }
}
