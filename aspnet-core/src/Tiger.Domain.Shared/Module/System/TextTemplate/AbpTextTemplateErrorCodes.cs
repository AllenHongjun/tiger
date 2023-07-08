using System;
using System.Collections.Generic;
using System.Text;

namespace Tiger.Module.System.TextTemplate
{   
    /// <summary>
    /// 文本模板错误码
    /// </summary>
    public static class AbpTextTemplateErrorCodes
    {
        public const string Namespace = "TextTemplate";

        /// <summary>
        /// 模板不存在
        /// </summary>
        public const string TemplateNotFound = Namespace + ":01404";
    }
}
