using System;
using System.Collections.Generic;
using System.Text;

namespace Tiger.Module.TaskManagement
{
    public class BackgroundJobLogConsts
    {
        public static int MaxJobIdLength { get; set; } = 255;

        public static int MaxMessageLength { get; set;} = 1000;

        public static int MaxExceptionLength { get; set; } = 2000;
    }
}
