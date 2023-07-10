using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tiger.Utils
{   
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// 这个静态方法 如果别的模块会依赖 会增加耦合性
    /// </remarks>
    public static class CodeNumberGenerator
    {
        //public static string CreateCode(params int[] numbers)
        //{
        //    if (numbers.IsNullOrEmpty())
        //    {
        //        return null;
        //    }

        //    return numbers.Select(number => number.ToString(new string(PlatformConsts.CodePrefix, PlatformConsts.CodeUnitLength))).JoinAsString(".");
        //}

        public static string AppendCode(string parentCode, string childCode)
        {
            if (childCode.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(childCode), "childCode can not be null or empty.");
            }

            if (parentCode.IsNullOrEmpty())
            {
                return childCode;
            }

            return parentCode + "." + childCode;
        }

        public static string GetRelativeCode(string code, string parentCode)
        {
            if (code.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(code), "code can not be null or empty.");
            }

            if (parentCode.IsNullOrEmpty())
            {
                return code;
            }

            if (code.Length == parentCode.Length)
            {
                return null;
            }

            return code.Substring(parentCode.Length + 1);
        }

        //public static string CalculateNextCode(string code)
        //{
        //    if (code.IsNullOrEmpty())
        //    {
        //        throw new ArgumentNullException(nameof(code), "code can not be null or empty.");
        //    }

        //    var parentCode = GetParentCode(code);
        //    var lastUnitCode = GetLastCode(code);

        //    return AppendCode(parentCode, CreateCode(Convert.ToInt32(lastUnitCode) + 1));
        //}

        public static string GetLastCode(string code)
        {
            if (code.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(code), "code can not be null or empty.");
            }

            var splittedCode = code.Split('.');
            return splittedCode[splittedCode.Length - 1];
        }

        public static string GetParentCode(string code)
        {
            if (code.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(code), "code can not be null or empty.");
            }

            var splittedCode = code.Split('.');
            if (splittedCode.Length == 1)
            {
                return null;
            }

            return splittedCode.Take(splittedCode.Length - 1).JoinAsString(".");
        }
    }

}
