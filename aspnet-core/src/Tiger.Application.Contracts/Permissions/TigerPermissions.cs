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
    /// <summary>
    /// 课程
    /// </summary>
    public class Course
    {
        public const string Default = GroupName + ".Course";
        public const string Update = Default + ".Update";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }
    
    /// <summary>
    /// 试卷大题
    /// </summary>
    public class TestPaperSection
    {
        public const string Default = GroupName + ".TestPaperSection";
        public const string Update = Default + ".Update";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }
    /// <summary>
    /// 实训平台
    /// </summary>
    public class TrainPlatform
    {
        public const string Default = GroupName + ".TrainPlatform";
        public const string Update = Default + ".Update";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }
    }
}
