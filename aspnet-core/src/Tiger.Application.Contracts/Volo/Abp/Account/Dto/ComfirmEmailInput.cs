using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Tiger.Volo.Abp.Account.Dto
{   
    /// <summary>
    /// 邮箱确认
    /// </summary>
    public class ComfirmEmailInput
    {
        [Required]
        public Guid UserId {get; set;}

        [Required]
        public string ConfirmToken { get; set; }
    }
}
