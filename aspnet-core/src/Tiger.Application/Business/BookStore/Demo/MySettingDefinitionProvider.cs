/**
 * 类    名：MySettingDefinitionProvider   
 * 作    者：花生了什么树       
 * 创建时间：2021/8/16 17:56:59       
 * 说    明: 
 * 
 */

using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Localization;
using Volo.Abp.Settings;

namespace Tiger.Books.Demo
{
    public class MySettingDefinitionProvider : SettingDefinitionProvider
    {
        // 在某些情况下,你可能希望更改应用程序/模块所依赖的其他模块中定义的设置的某些属性. 设置定义提供程序可以查询和更新设置定义.
        //   下面的示例中获取了由 Volo.Abp.Emailing 包定义的设置并将其更改:

        public override void Define(ISettingDefinitionContext context)
        {
            var smtpHost = context.GetOrNull("Abp.Mailing.Smtp.Host");
            if (smtpHost != null)
            {
                smtpHost.DefaultValue = "mail.mydomain.com";
                //smtpHost.DisplayName =
                //    new LocalizableString(
                //        typeof(MyLocalizationResource),
                //        "SmtpServer_DisplayName"
                //    );
            }
        }
    }
}
