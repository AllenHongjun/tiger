using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Reflection;

namespace Tiger.Permissions
{
    public static class StockPermissions
    {
        public const string GroupName = "Stock";

        public class Inventory
        {
            public const string Default = GroupName + ".Inventory";
            public const string Update = Default + ".Update";
            public const string Create = Default + ".Create";
            public const string Delete = Default + ".Delete";
        }

        public class InventoryHistory
        {
            public const string Default = GroupName + ".InventoryHistory";
            public const string Update = Default + ".Update";
            public const string Create = Default + ".Create";
            public const string Delete = Default + ".Delete";
        }

        public class CheckHeader
        {
            public const string Default = GroupName + ".CheckHeader";
            public const string Update = Default + ".Update";
            public const string Create = Default + ".Create";
            public const string Delete = Default + ".Delete";
        }

        public class CheckDetail
        {
            public const string Default = GroupName + ".CheckDetail";
            public const string Update = Default + ".Update";
            public const string Create = Default + ".Create";
            public const string Delete = Default + ".Delete";
        }

        public class ReceiptHeader
        {
            public const string Default = GroupName + ".ReceiptHeader";
            public const string Update = Default + ".Update";
            public const string Create = Default + ".Create";
            public const string Delete = Default + ".Delete";
        }

        public class ReverseDetail
        {
            public const string Default = GroupName + ".ReverseDetail";
            public const string Update = Default + ".Update";
            public const string Create = Default + ".Create";
            public const string Delete = Default + ".Delete";
        }

        public class ShipmentHeader
        {
            public const string Default = GroupName + ".ShipmentHeader";
            public const string Update = Default + ".Update";
            public const string Create = Default + ".Create";
            public const string Delete = Default + ".Delete";
        }

        public class ShipmentDetail
        {
            public const string Default = GroupName + ".ShipmentDetail";
            public const string Update = Default + ".Update";
            public const string Create = Default + ".Create";
            public const string Delete = Default + ".Delete";
        }

        public class TransferHeader
        {
            public const string Default = GroupName + ".TransferHeader";
            public const string Update = Default + ".Update";
            public const string Create = Default + ".Create";
            public const string Delete = Default + ".Delete";
        }

        public class TransferDetail
        {
            public const string Default = GroupName + ".TransferDetail";
            public const string Update = Default + ".Update";
            public const string Create = Default + ".Create";
            public const string Delete = Default + ".Delete";
        }



        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(StockPermissions));
        }
    }
}
