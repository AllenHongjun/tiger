using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Tiger.Volo.Abp.Account.Dto
{
    public class SendPhoneRegisterCodeDto
    {

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
    }
}
