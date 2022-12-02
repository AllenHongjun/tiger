using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Tiger.Volo.Abp.SettingManagementAppService
{   
    /// <summary>
    /// 发送测试邮件
    /// </summary>
    public class SendTestEmailInput
    {   
        /// <summary>
        /// 发件人地址
        /// </summary>
        [Required]
        public string SenderEmailAddress { get; set; }

        /// <summary>
        /// 收件人地址
        /// </summary>
        [Required]
        public string TargetEmailAddress { get; set; }

        /// <summary>
        /// 主题
        /// </summary>
        [Required]
        public string Subject { get; set; }

        /// <summary>
        /// 正文
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// 附件
        /// </summary>
        public string attachment { get; set; }
    }
}
