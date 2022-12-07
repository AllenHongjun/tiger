using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tiger.Volo.Abp.Account.Dto;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace TigerAdmin.Volo.Abp.Account
{
    public interface ITigerAccountAppService:IApplicationService
    {

        /// <summary>
        /// 通过手机号注册
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task RegisterAsync(PhoneRegisterDto input);

        /// <summary>
        /// 通过手机号重置用户密码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task ResetPasswordAsync(PhoneRestPasswordDto input);


        /// <summary>
        /// 发送手机号注册验证码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task SendPhoneRegisterCodeAsync(SendPhoneRegisterCodeDto input);

        /// <summary>
        /// 发送手机号登陆验证码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task SendPhoneSigninCodeAsync(SendPhoneSigninCodeDto input);


        /// <summary>
        /// 发送手机号重置密码验证码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task SendPhoneResetPasswordCodeAsync(SendPhoneResetPasswordCode input);

        /// <summary>
        /// 发送邮件登录验证码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task SendEmailSigninCodeAsync(SendEmailSigninCodeDto input);

        ///// <summary>
        ///// 获取用户二次认证提供者列表
        ///// </summary>
        ///// <param name="input"></param>
        ///// <returns></returns>
        //Task<ListResultDto<NameValue>> GetTwoFactorProvidersAsync(GetTwoFactorProvidersInput input);

    }
}
