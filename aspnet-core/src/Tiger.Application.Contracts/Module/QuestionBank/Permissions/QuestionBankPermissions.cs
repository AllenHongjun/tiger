using System;
using System.Collections.Generic;
using System.Text;

namespace Tiger.Module.QuestionBank.Permissions
{
    public static class QuestionBankPermissions
    {
        public const string GroupName = "QuestionBank";

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
