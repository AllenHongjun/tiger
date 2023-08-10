using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Scriban;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tiger.Volo.Abp.Account.Dto;
using Tiger.Volo.Abp.Identity;
using Tiger.Volo.Abp.Identity.Security;
using Tiger.Volo.Abp.Identity.Settings;
using TigerAdmin.Volo.Abp.Account;
using Volo.Abp;
using Volo.Abp.Account;
using Volo.Abp.Account.Emailing;
using Volo.Abp.AuditLogging;
using Volo.Abp.Caching;
using Volo.Abp.Identity;
using Volo.Abp.Settings;
using Volo.Abp.Sms;
using Volo.Abp.Users;
using Volo.Abp.Validation;
using IdentityUser = Volo.Abp.Identity.IdentityUser;

namespace Tiger.Volo.Abp.Account
{
    /// <summary>
    /// 用户账号管理
    /// </summary>
    [RemoteService(false)]
    public class TigerAccountAppService : AccountAppService, ITigerAccountAppService
    {
        //protected IOptions<IdentityOptions> IdentityOptions { get { 
        //    return LazyGetRequiredService<IOptions<IdentityOptions>>();
        //    } }

        //protected IdentityUserStore UserStore => LazyServiceProvider.LazyGetRequiredService<IdentityUserStore>();
        //protected IdentityUserManager UserManager => LazyServiceProvider.LazyGetRequiredService<IdentityUserManager>();


        protected IdentityUserStore UserStore { get; }

        protected IOptions<IdentityOptions> IdentityOptions { get; }

        // 生产验证码服务
        protected ITotpService TotpService { get; }

        protected ISmsSender SmsSender { get; }

        protected IAuditLogRepository AuditLogRepository { get; }
        protected ITigerIdentityUserRepository UserRepository { get; }
        protected IDistributedCache<SecurityTokenCacheItem> SecurityTokenCache { get; }


        //protected IAbpApiDefinition

        //protected IApplicationConfiguration

        public TigerAccountAppService(
            IdentityUserManager userManager,
            IIdentityRoleRepository roleRepository,
            IAccountEmailer accountEmailer,
            IdentitySecurityLogManager identitySecurityLogManager
,
            IDistributedCache<SecurityTokenCacheItem> securityTokenCache,
            IAuditLogRepository auditLogRepository,
            ITigerIdentityUserRepository userRepository,
            ITotpService totpService,
            ISmsSender smsSender,
            IdentityUserStore userStore) : base(userManager, roleRepository, accountEmailer, identitySecurityLogManager)
        {
            SecurityTokenCache=securityTokenCache;
            AuditLogRepository=auditLogRepository;
            UserRepository=userRepository;
            TotpService=totpService;
            SmsSender=smsSender;
            UserStore=userStore;
        }


        protected async virtual Task SendSmsCodeAsync(
            string phone,
            string token,
            string template,
            CancellationToken cancellation = default)
        {
            Check.NotNullOrWhiteSpace(template, nameof(template));

            var smsMessage = new SmsMessage(phone, token);
            smsMessage.Properties.Add("code", token);
            smsMessage.Properties.Add("TemplateCode", template);

            await SmsSender.SendAsync(smsMessage);
        }

        /// <summary>
        /// 通过用户名和邮箱注册账号
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public override async Task<IdentityUserDto> RegisterAsync(RegisterDto input)
        {
            return await base.RegisterAsync(input);
        }


        public override async Task SendPasswordResetCodeAsync(SendPasswordResetCodeDto input)
        {
            await base.SendPasswordResetCodeAsync(input);
        }

