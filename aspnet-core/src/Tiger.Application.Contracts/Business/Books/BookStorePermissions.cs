/**
 * 类    名：BookStorePermissions   
 * 作    者：花生了什么树       
 * 创建时间：2021/8/17 1:37:29       
 * 说    明: 
 * 
 */

using System;
using System.Collections.Generic;
using System.Text;

namespace Tiger.Books
{
    public static class BookStorePermissions
    {
        public const string GroupName = "BookStore";

        public static class Books
        {
            public const string Default = GroupName + ".Books";
            public const string Create = Default + ".Create";
            public const string Edit = Default + ".Edit";
            public const string Delete = Default + ".Delete";
        }

        // *** ADDED a NEW NESTED CLASS ***
        public static class Authors
        {
            public const string Default = GroupName + ".Authors";
            public const string Create = Default + ".Create";
            public const string Edit = Default + ".Edit";
            public const string Delete = Default + ".Delete";
        }
    }
}
