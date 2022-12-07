using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Tiger.Volo.Abp.Account.Dto
{
    public class SendEmailConfirmCodeDto
    {
        [Required]
        [EmailAddress]
        [Display(Name ="EmailAddress")]
        public string Email { get; set; }

        [Required]
        public string AppName { get; set; }

        public string ReturnUrl { get; set; }

        public string ReturnUrlHash { get; set; }
    }
}
