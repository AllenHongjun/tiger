using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Auditing;

namespace Tiger.Volo.Abp.Account.Dto
{
    public class PhoneResetPasswordDto
    {

        /// <summary>
        /// 手机号
        /// </summary>
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 新密码
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        [DisableAuditing]
        public string NewPassword { get; set; }

        /// <summary>
        /// 短信验证码
        /// </summary>
        [Required]
        [StringLength(6)]
        [DisableAuditing]
        [Display(Name = "DisplayName:SmsVerifyCode")]
        public string Code { get; set; }

    }
}
