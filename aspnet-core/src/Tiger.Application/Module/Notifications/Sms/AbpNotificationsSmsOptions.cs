using System;
using System.Collections.Generic;
using System.Text;

namespace Tiger.Infrastructure.Notification.Sms
{
    public class AbpNotificationsSmsOptions
    {
        /// <summary>
        /// 短信模板变量前缀
        /// </summary>
        public string TemplateParamsPrefix { get; set; } = "[sms]";
    }
}
