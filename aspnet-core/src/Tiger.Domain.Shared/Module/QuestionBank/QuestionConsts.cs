using System;
using System.Collections.Generic;
using System.Text;

namespace Tiger.Module.QuestionBank
{
    public static class QuestionConsts
    {
        public static int MaxNameLength { get; set; } = 1024;

        public static int MaxOptionLength { get; set; } = 512;

        public static int MaxAnalysisLength { get; set; } = 512;

        public static int MaxSourceLength { get; set; } = 256;
        public static int MaxHelpMessageLength { get; set; } = 256;
        public static int MaxHelpVideoLength { get; set; } = 512;
        public static int MaxFileUrlLength { get; set; } = 512;
        public static int MaxFileNameLength { get; set; } = 256;
    }
}
