using System;
using System.Collections.Generic;
using System.Text;
using Tiger.Localization;
using Volo.Abp.AuditLogging;
using Volo.Abp.AuditLogging.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Tiger.Volo.Abp.AuditLogging
{
    /// <summary>
    /// 定义审计模块权限
    /// </summary>
    /// <remarks>
    /// Abp会自动发现这个类 不需要注册
    /// 权限系统是为特定用户,角色或客户端授权或禁止的简单策略.
    /// </remarks>
    public class AuditingPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            //添加权限分组
            var auditLogGroup = context.AddGroup(
                AuditLogPermissions.GroupName,
                L("Permission:Auditing"));

            // 添加权限
            var aduditLogPermission = auditLogGroup.AddPermission(
                AuditLogPermissions.AuditLogs.Default,
                L("Permission:AuditLog"));

            aduditLogPermission.AddChild(
                AuditLogPermissions.AuditLogs.Delete,
                L("Permission:AuditLog.Delete"));

            aduditLogPermission.AddChild(
                AuditLogPermissions.AuditLogs.Export,
                L("Permission:AuditLog.Export"));

            //aduditLogPermission.AddChild(
            //    AuditLogPermissions.AuditLogs.ManagePermissions,
            //    L("Permission:AuditLog.AuditLogManagement"));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private static LocalizableString L(string name)
        {   
            // 需要使用对应的的汉化资源方法
            return LocalizableString.Create<AuditLoggingResource>(name);
        }
    }
}
