using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;
using Tiger.Volo.Abp.Account.Dto;
using TigerAdmin.Volo.Abp.Account;
using Volo.Abp;
using Volo.Abp.Account;
using Volo.Abp.Account.Emailing;
using Volo.Abp.AuditLogging;
using Volo.Abp.Identity;
using Volo.Abp.Settings;
using Volo.Abp.Users;
using Volo.Abp.Validation;

namespace Tiger.Volo.Abp.Account
{   
    /// <summary>
    /// 用户账号管理
    /// </summary>
    [RemoteService(false)]
    public class TigerAccountAppService : AccountAppService, ITigerAccountAppService
    {
        protected IAuditLogRepository AuditLogRepository { get; }
        protected IAccountAppService AccountAppService { get; }

        protected IUserRepository<IdentityUser> UserRepository { get; }

        //protected IAbpApiDefinition

        //protected IApplicationConfiguration

        public TigerAccountAppService(
            IdentityUserManager userManager, 
            IIdentityRoleRepository roleRepository, 
            IAccountEmailer accountEmailer, 
            IdentitySecurityLogManager identitySecurityLogManager
            ) : base(userManager, roleRepository, accountEmailer, identitySecurityLogManager)
        {

        }

        public void Test()
        {
            //UserManager.AddLoginAsync();
        }

        public Task RegisterAsync(PhoneRegisterDto input)
        {
            throw new NotImplementedException();
        }

        public Task ResetPasswordAsync(PhoneRestPasswordDto input)
        {
            throw new NotImplementedException();
        }

        public async Task SendPhoneRegisterCodeAsync(SendPhoneRegisterCodeDto input)
        {
            await CheckSelfRegistrationAsync();
            await CheckNewUserPhoneNumberNotBeUsedAsync(input.PhoneNumber);



            throw new NotImplementedException();
        }

        public Task SendPhoneSigninCodeAsync(SendPhoneSigninCodeDto input)
        {
            throw new NotImplementedException();
        }

        public Task SendPhoneResetPasswordCodeAsync(SendPhoneResetPasswordCode input)
        {
            throw new NotImplementedException();
        }

        public Task SendEmailSigninCodeAsync(SendEmailSigninCodeDto input)
        {
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

        protected async virtual Task CheckNewUserPhoneNumberNotBeUsedAsync(string phoneNumber)
        {
            return;
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
        /// 严重邮箱
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
