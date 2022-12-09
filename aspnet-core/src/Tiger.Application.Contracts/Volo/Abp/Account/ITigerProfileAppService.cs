using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tiger.Volo.Abp.Account.Dto;

namespace Tiger.Volo.Abp.Account
{
    /// <summary>
    /// 针对每个帐户的 数据存储 
    /// </summary>
    public interface ITigerProfileAppService
    {

        //Task<TwoFactorEnableDto> GetTwoFactorEnabledAsync();

        /// <summary>
        /// 发送改变手机号验证码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task SendChangePhoneNumberCodeAsync(SendChangePhoneNumberCodeInput input);

        /// <summary>
        /// 更改手机绑定
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task ChangePhoneNumberAsync(ChangePhoneNumberInput input);

        /// <summary>
        /// 发送确认邮件验证码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task SendEmailConfirmLinkAsync(SendEmailConfirmCodeDto input);

        /// <summary>
        /// 确认邮件地址
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task ConfirmEmailAsync(ComfirmEmailInput input);


    }
}
