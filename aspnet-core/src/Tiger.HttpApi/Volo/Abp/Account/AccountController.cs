using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tiger.Volo.Abp.Account.Dto;
using TigerAdmin.Volo.Abp.Account;
using Volo.Abp;
using Volo.Abp.Account;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Identity;

namespace Tiger.Volo.Abp.Account
{   
    /// <summary>
    /// 账号
    /// </summary>
    [Controller]
    [RemoteService(Name = AccountRemoteServiceConsts.RemoteServiceName)]
    [Area("Account")]
    [Route("api/account")]
    public class AccountController : AbpController, ITigerAccountAppService
    {
        protected ITigerAccountAppService AccountAppService { get; }

        public AccountController(ITigerAccountAppService accountAppService) 
        {
            AccountAppService = accountAppService;
        }
        
        
        

        /// <summary>
        /// 通过手机号注册
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("phone/register")]
        public async Task RegisterAsync(PhoneRegisterDto input)
        {
            await AccountAppService.RegisterAsync(input);
        }

        
        /// <summary>
        /// 发送手机号注册验证码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("phone/send-register-code")]
        public async Task SendPhoneRegisterCodeAsync(SendPhoneRegisterCodeDto input)
        {
            await AccountAppService.SendPhoneRegisterCodeAsync(input);
        }

        /// <summary>
        /// 发送手机号登录验证码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("phone/send-signin-code")]
        public async Task SendPhoneSigninCodeAsync(SendPhoneSigninCodeDto input)
        {
            await AccountAppService.SendPhoneSigninCodeAsync(input);
        }

        /// <summary>
        /// 发送手机号密码重置验证码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("phone/send-password-reset-code")]
        public async Task SendPhoneResetPasswordCodeAsync(SendPhoneResetPasswordCode input)
        {
            await AccountAppService.SendPhoneResetPasswordCodeAsync(input);
        }

        /// <summary>
        /// 通过手机号重置密码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("phone/reset-password")]
        public async Task ResetPasswordAsync(PhoneResetPasswordDto input)
        {
            await AccountAppService.ResetPasswordAsync(input);
        }

        /// <summary>
        /// 发送邮件登录验证码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("email/send-signin-code")]
        public async Task SendEmailSigninCodeAsync(SendEmailSigninCodeDto input)
        {
            await AccountAppService.SendEmailSigninCodeAsync(input);
        }

        //[HttpGet]
        //[Route("two-factor-providers")]
        //public async virtual Task<ListResultDto<NameValue>> GetTwoFactorProvidersAsync(GetTwoFactorProvidersInput input)
        //{
        //    return await AccountAppService.GetTwoFactorProvidersAsync(input);
        //}
    }
}
