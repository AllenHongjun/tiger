using System;
using System.Collections.Generic;
using System.Text;

namespace Tiger.Volo.Abp.SettingManagementAppService
{   
    /// <summary>
    /// 身份标识管理
    /// </summary>
    public class IdentitySettingsDto
    {
        public Password password { get; set; }
        public Lockout lockout { get; set; }
        public Signin signIn { get; set; }
        public User user { get; set; }

        
    }

    /// <summary>
    /// 密码设置
    /// </summary>
    public class Password
    {
        /// <summary>
        /// 要求长度
        /// </summary>
        public int requiredLength { get; set; }

        /// <summary>
        /// 密码必须包含唯一字符的数量.
        /// </summary>
        public int requiredUniqueChars { get; set; }
        public bool requireNonAlphanumeric { get; set; }

        /// <summary>
        /// 密码是否包含小写字母
        /// </summary>
        public bool requireLowercase { get; set; }

        /// <summary>
        /// 密码是否包含大写字母
        /// </summary>
        public bool requireUppercase { get; set; }

        /// <summary>
        /// 密码是否包含数字
        /// </summary>
        public bool requireDigit { get; set; }
    }

    /// <summary>
    /// 锁定设置
    /// </summary>
    public class Lockout
    {
        /// <summary>
        /// 允许新用户被锁定.
        /// </summary>
        public bool allowedForNewUsers { get; set; }

        /// <summary>
        /// 锁定时间(秒)
        /// </summary>
        public int lockoutDuration { get; set; }

        /// <summary>
        /// 如果启用锁定, 当用户被锁定前失败的访问尝试次数.
        /// </summary>
        public int maxFailedAccessAttempts { get; set; }
    }

    /// <summary>
    /// 登录设置
    /// </summary>
    public class Signin
    {
        /// <summary>
        /// 登录时是否需要验证的电子邮箱.
        /// </summary>
        public bool requireConfirmedEmail { get; set; }

        /// <summary>
        /// 用户是否可以确认电话号码
        /// </summary>
        public bool enablePhoneNumberConfirmation { get; set; }

        /// <summary>
        /// 登录时是否需要验证的手机号码.
        /// </summary>
        public bool requireConfirmedPhoneNumber { get; set; }
    }

    /// <summary>
    /// 用户设置
    /// </summary>
    public class User
    {
        /// <summary>
        /// 是否允许用户更新用户名
        /// </summary>
        public bool isUserNameUpdateEnabled { get; set; }

        /// <summary>
        /// 是否允许用户更新电子邮箱.
        /// </summary>
        public bool isEmailUpdateEnabled { get; set; }
    }



}
