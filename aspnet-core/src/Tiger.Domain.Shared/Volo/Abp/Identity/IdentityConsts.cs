using System;
using System.Collections.Generic;
using System.Text;

namespace Tiger.Volo.Abp.Identity
{   
    /// <summary>
    /// 身份模块常量
    /// </summary>
    public static class IdentityConsts
    {   
        /// <summary>
        /// 声明类型
        /// </summary>
        public static class ClaimType
        {
            public static class Avatar
            {
                public static string Name { get; set; }

                public static string DisplayName { get; set; }

                public static string Description { get; set; }

            }
        }

    }
}
