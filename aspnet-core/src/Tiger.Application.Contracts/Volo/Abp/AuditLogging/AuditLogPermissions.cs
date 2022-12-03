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
        public const string GroupName = "AbpAuditLogging";
        public static class AuditLogs
        {
            public const string Default = GroupName + ".Default";

            public const string Create = Default + ".Create";
            public const string Update = Default + ".Update";
            public const string Delete = Default + ".Delete";
            public const string Import = Default + ".Import";
            public const string Export = Default + ".Export";
            public const string Audit  = Default + ".Audit";
            public const string Print  = Default + ".Print";
        }

        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(AuditLogPermissions));
        }
    }
}
