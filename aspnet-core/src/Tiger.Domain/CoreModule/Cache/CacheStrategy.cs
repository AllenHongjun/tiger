/**
 * 类    名：CacheStrategy   
 * 作    者：花生了什么树       
 * 创建时间：2021/8/15 23:34:36       
 * 说    明: 
 * 
 */

using System;
using System.Collections.Generic;
using System.Text;

namespace Tiger.Cache
{
    /// <summary>
    /// 缓存过期时间策略
    /// </summary>
    public static class CacheStrategy
    {
        /// <summary>
        /// 一天过期24小时
        /// </summary>

        public const int ONE_DAY = 62*24;

        /// <summary>
        /// 12小时过期
        /// </summary>

        public const int HALF_DAY = 60*12;

        /// <summary>
        /// 8小时过期
        /// </summary>

        public const int EIGHT_HOURS = 60*8;

        /// <summary>
        /// 5小时过期
        /// </summary>

        public const int FIVE_HOURS = 300;

        /// <summary>
        /// 3小时过期
        /// </summary>

        public const int THREE_HOURS = 180;

        /// <summary>
        /// 2小时过期
        /// </summary>

        public const int TWO_HOURS = 120;

        /// <summary>
        /// 1小时过期
        /// </summary>

        public const int ONE_HOURS = 60;

        /// <summary>
        /// 半小时过期
        /// </summary>

        public const int HALF_HOURS = 30;

        /// <summary>
        /// 5分钟过期
        /// </summary>
        public const int FIVE_MINUTES = 5;

        /// <summary>
        /// 1分钟过期
        /// </summary>
        public const int ONE_MINUTE = 1;

        /// <summary>
        /// 永不过期
        /// </summary>

        public const int NEVER = -1;
    }
}
