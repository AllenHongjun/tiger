using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Reflection;

namespace Tiger.Basic.Products
{
    public static class OrderPermissions
    {
        public const string GroupName = "Order";

        public static class Order
        {
            public const string Default = GroupName + ".OrderManage";
            public const string Create = Default + ".Create";
            public const string Update = Default + ".Update";
            public const string Delete = Default + ".Delete";
            public const string ManagePermissions = Default + ".ManagePermissions";
        }

        public static class OrderReturn
        {
            public const string Default = GroupName + ".OrderReturn";
            public const string Create = Default + ".Create";
            public const string Update = Default + ".Update";
            public const string Delete = Default + ".Delete";
            public const string ManagePermissions = Default + ".ManagePermissions";
        }

        //public static class OrderOperateHistorys
        //{
        //    public const string Default = GroupName + ".OrderOperateHistorys";
        //    public const string Create = Default + ".Create";
        //    public const string Update = Default + ".Update";
        //    public const string Delete = Default + ".Delete";
        //    public const string ManagePermissions = Default + ".ManagePermissions";
        //}

        public static class OrderSetting
        {
            public const string Default = GroupName + ".OrderSetting";
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
