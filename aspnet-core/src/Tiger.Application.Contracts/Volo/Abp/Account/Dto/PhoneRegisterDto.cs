using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Auditing;

namespace Tiger.Volo.Abp.Account.Dto
{   
    /// <summary>
    /// 手机号注册
    /// </summary>
    public class PhoneRegisterDto
    {
        [Required]
        [Phone]
        [Display(Name = "PhoneNumber")]
        public string PhoneNumber { get; set; }

        public string Name { get; set; }

        public string UserName { get; set; }

        [EmailAddress]
        public string EmailAddress { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [DisableAuditing]
        public string Password { get; set; }

        [Required]
        [StringLength(6, MinimumLength = 6)]
        [DisableAuditing]
        public string Code { get; set; }
    }
}
