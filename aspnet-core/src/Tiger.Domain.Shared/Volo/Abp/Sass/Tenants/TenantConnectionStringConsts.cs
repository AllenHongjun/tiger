using System;
using System.Collections.Generic;
using System.Text;

namespace Tiger.Volo.Abp.Sass.Tenants
{   
     /// <summary>
     /// 租户数据库连接字符串
     /// </summary>
    public class TenantConnectionStringConsts
    {   
        /// <summary>
        /// 名称最大长度限制
        /// </summary>
        public static int MaxNameLength { get; set; } = 64;

        /// <summary>
        /// 连接字符串最大长度限制
        /// </summary>
        public static int MaxValueLength { get; set; } = 1024;
    }
}
