using System;
using System.Collections.Generic;
using System.Text;
using Tiger.Localization;
using Volo.Abp.AuditLogging;
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
                L("Permission:AuditLog"));

            // 添加权限
            var aduditLogPermission = auditLogGroup.AddPermission(
                AuditLogPermissions.AuditLogs.Default,
                L("Permission:AuditLog"));

            // 添加子权限
            aduditLogPermission.AddChild(
                AuditLogPermissions.AuditLogs.Create,
                L("Permission:AuditLog.Create"));

            aduditLogPermission.AddChild(
                AuditLogPermissions.AuditLogs.Update,
                L("Permission:AuditLog.Update"));

            aduditLogPermission.AddChild(
                AuditLogPermissions.AuditLogs.Delete,
                L("Permission:AuditLog.Delete"));


            aduditLogPermission.AddChild(
                AuditLogPermissions.AuditLogs.Import,
                L("Permission:AuditLog.Import"));

            aduditLogPermission.AddChild(
                AuditLogPermissions.AuditLogs.Export,
                L("Permission:AuditLog.Export"));

            aduditLogPermission.AddChild(
                AuditLogPermissions.AuditLogs.Audit,
                L("Permission:AuditLog.Audit"));

            aduditLogPermission.AddChild(
                AuditLogPermissions.AuditLogs.Print,
                L("Permission:AuditLog.Print"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<TigerResource>(name);
        }
    }
}
