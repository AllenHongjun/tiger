namespace Tiger
{
    public static class TigerConsts
    {
        public const string DbTablePrefix = "App";

        public const string DbTableStockPrefix = "Stock";

        public const string DbSchema = null;
    }

   
    


    public static class IdentitySettingsConsts
    {
        /// <summary>
        /// 密码设置
        /// </summary>
        public static class Password
        {
            /// <summary>
            /// 要求长度
            /// </summary>
            public const string RequiredLength = "Identity.Password.RequiredLength";

            /// <summary>
            /// 密码必须包含唯一字符的数量.
            /// </summary>
            public const string RequiredUniqueChars = "Identity.Password.RequiredUniqueChars";
            public const string RequireNonAlphanumeric = "Identity.Password.RequireNonAlphanumeric";

            /// <summary>
            /// 密码是否包含小写字母
            /// </summary>
            public const string RequireLowercase = "Identity.Password.RequireLowercase";

            /// <summary>
            /// 密码是否包含大写字母
            /// </summary>
            public const string RequireUppercase = "Identity.Password.RequireUppercase";

            /// <summary>
            /// 密码是否包含数字
            /// </summary>
            public const string RequireDigit = "Identity.Password.RequireDigit";
        }

        /// <summary>
        /// 锁定设置
        /// </summary>
        public static class Lockout
        {
            /// <summary>
            /// 允许新用户被锁定.
            /// </summary>
            public const string AllowedForNewUsers = "Identity.Lockout.AllowedForNewUsers";

            /// <summary>
            /// 锁定时间(秒)
            /// </summary>
            public const string LockoutDuration = "Identity.Lockout.LockoutDuration";

            /// <summary>
            /// 如果启用锁定, 当用户被锁定前失败的访问尝试次数.
            /// </summary>
            public const string MaxFailedAccessAttempts = "Identity.Lockout.MaxFailedAccessAttempts";
        }

        /// <summary>
        /// 登录设置
        /// </summary>
        public static class Signin
        {
            /// <summary>
            /// 登录时是否需要验证的电子邮箱.
            /// </summary>
            public const string RequireConfirmedEmail = "Identity.Signin.RequireConfirmedEmail";

            /// <summary>
            /// 用户是否可以确认电话号码
            /// </summary>
            public const string EnablePhoneNumberConfirmation = "Identity.Signin.EnablePhoneNumberConfirmation";

            /// <summary>
            /// 登录时是否需要验证的手机号码.
            /// </summary>
            public const string RequireConfirmedPhoneNumber = "Identity.Signin.RequireConfirmedPhoneNumber";
        }

        /// <summary>
        /// 用户设置
        /// </summary>
        public static class User
        {
            /// <summary>
            /// 是否允许用户更新用户名
            /// </summary>
            public const string IsUserNameUpdateEnabled = "Identity.User.IsUserNameUpdateEnabled";

            /// <summary>
            /// 是否允许用户更新电子邮箱.
            /// </summary>
            public const string IsEmailUpdateEnabled = "Identity.User.IsEmailUpdateEnabled";
        }
    }
}
