using System;
using System.Collections.Generic;
using System.Text;

namespace Tiger.Module.QuestionBank
{
    /// <summary>
    /// Token传值方式：0、使用旧版Cookie；1、使用旧版Url Token；2、使用新版Url Token；
    /// </summary>
    public enum TokenType
    {
        /// <summary>
        /// 使用旧版Cookie
        /// </summary>
        Cookie = 0,

        /// <summary>
        /// 使用旧版Url
        /// </summary>
        Url = 1,

        /// <summary>
        /// 使用新版Url
        /// </summary>
        NewUrl = 2,
    }
}
