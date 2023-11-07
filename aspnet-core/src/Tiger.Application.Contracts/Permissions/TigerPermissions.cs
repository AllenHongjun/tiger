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
    /// 题目分类
    /// </summary>
    public class QuestionCategory
    {
        public const string Default = GroupName + ".QuestionCategory";
        public const string Update = Default + ".Update";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }
    /// <summary>
    /// 题目表
    /// </summary>
    public class Question
    {
        public const string Default = GroupName + ".Question";
        public const string Update = Default + ".Update";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }
    /// <summary>
    /// 题目附件表
    /// </summary>
    public class QuestionAttachment
    {
        public const string Default = GroupName + ".QuestionAttachment";
        public const string Update = Default + ".Update";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }
    /// <summary>
    /// 题目答案
    /// </summary>
    public class QuestionAnswer
    {
        public const string Default = GroupName + ".QuestionAnswer";
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
    }
}
