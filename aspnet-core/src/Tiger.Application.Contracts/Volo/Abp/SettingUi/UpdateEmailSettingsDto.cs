using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Auditing;

namespace Tiger.Volo.Abp.SettingManagementAppService
{   
    /// <summary>
    /// 更新邮箱设置
    /// </summary>
    public class UpdateEmailSettingsDto
    {   
        /// <summary>
        /// Smtp IP
        /// </summary>
        [MaxLength(256)]
        public string SmtpHost { get; set; }

        /// <summary>
        /// 端口号
        /// </summary>
        [Range(1, 65535)]
        public int SmtpPort { get; set; }

        /// <summary>
        /// Smtp用户名
        /// </summary>
        [MaxLength(1024)]
        public string SmtpUserName { get; set; }

        /// <summary>
        /// Smtp密码
        /// </summary>
        [MaxLength(1024)]
        [DataType(DataType.Password)]
        [DisableAuditing]
        public string SmtpPassword { get; set; }

        /// <summary>
        /// Smtp域名
        /// </summary>
        [MaxLength(1024)]
        public string SmtpDomain { get; set; }

        /// <summary>
        /// 启用https
        /// </summary>
        public bool SmtpEnableSsl { get; set; }

        public bool SmtpUseDefaultCredentials { get; set; }

        /// <summary>
        /// 默认发件地址
        /// </summary>
        [MaxLength(1024)]
        [Required]
        public string DefaultFromAddress { get; set; }

        /// <summary>
        /// 默认发件人名称
        /// </summary>
        [MaxLength(1024)]
        [Required]
        public string DefaultFromDisplayName { get; set; }
    }
}
