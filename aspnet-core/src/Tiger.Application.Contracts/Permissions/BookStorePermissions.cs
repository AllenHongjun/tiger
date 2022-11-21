using System;
using System.Collections.Generic;
using System.Text;

namespace Tiger.Books
{
    /// <summary>
    /// 书店权限管理
    /// </summary>
    /// <remarks>
    /// ASP.NET Core 中的授权简介   https://learn.microsoft.com/zh-cn/aspnet/core/security/authorization/introduction?view=aspnetcore-7.0
    /// 
    /// 权限名称配置
    /// 权限必须有唯一的名称 (一个 `字符串`). 最好的方法是把它定义为一个 `常量`, 这样我们就可以重用这个权限名称了
    /// 权限名称具有层次结构. 例如, "创建图书" 权限被定义为 `BookStore.Books.Create`. 
    /// </remarks>
    public static class BookStorePermissions
    {
        public const string GroupName = "BookStore";

        public static class Books
        {
            public const string Default = GroupName + ".Books";
            public const string Create = Default + ".Create";
            public const string Update = Default + ".Update";
            public const string Delete = Default + ".Delete";
        }

        // *** ADDED a NEW NESTED CLASS ***
        public static class Authors
        {
            public const string Default = GroupName + ".Authors";
            public const string Create = Default + ".Create";
            public const string Update = Default + ".Update";
            public const string Delete = Default + ".Delete";
        }
    }
}
