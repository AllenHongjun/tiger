using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Auditing;

namespace Tiger.Volo.Abp.Account.Dto
{   
    /// <summary>
    /// 修改手机号
    /// </summary>
    public class ChangePhoneNumberInput
    {
        [Required]
        [Phone]
        [Display(Name = "PhoneNumber")]
        public string NewPhoneNumber { get; set; }


        [Required]
        [DisableAuditing]
        [StringLength(6, MinimumLength = 6)]
        [Display(Name = "SmsVerifyCode")]
        public string Code { get; set; }
    }
}
