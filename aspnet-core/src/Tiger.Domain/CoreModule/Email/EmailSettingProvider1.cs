using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Settings;

namespace Tiger.CoreModule.Email
{   
    /// <summary>
    /// 定义模块内邮件配置
    /// </summary>
    public class EmailSettingProvider1 : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            context.Add(
                new SettingDefinition("Smtp.Host", "127.0.0.1"),
                new SettingDefinition("Smtp.Port", "25"),
                new SettingDefinition("Smtp.UserName","test123456"),
                new SettingDefinition("Smtp.Password", isEncrypted: true),
                new SettingDefinition("Smtp.EnableSsl", "false")
            );
        }
    }
}
