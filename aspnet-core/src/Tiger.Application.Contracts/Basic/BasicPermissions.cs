using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Reflection;

namespace Tiger.Basic.Products
{
    public static class BasicPermissions
    {
        public const string GroupName = "Basic";

        public static class Product
        {
            public const string Default = GroupName + ".Products";
            public const string Create = Default + ".Create";
            public const string Update = Default + ".Update";
            public const string Delete = Default + ".Delete";
            public const string ManagePermissions = Default + ".ManagePermissions";
        }

        public static class ProductAttribute
        {
            public const string Default = GroupName + ".ProductAttributes";
            public const string Create = Default + ".Create";
            public const string Update = Default + ".Update";
            public const string Delete = Default + ".Delete";
            public const string ManagePermissions = Default + ".ManagePermissions";
        }

        public static class Category
        {
            public const string Default = GroupName + ".Categorys";
            public const string Create = Default + ".Create";
            public const string Update = Default + ".Update";
            public const string Delete = Default + ".Delete";
            public const string ManagePermissions = Default + ".ManagePermissions";
        }

        public static class UserLookup
        {
            public const string Default = GroupName + ".UserLookup";
        }

        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(BasicPermissions));
        }
    }
}
