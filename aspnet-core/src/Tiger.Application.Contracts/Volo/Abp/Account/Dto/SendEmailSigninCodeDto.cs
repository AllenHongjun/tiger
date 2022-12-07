using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Tiger.Volo.Abp.Account.Dto
{
    public class SendEmailSigninCodeDto
    {
        [Required]
        [EmailAddress]
        [Display(Name = "EmailAddress")]
        public string EmailAddress { get; set; }
    }
}
