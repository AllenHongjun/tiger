using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using Volo.Abp.Account;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using System.Threading.Tasks;
using Tiger.Volo.Abp.Account.Dto;

namespace Tiger.Volo.Abp.Account
{
    /// <summary>
    /// 个人信息管理
    /// </summary>
    [Controller]
    [RemoteService(Name = AccountRemoteServiceConsts.RemoteServiceName)]
    [Area("Account")]
    [Route("api/account/profile")]
    public class ProfileController : AbpController, IProfileAppService
    {
        protected readonly IProfileAppService profileAppService;

        public ProfileController(IProfileAppService profileAppService)
        {
            this.profileAppService=profileAppService;
        }

        /// <summary>
        /// 发送修改手机号验证码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("send-change-phone-number-code")]
        public async Task SendChangePhoneNumberCodeAsync(SendChangePhoneNumberCodeInput input)
        {
            await profileAppService.SendChangePhoneNumberCodeAsync(input);
        }

        /// <summary>
        /// 修改用户手机号
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("change-phone-number")]
        public async Task ChangePhoneNumberAsync(ChangePhoneNumberInput input)
        {
            await profileAppService.ChangePhoneNumberAsync(input);
        }

        /// <summary>
        /// 确认用户邮箱
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("confirm-email")]
        public async Task ConfirmEmailAsync(ComfirmEmailInput input)
        {
            await profileAppService.ConfirmEmailAsync(input);
        }


        /// <summary>
        /// 发送邮箱确认链接
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("send-email-confirm-link")]
        public async Task SendEmailConfirmLinkAsync(SendEmailConfirmCodeDto input)
        {
            await profileAppService.SendEmailConfirmLinkAsync(input);
        }

        /// <summary>
        /// 修改用户头像
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("change-avatar")]
        public async Task ChangeAvatarAsync(ChangeAvatarInput input)
        {
            await profileAppService.ChangeAvatarAsync(input);
        }
    }
}
