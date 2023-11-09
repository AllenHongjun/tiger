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
    /// 试卷
    /// </summary>
    public class TestPaper
    {
        public const string Default = GroupName + ".TestPaper";
        public const string Update = Default + ".Update";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }
    
    /// <summary>
    /// 组卷策略配置表
    /// </summary>
    public class TestPaperStrategy
    {
        public const string Default = GroupName + ".TestPaperStrategy";
        public const string Update = Default + ".Update";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }
    /// <summary>
    /// 试卷内容(题目)表
    /// </summary>
    public class TestPaperQuestion
    {
        public const string Default = GroupName + ".TestPaperQuestion";
        public const string Update = Default + ".Update";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }
    /// <summary>
    /// 考试
    /// </summary>
    public class Exam
    {
        public const string Default = GroupName + ".Exam";
        public const string Update = Default + ".Update";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }
    /// <summary>
    /// 答卷表
    /// </summary>
    public class AnswerSheet
    {
        public const string Default = GroupName + ".AnswerSheet";
        public const string Update = Default + ".Update";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }
    /// <summary>
    /// 答卷明细表
    /// </summary>
    public class AnswerSheetDetail
    {
        public const string Default = GroupName + ".AnswerSheetDetail";
        public const string Update = Default + ".Update";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }
    }
}
