﻿namespace TigerAdmin.Permissions
{
    public static class TigerAdminPermissions
    {
        public const string GroupName = "TigerAdmin";

        //Add your own permission names. Example:
        //public const string MyPermission1 = GroupName + ".MyPermission1";
        public static class Books
        {
            //权限名称定义 BookStore.Books.Create
            public const string Default = GroupName + ".Books";
            public const string Create = Default + ".Create";
            public const string Edit = Default + ".Edit";
            public const string Delete = Default + ".Delete";
        }

        // 权限使用。权限直接定义在这个类当中。 使用的权限只要继承ABP提供的类就可以了。
    }
}