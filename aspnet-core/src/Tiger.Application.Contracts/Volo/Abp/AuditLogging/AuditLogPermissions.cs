using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Reflection;

namespace Volo.Abp.AuditLogging
{   
    /// <summary>
    /// 系统日志权限配置
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
            public const string Audit = Default + ".Audit";
            public const string Print = Default + ".Print";
            // 复制，生成，作废，换货，退款 等业务相关的操作有需要单独添加。不然就可以作为修改删除基础权限来管理。
        }

        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(AuditLogPermissions));
        }
    }
}
