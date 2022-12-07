using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Auditing;

namespace Tiger.Volo.Abp.Account.Dto
{
    public class PhoneRestPasswordDto
    {

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [DisableAuditing]
        public string NewPassword { get; set; }

        [Required]
        [StringLength(6)]
        [DisableAuditing]
        [Display(Name = "DisplayName:SmsVerifyCode")]
        public string Code { get; set; }

    }
}
