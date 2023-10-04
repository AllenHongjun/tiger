using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Tiger.Volo.Abp.Account.Dto;
using Tiger.Volo.Abp.Account.Emailing;
using Tiger.Volo.Abp.Identity;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Caching;
using Volo.Abp.Identity;
using Volo.Abp.Settings;
using Volo.Abp.Sms;
using Volo.Abp.Users;
using static IdentityServer4.Models.IdentityResources;

namespace Tiger.Volo.Abp.Account
{
    /// <summary>
    /// Profile服务
    /// </summary>
    [Authorize]
    [RemoteService(false)]
    public class ProfileAppService : ApplicationService, IProfileAppService
    {
        #region 构造函数
        public ProfileAppService(
            IDistributedCache<SecurityTokenCacheItem> securityTokenCache,
            ITigerIdentityUserRepository userRepository,
            IdentitySecurityLogManager identitySecurityLogManager,
            IdentityUserManager userManager,
            ISmsSender smsSender)
        {
            SecurityTokenCache=securityTokenCache;
            UserRepository=userRepository;
            IdentitySecurityLogManager=identitySecurityLogManager;
            UserManager=userManager;
            SmsSender=smsSender;
        }

        protected IDistributedCache<SecurityTokenCacheItem> SecurityTokenCache { get; }
        protected ITigerIdentityUserRepository UserRepository { get; }
        protected IdentitySecurityLogManager IdentitySecurityLogManager { get; }
        protected IdentityUserManager UserManager { get; }
        protected ISmsSender SmsSender { get; }
        #endregion

        #region 验证手机号
        /// <summary>
        /// 发送修改手机号验证码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task SendChangePhoneNumberCodeAsync(SendChangePhoneNumberCodeInput input)
        {
            var securityTokenCacheKey = SecurityTokenCacheItem.CalculateSmsCacheKey(input.NewPhoneNumber, "SmsChangePhoneNumber");
            var securityTokenCacheItem = await SecurityTokenCache.GetAsync(securityTokenCacheKey);
            // 获取验证码缓存时间设置
            var interval = await SettingProvider.GetAsync(Identity.Settings.IdentitySettingNames.User.SmsRepetInterval, 1);

            if (securityTokenCacheItem != null)
            {
                throw new UserFriendlyException(L["SendRepeatPhoneVerifyCode", interval]);
            }

            // 是否手机号已经被别的用户绑定
            if (await UserRepository.IsPhoneNumberConfirmedAsync(input.NewPhoneNumber))
            {
                throw new BusinessException(Identity.IdentityErrorCodes.DuplicatePhoneNumber, input.NewPhoneNumber);
            }

            var user = await UserManager.GetByIdAsync(CurrentUser.GetId());

            var template = await SettingProvider.GetOrNullAsync(Identity.Settings.IdentitySettingNames.User.SmsPhoneNumberConfirmed);
            var token = await UserManager.GenerateChangePhoneNumberTokenAsync(user, input.NewPhoneNumber);
            // 发送验证码

            Check.NotNullOrWhiteSpace(template, nameof(template));
            var smsMessage = new SmsMessage(input.NewPhoneNumber, token);
            smsMessage.Properties.Add("code", token);
            smsMessage.Properties.Add("TemplateCode", template);

            await SmsSender.SendAsync(smsMessage);

            // 缓存发送的验证码
            securityTokenCacheItem = new SecurityTokenCacheItem(token, user.ConcurrencyStamp);
            await SecurityTokenCache
                .SetAsync(securityTokenCacheKey, securityTokenCacheItem,
                    new DistributedCacheEntryOptions
                    {
                        AbsoluteExpiration = DateTimeOffset.UtcNow.AddMinutes(interval),
                    });
        }

        /// <summary>
        /// 修改绑定的手机号
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task ChangePhoneNumberAsync(ChangePhoneNumberInput input)
        {
            if (await UserRepository.IsPhoneNumberConfirmedAsync(input.NewPhoneNumber))
            {
                throw new BusinessException(Identity.IdentityErrorCodes.DuplicatePhoneNumber);
            }

            var user = await UserManager.GetByIdAsync(CurrentUser.GetId());

            (await UserManager.ChangePhoneNumberAsync(user, input.NewPhoneNumber, input.Code))
                .CheckErrors();
            await CurrentUnitOfWork.SaveChangesAsync();

            var securityTokenCacheKey = SecurityTokenCacheItem.CalculateSmsCacheKey(input.NewPhoneNumber, "SmsChangePhoneNumber");
            await SecurityTokenCache.RemoveAsync(securityTokenCacheKey);
        }
        #endregion

        #region 验证邮箱
        /// <summary>
        /// 发送邮箱确认链接
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task SendEmailConfirmLinkAsync(SendEmailConfirmCodeDto input)
        {
            var user = await UserManager.FindByEmailAsync(input.Email);

            if (user == null)
            {
                throw new UserFriendlyException(L["Volo.Account.InvalidEmailAddress", input.Email]);
            }

            if (user.EmailConfirmed)
            {
                throw new BusinessException(Identity.IdentityErrorCodes.DuplicateConfirmEmailAddress);
            }

            var token = await UserManager.GenerateEmailConfirmationTokenAsync(user);

            // 发送确认邮件
            IAccountEmailConfirmSender accountEmailConfirmSender = null;
            var sender = LazyGetRequiredService(ref accountEmailConfirmSender);
            await sender.SendEmailConfirmLinkAsync(
                user,
                token,
                input.AppName,
                input.ReturnUrl,
                input.ReturnUrlHash);
        }


        /// <summary>
        /// 用户确认邮箱
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task ConfirmEmailAsync(ComfirmEmailInput input)
        {
            //await IdentityOptions.SetAsync();

            var user = await UserManager.GetByIdAsync(input.UserId);
            (await UserManager.ConfirmEmailAsync(user, input.ConfirmToken)).CheckErrors();

            await IdentitySecurityLogManager.SaveAsync(new IdentitySecurityLogContext
            {
                Identity = IdentitySecurityLogIdentityConsts.Identity,
                Action = "ConfirmEmail"
            });
        } 
        #endregion


        /// <summary>
        /// 修改用户头像
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <remarks> 通用用户声明类型配置用户头像</remarks>
        public async  virtual Task ChangeAvatarAsync(ChangeAvatarInput input)
        {

            var user = await UserManager.GetByIdAsync(CurrentUser.GetId());

            user.Claims.RemoveAll(x => x.ClaimType.Equals(IdentityConsts.ClaimType.Avatar.Name));

            var avatrName = nameof(IdentityConsts.ClaimType.Avatar.Name);

            user.AddClaim(GuidGenerator, new Claim(avatrName, input.AvatarUrl));

            (await UserManager.UpdateAsync(user)).CheckErrors();

            await CurrentUnitOfWork.SaveChangesAsync();
        }

    }
}
