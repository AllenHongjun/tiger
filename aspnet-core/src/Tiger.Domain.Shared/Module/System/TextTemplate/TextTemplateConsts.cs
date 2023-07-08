using System;
using System.Collections.Generic;
using System.Text;

namespace Tiger.Module.System.TextTemplate
{
    /// <summary>
    /// 文本模板常量
    /// </summary>
    public static class TextTemplateConsts
    {
        public static int MaxNameLength { get; set; } = 100;
        public static int MaxDisplayNameLength { get; set; } = 100;
        public static int MaxContentLength { get; set; } = 1024 * 1024;
        public static int MaxCultureLength { get; set; } = 30;
    }
}
