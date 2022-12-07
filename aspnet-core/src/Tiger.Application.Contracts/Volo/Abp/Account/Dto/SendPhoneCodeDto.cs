using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Tiger.Volo.Abp.Account.Dto
{
    public class SendPhoneCodeDto
    {

        [Required]
        [Phone]
        [Display(Name = "PhoneNumber")]
        public string PhoneNumber { get; set; }
    }
}
