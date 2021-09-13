using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Reflection;

namespace Tiger.Basic.Products
{
    public static class PurchasePermissions
    {
        public const string GroupName = "Purchase";

        public static class Purchase
        {
            public const string Default = GroupName + ".PurchaseManage";
            public const string Create = Default + ".Create";
            public const string Update = Default + ".Update";
            public const string Delete = Default + ".Delete";
            public const string ManagePermissions = Default + ".ManagePermissions";
        }

        public static class PurchaseReturn
        {
            public const string Default = GroupName + ".PurchaseReturn";
            public const string Create = Default + ".Create";
            public const string Update = Default + ".Update";
            public const string Delete = Default + ".Delete";
            public const string ManagePermissions = Default + ".ManagePermissions";
        }

        

        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(BasicPermissions));
        }
    }
}
