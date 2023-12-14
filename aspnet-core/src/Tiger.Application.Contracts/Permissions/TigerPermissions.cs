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
    /// <summary>
    /// 考试时间调整表
    /// </summary>
    public class ExamModifyTime
    {
        public const string Default = GroupName + ".ExamModifyTime";
        public const string Update = Default + ".Update";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }
    /// <summary>
    /// 考试人员表(应试人；参加考试者)
    /// </summary>
    public class Examinee
    {
        public const string Default = GroupName + ".Examinee";
        public const string Update = Default + ".Update";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }
    /// <summary>
    /// 试卷评委表        <remarks>    评卷人只有关联了试卷才能改卷（默认只有超管能改卷）    </remarks>
    /// </summary>
    public class TestPaperJudge
    {
        public const string Default = GroupName + ".TestPaperJudge";
        public const string Update = Default + ".Update";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }
    }
}
