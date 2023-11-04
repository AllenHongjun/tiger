namespace Tiger.Permissions
{
    public static class TigerPermissions
    {
        public const string GroupName = "Tiger";

        //Add your own permission names. Example:
        //public const string MyPermission1 = GroupName + ".MyPermission1";

    /// <summary>
    /// 学校管理
    /// </summary>
    public class School
    {
        public const string Default = GroupName + ".School";
        public const string Update = Default + ".Update";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }
    }
}
