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

        

        

        public class ReceiptDetail
        {
            public const string Default = GroupName + ".ReceiptDetail";
            public const string Update = Default + ".Update";
            public const string Create = Default + ".Create";
            public const string Delete = Default + ".Delete";
        }

        public class PurchaseHeader
        {
            public const string Default = GroupName + ".PurchaseHeader";
            public const string Update = Default + ".Update";
            public const string Create = Default + ".Create";
            public const string Delete = Default + ".Delete";
        }

        public class PurchaseReturnDetail
        {
            public const string Default = GroupName + ".PurchaseReturnDetail";
            public const string Update = Default + ".Update";
            public const string Create = Default + ".Create";
            public const string Delete = Default + ".Delete";
        }

        public class PurchaseReturnHeader
        {
            public const string Default = GroupName + ".PurchaseReturnHeader";
            public const string Update = Default + ".Update";
            public const string Create = Default + ".Create";
            public const string Delete = Default + ".Delete";
        }

        public class PurchaseDetail
        {
            public const string Default = GroupName + ".PurchaseDetail";
            public const string Update = Default + ".Update";
            public const string Create = Default + ".Create";
            public const string Delete = Default + ".Delete";
        }

        public class OrderReturnDetail
        {
            public const string Default = GroupName + ".OrderReturnDetail";
            public const string Update = Default + ".Update";
            public const string Create = Default + ".Create";
            public const string Delete = Default + ".Delete";
        }
    }
}