        /// <summary>
        /// 通过手机号注册
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task RegisterAsync(PhoneRegisterDto input)
        {
            await CheckSelfRegistrationAsync();
            //await IdentityOptions.SetAsync();
            await CheckNewUserPhoneNumberNotBeUsedAsync(input.PhoneNumber);


            // 判断验证码是否过期
            var securityTokenCacheKey = SecurityTokenCacheItem.CalculateSmsCacheKey(input.PhoneNumber, "SmsVerifyCode");
            var securityTokenCacheItem = await SecurityTokenCache.GetAsync(securityTokenCacheKey);
            if (securityTokenCacheItem == null)
            {
                throw new UserFriendlyException(L["InvalidVerifyCode"]);
            }


            if (input.Code.Equals(securityTokenCacheItem.Token) && int.TryParse(input.Code, out int token))
            {
                var securityToken = Encoding.Unicode.GetBytes(securityTokenCacheItem.SecurityToken);

                if (TotpService.ValidateCode(securityToken, token, securityTokenCacheKey))
                {

                    // 验证通过注册创建用户

                    var userEmail = input.EmailAddress ?? string.Empty;
                    //用户名必须添加 如果为空 就用手机号
                    var userName = input.UserName ?? input.PhoneNumber;
                    var user = new IdentityUser(GuidGenerator.Create(), userName, userEmail,
                        CurrentTenant.Id)
                    {
                        Name = input.Name ?? input.PhoneNumber
                    };

                    await UserStore.SetPhoneNumberAsync(user, input.PhoneNumber);

                    //如果用户验证码验证标记已经验证
                    await UserStore.SetPhoneNumberConfirmedAsync(user, true);

                    (await UserManager.CreateAsync(user, input.Password)).CheckErrors();

                    //给用户添加默认的角色
                    (await UserManager.AddDefaultRolesAsync(user)).CheckErrors();

                    //注册完成清楚验证码的缓存
                    await SecurityTokenCache.RemoveAsync(securityTokenCacheKey);


                    //记录安全日志
                    await IdentitySecurityLogManager.SaveAsync(
                        new IdentitySecurityLogContext
                        {
                            Action = "PhoneNumberRegister",
                            //ClientId = await FindClientIdAsync(),
                            Identity = "Account",
                            UserName = user.UserName
                        });

                    await CurrentUnitOfWork.SaveChangesAsync();
                    return;
                }

            }

            // 验证码无效
            throw new UriFormatException(L["InvalidVerifyCode"]);
        }

        
        /// <summary>
        /// 通过手机验证码重置密码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="UserFriendlyException"></exception>
        /// <exception cref="NotImplementedException"></exception>
        public async Task ResetPasswordAsync(PhoneResetPasswordDto input)
        {
            var securityTokenCacheKey = SecurityTokenCacheItem.CalculateSmsCacheKey(input.PhoneNumber,
                "SmsVerifyCode");

            var securityTokenCacheItem = await SecurityTokenCache.GetAsync(securityTokenCacheKey);

            if (securityTokenCacheItem == null)
            {
                throw new UserFriendlyException(L["InvalidVerifyCode"]);
            }

            //await IdentityOptions.SetAsync();

            var user = await GetUserByPhoneNumberAsync(input.PhoneNumber, isConfirmed: true);

            //外部认证的用户不允许修改密码（外部认证？？）
            if (user.IsExternal)
            {
                throw new BusinessException(code: Identity.IdentityErrorCodes.ExternalUserPasswordChange);
            }

            // 验证二次认证码
            if (!await UserManager.VerifyTwoFactorTokenAsync(user, TokenOptions.DefaultPhoneProvider,input.Code))
            {
                throw new UserFriendlyException(L["InvalidVerifyCode"]);
            }

            // 生成重置密码的Token (TODO: 需要阅读源码)
            var resetPwdToken = await UserManager.GeneratePasswordResetTokenAsync(user);

            // 重置密码
            (await UserManager.ResetPasswordAsync(user, resetPwdToken, input.NewPassword)).CheckErrors();

            // 移除验证码的缓存
            await SecurityTokenCache.RemoveAsync(securityTokenCacheKey);

            // 记录安全日志
            await IdentitySecurityLogManager.SaveAsync(
                new IdentitySecurityLogContext
                {
                    Action = "ResetPassword",
                    //ClientId = await FindClientIdAsync(),
                    Identity = "Account",
                    UserName = user.UserName
                });
        }

        /// <summary>
        /// 发送注册短信验证码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="UserFriendlyException"></exception>
        public async Task SendPhoneRegisterCodeAsync(SendPhoneRegisterCodeDto input)
        {
            await CheckSelfRegistrationAsync();
            await CheckNewUserPhoneNumberNotBeUsedAsync(input.PhoneNumber);

            // 生成安全令牌的缓存key
            var securityTokenCacheKey = SecurityTokenCacheItem.CalculateSmsCacheKey(input.PhoneNumber, "SmsVerifyCode");
            //获取缓存中用户的手机安全令牌缓存
            var securityTokenCacheItem = SecurityTokenCache.Get(securityTokenCacheKey);
            var interval = await SettingProvider.GetAsync(Identity.Settings.IdentitySettingNames.User.SmsRepetInterval, 1);

            //如果安全令牌缓存已经存在 提示错误
            if (securityTokenCacheItem != null)
            {
                // 提示验证码已经发送
                throw new UserFriendlyException(L["SendRepeatSmsVerifyCode", interval]);
            }

            //获取发送短信验证码的模板
            var template = await SettingProvider.GetOrNullAsync(IdentitySettingNames.User.SmsNewUserRegister);

            // 
            var securityToken = GuidGenerator.Create().ToString("N");

            //使用 TotpService来生产验证码
            var code = TotpService.GenerateCode(Encoding.Unicode.GetBytes(securityToken), securityTokenCacheKey);


            //给用户发送发送安全令牌短信
            await SendSmsCodeAsync(
                input.PhoneNumber, securityTokenCacheItem.Token, template);

            // 将安全令牌保存到缓存中
            await SecurityTokenCache
            .SetAsync(securityTokenCacheKey, securityTokenCacheItem,
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(interval)
                });
        }


