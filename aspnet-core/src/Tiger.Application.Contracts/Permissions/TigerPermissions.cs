namespace Tiger.Permissions
{
    public static class TigerPermissions
    {
        public const string GroupName = "Tiger";

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

        public class Warehouse
        {
            public const string Default = GroupName + ".Warehouse";
            public const string Update = Default + ".Update";
            public const string Create = Default + ".Create";
            public const string Delete = Default + ".Delete";
        }

        public class Supply
        {
            public const string Default = GroupName + ".Supply";
            public const string Update = Default + ".Update";
            public const string Create = Default + ".Create";
            public const string Delete = Default + ".Delete";
        }

        public class Store
        {
            public const string Default = GroupName + ".Store";
            public const string Update = Default + ".Update";
            public const string Create = Default + ".Create";
            public const string Delete = Default + ".Delete";
        }

        public class Sku
        {
            public const string Default = GroupName + ".Sku";
            public const string Update = Default + ".Update";
            public const string Create = Default + ".Create";
            public const string Delete = Default + ".Delete";
        }

        public class FlashPromotion
        {
            public const string Default = GroupName + ".FlashPromotion";
            public const string Update = Default + ".Update";
            public const string Create = Default + ".Create";
            public const string Delete = Default + ".Delete";
        }

        public class FlashPromotionSession
        {
            public const string Default = GroupName + ".FlashPromotionSession";
            public const string Update = Default + ".Update";
            public const string Create = Default + ".Create";
            public const string Delete = Default + ".Delete";
        }

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

        public class Member
        {
            public const string Default = GroupName + ".Member";
            public const string Update = Default + ".Update";
            public const string Create = Default + ".Create";
            public const string Delete = Default + ".Delete";
        }

        public class MemberLevel
        {
            public const string Default = GroupName + ".MemberLevel";
            public const string Update = Default + ".Update";
            public const string Create = Default + ".Create";
            public const string Delete = Default + ".Delete";
        }

        public class MemberLoginLog
        {
            public const string Default = GroupName + ".MemberLoginLog";
            public const string Update = Default + ".Update";
            public const string Create = Default + ".Create";
            public const string Delete = Default + ".Delete";
        }

        public class MemberReceiveAddress
        {
            public const string Default = GroupName + ".MemberReceiveAddress";
            public const string Update = Default + ".Update";
            public const string Create = Default + ".Create";
            public const string Delete = Default + ".Delete";
        }

        public class MemberStatisticInfo
        {
            public const string Default = GroupName + ".MemberStatisticInfo";
            public const string Update = Default + ".Update";
            public const string Create = Default + ".Create";
            public const string Delete = Default + ".Delete";
        }
    }
}
