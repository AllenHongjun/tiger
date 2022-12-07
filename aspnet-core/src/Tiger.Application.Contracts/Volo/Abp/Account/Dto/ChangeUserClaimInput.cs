using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Tiger.Volo.Abp.Account.Dto
{
    public class ChangeUserClaimInput
    {
        [Required]
        public string ClaimType { get; set; }

        public string ClaimValue { get; set; }
    }
}
