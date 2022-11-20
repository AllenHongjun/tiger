/**
 * 类    名：EmailSettingProvider   
 * 作    者：花生了什么树       
 * 创建时间：2021/8/16 17:54:13       
 * 说    明: 
 * 
 */

using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Settings;

namespace Tiger.Books.Demo
{   
    /// <summary>
    /// ABP设置系统使用 demo
    /// </summary>
    public class EmailSettingProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {

            // ABP会自动发现并注册设置的定义.
            /*
             SettingDefinition
            SettingDefinition 类具有以下属性:

            Name: 应用程序中设置的唯一名称. 是具有约束的唯一属性, 在应用程序获取/设置此设置的值 (设置名称定义为常量而不是magic字符串是个好主意).
            DefaultValue: 设置的默认值.
            DisplayName: 本地化的字符串,用于在UI上显示名称.
            Description: 本地化的字符串,用于在UI上显示描述.
            IsVisibleToClients: 布尔值,表示此设置是否在客户端可用. 默认为false,避免意外暴漏内部关键设置.
            IsInherited: 布尔值,此设置值是否从其他提供程序继承. 如果没有为请求的提供程序设置设定值,那么默认值是true并回退到下一个提供程序 (参阅设置值提供程序部分了解更多).
            IsEncrypted: 布尔值,表示是否在保存值是加密,读取时解密. 在数据库中存储加密的值.
            Providers: 限制可用于特定的设置值提供程序(参阅设置值提供程序部分了解更多).
            Properties: 设置此值的自定义属性 名称/值 集合,可以在之后的应用程序代码中使用.
             
             
             */


            context.Add(
                new SettingDefinition("Abp.Mailing.Smtp.Host", "127.0.0.1"),
                new SettingDefinition("Abp.Mailing.Smtp.Port", "25"),
                new SettingDefinition("Abp.Mailing.Smtp.UserName", "hongjy"),
                new SettingDefinition("Abp.Mailing.Smtp.Password", isEncrypted: true),
                new SettingDefinition("Abp.Mailing.Smtp.Domain", ""),
                new SettingDefinition("Abp.Mailing.Smtp.EnableSsl", "false"),
                new SettingDefinition("Abp.Mailing.Smtp.UseDefaultCredentials", "true"),
                new SettingDefinition("Abp.Mailing.DefaultFromAddress", "hongjy1991@gmai.com"),
                new SettingDefinition("Abp.Mailing.DefaultFromDisplayName", "ABP application")
            );
        }
    }
}