        /// <summary>
        /// 发送手机号登录验证码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task SendPhoneSigninCodeAsync(SendPhoneSigninCodeDto input)
        {
            // 判断验证码是否已经发送
            var securityTokenCacheKey = SecurityTokenCacheItem.CalculateSmsCacheKey(input.PhoneNumber, "SmsVerifyCode");

            var securityTokenCacheItem = await SecurityTokenCache.GetAsync(securityTokenCacheKey);
            var interval = await SettingProvider.GetAsync(IdentitySettingNames.User.SmsRepetInterval, 1);

            if (securityTokenCacheItem != null)
            {   
                //异常错误都是配置多语言提示
                throw new UserFriendlyException(L["SendRepeatSmsVerifyCode", interval]);
            }

            //生成 然后发送用户登录的验证码
            var user = await GetUserByPhoneNumberAsync(input.PhoneNumber, isConfirmed: true);
            var code = await UserManager.GenerateTwoFactorTokenAsync(user, TokenOptions.DefaultPhoneProvider);
            var template = await SettingProvider.GetOrNullAsync(IdentitySettingNames.User.SmsUserSignin);

            await SendSmsCodeAsync(input.PhoneNumber, code, template);

            // 缓存已经发送的验证码
            await SecurityTokenCache
                .SetAsync(securityTokenCacheKey, securityTokenCacheItem,
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(interval)
                }); ;

        }

        public Task SendPhoneResetPasswordCodeAsync(SendPhoneResetPasswordCode input)
        {
            throw new NotImplementedException();
        }

        public async Task SendEmailSigninCodeAsync(SendEmailSigninCodeDto input)
        {

            var user = await UserManager.FindByEmailAsync(input.EmailAddress);

            if (user == null)
            {
                throw new UriFormatException(L["UserNotRegisterd"]);
            }

            if (!user.EmailConfirmed)
            {
                throw new UserFriendlyException(L["UserEmailNotConfired"]);
            }

            var code = await UserManager.GenerateTwoFactorTokenAsync(user, TokenOptions.DefaultEmailProvider);

            // TODO:发送邮件验证码

            throw new NotImplementedException();
        }


        ///// <summary>
        ///// 检查是否允许用户注册
        ///// </summary>
        ///// <returns></returns>
        //protected async virtual Task CheckSelfRegistrationAsync()
        //{
        //    if (!await SettingProvider.IsTrueAsync(Settings.AccountSettingNames.IsSelfRegistrationEnabled))
        //    {
        //        throw new UserFriendlyException(L["SelfRegistrationDisabledMessage"]);
        //    }
        //}

        protected async virtual Task<IdentityUser> GetUserByPhoneNumberAsync(string phoneNumber, bool isConfirmed = true)
        {
            var user = await UserRepository.FindByPhoneNumberAsync(phoneNumber, isConfirmed, true);
            if (user == null)
            {
                throw new UserFriendlyException(L["PhoneNumberNotRegisterd"]);
            }
            return user;
        }


        protected virtual Task CheckNewUserPhoneNumberNotBeUsedAsync(string phoneNumber)
        {

            throw new NotImplementedException();
            //if (await UserRepository.IsPhoneNumberUedAsync(phoneNumber))
            //{
            //    throw new UserFriendlyException(L["DuplicatePhoneNumber"]);
            //}
        }


        //protected virtual Task<string> FindClientIdAsync()
        //{
        //    //var client = LazyServiceProvider.LazyGetRequiredService
        //    return "";
        //}


        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputEmail"></param>
        /// <exception cref="AbpValidationException"></exception>
        private void ThrowIfInvalidEmailAddress(string inputEmail)
        {
            if (!inputEmail.IsNullOrEmpty() && !ValidationHelper.IsValidEmailAddress(inputEmail))
            {
                throw new AbpValidationException(
                    new ValidationResult[]
                    {
                        new ValidationResult(L["The {0} field is not a valid e-mail address.", L["DisplayName:EmailAddress"]], new string[]{ "EmailAddress" })
                    });
            }

        }

    }
}
