using System;
using System.Collections.Generic;
using System.Text;

namespace Tiger.Volo.Abp.Identity.Users
{
    public class AppUserConsts
    {
        /// <summary>
        /// Default value: 1024
        /// </summary>
        public static int MaxAvatarLength { get; set; } = 1024;

        /// <summary>
        /// Default value: 1024
        /// </summary>
        public static int MaxIntroductionLength { get; set; } = 1024;
    }
}
