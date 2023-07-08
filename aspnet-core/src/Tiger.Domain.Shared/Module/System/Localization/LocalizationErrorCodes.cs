using System;
using System.Collections.Generic;
using System.Text;

namespace Tiger.Module.System.Localization
{   
    /// <summary>
    /// 本地化错误码
    /// </summary>
    public class LocalizationErrorCodes
    {
        public const string CultureNotValid = LocalizationConsts.NameSpace + ":100001";

        public const string ResourceNotFound = LocalizationConsts.NameSpace + ":100002";
        public const string LanguageNotFound = LocalizationConsts.NameSpace + ":100003";
        public const string LanguageExist = LocalizationConsts.NameSpace + ":100004";
        public const string LanguageTextNotFound = LocalizationConsts.NameSpace + ":100005";
        public const string LanguageTextExist = LocalizationConsts.NameSpace + ":100006";
    }
}
