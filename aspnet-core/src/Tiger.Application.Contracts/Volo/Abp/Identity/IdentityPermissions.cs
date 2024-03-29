﻿using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Reflection;

namespace Volo.Abp.Identity
{   
    /// <summary>
    /// 身份认证模块权限常量
    /// </summary>
    public static class TigerIdentityPermissions
    {
        

        public static class Users
        {
            public const string ImportXlsx = IdentityPermissions.Users.Default + ".ImportXlsx";
            public const string ExportXlsx = IdentityPermissions.Users.Default + ".ExportXlsx";
            public const string ResetPassword = IdentityPermissions.Users.Default + ".ResetPassword";
            public const string ManageClaims = IdentityPermissions.Users.Default + ".ManageClaims";
            public const string ManageOrganizationUnits = IdentityPermissions.Users.Default + ".ManageOrganizationUnits";
        }

        public static class Roles
        {
            public const string ImportXlsx = IdentityPermissions.Roles.Default + ".ImportXlsx";
            public const string ExportXlsx = IdentityPermissions.Roles.Default + ".ExportXlsx";
            public const string ManageClaims = IdentityPermissions.Roles.Default + ".ManageClaims";
            public const string ManageOrganizationUnits = IdentityPermissions.Roles.Default + ".ManageOrganizationUnits";
        }

        public static class OrganizationUnits
        {
            public const string Default = IdentityPermissions.GroupName + ".OrganizationUnits";
            public const string Create = Default + ".Create";
            public const string Update = Default + ".Update";
            public const string Delete = Default + ".Delete";
            public const string ManageUsers = Default + ".ManageUsers";
            public const string ManageRoles = Default + ".ManageRoles";
            public const string ManagePermissions = Default + ".ManagePermissions";
        }

        public static class IdentityClaimType
        {
            public const string Default = IdentityPermissions.GroupName + ".IdentityClaimTypes";
            public const string Create = Default + ".Create";
            public const string Update = Default + ".Update";
            public const string Delete = Default + ".Delete";
        }

        public static class IdentitySecurityLog
        {
            public const string Default = IdentityPermissions.GroupName + ".IdentitySecurityLogs";
            public const string Delete  = Default + ".Delete";
        }

        /// <summary>
        /// 获取指定类型（包括基类型）的所有常量值。
        /// </summary>
        /// <returns></returns>
        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(IdentityPermissions));
        }
    }
}
