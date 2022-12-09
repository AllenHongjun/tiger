using System;
using System.Collections.Generic;
using System.Text;

namespace Tiger.Volo.Abp.Identity
{   
    /// <summary>
    /// 安全令牌验证缓存
    /// </summary>
    public class SecurityTokenCacheItem
    {
        public SecurityTokenCacheItem()
        {
        }

        public SecurityTokenCacheItem(string token, string securityToken)
        {
            Token=token;
            SecurityToken=securityToken;
        }

        public string Token { get; set; }


        public string SecurityToken { get; set; }


        /// <summary>
        /// 生产查询key
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <param name="purpose"></param>
        /// <returns></returns>
        public static string CalculateSmsCacheKey(string phoneNumber, string purpose)
        {
            return "Tocp:" + purpose + ";p:" + phoneNumber;
        }

        /// <summary>
        /// 生成查询key
        /// </summary>
        /// <param name="email"></param>
        /// <param name="purpose"></param>
        /// <returns></returns>
        public static string CalculateEmailCacheKey(string email, string purpose)
        {
            return "Tocp:" + purpose + ";p:" + email;
        }

    }
}
