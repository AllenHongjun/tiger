### 0811 abp 使用问题 02

> 查找 abp vnext 的教程 升级这个高度。 把这个框架 数量使用。变成趁手的一件工具。 abp 框架使用 缓存redis 定时任务 日志 identityserver

- 如何集成 redis 缓存。使用缓存帮助类。

  - [abp官方文档说明](https://docs.abp.io/zh-Hans/abp/3.2/Caching)
  - [集成redis说明文档](https://mp.weixin.qq.com/s/fTqDnwVUgqKnwz21AsETGA)
  - 

- 如何禁用本地化，直接使用中文。验证的消息。异常小直接返回中文。

  - 在 host 的TigerAdminHttpApiHostModule 模块配置文件中 配置多语言。

  - ```C#
    /// <summary>
            /// 多语言配置
            /// </summary>
            private void ConfigureLocalization()
            {
                Configure<AbpLocalizationOptions>(options =>
                {
                    //options.Languages.Add(new LanguageInfo("ar", "ar", "العربية"));
                    //options.Languages.Add(new LanguageInfo("cs", "cs", "Čeština"));
                    //options.Languages.Add(new LanguageInfo("en", "en", "English"));
                    //options.Languages.Add(new LanguageInfo("fr", "fr", "Français"));
                    //options.Languages.Add(new LanguageInfo("pt-BR", "pt-BR", "Português"));
                    //options.Languages.Add(new LanguageInfo("ru", "ru", "Русский"));
                    //options.Languages.Add(new LanguageInfo("tr", "tr", "Türkçe"));
                    options.Languages.Add(new LanguageInfo("zh-Hans", "zh-Hans", "简体中文"));
                    //options.Languages.Add(new LanguageInfo("zh-Hant", "zh-Hant", "繁體中文"));
                });
            }
    ```

  - 

- 如何使用日志记录功能。。如何写日志。记录日志 abp整合log4net 遇到的问题。

  - [.NET Core 下使用 Log4Net 记录日志](https://www.cnblogs.com/meowv/p/13718979.html)
  - [asp.net 日志文件记录](https://www.cnblogs.com/xbzhu/p/13033573.html)
  - [[.NET Core log4net 使用](https://www.cnblogs.com/linezero/p/log4net.html)](https://www.cnblogs.com/linezero/p/log4net.html)
  - abp 引入log4net 无法写入内容。。
  - 主要要选择使用3.0版本的包 不然无法使用
  - [一直都是有文件。但是没有日志内容] 原型就是写日志的对象使用的是注入的方式。一直都没有注入成功

- 

- 如何使用identityServer来授权日志

- 表单验证fluentValidation验证

  - [FluentValidation](https://docs.abp.io/en/abp/3.2/FluentValidation)  集成使用 因为dto都是在Contract层。。所有的请求参数都在这里。就把包放在了这一层。
  - [微软自带的表单验证教程](https://docs.microsoft.com/zh-cn/aspnet/core/mvc/models/validation?view=aspnetcore-5.0)
  - [[.NET Core中的验证组件FluentValidation的实战分享](https://www.cnblogs.com/yilezhu/p/10397393.html)](https://www.cnblogs.com/yilezhu/p/10397393.html)
  - [[基于 .NET 的 FluentValidation 验证教程](https://www.xcode.me/post/5849#)](https://www.xcode.me/post/5849)

- 使用定时任务

  - [https://docs.abp.io/zh-Hans/abp/3.2/Background-Jobs](https://docs.abp.io/zh-Hans/abp/3.2/Background-Jobs)

- 

- 

- 如何使用crud 多表关联查询。

- 如何配置swaggerUI 能够将返回的接口有中文日志解释显示。

- 解决方案重名名方法。

  > 新建一个分支测试一下

  - [如何重命名解决方案](https://qastack.cn/programming/2043618/proper-way-to-rename-solution-and-directories-in-visual-studio)

  - [修改项目名称](https://blog.csdn.net/wml00000/article/details/104209106)

  - 修改之后前端无法登陆

  - # [Failed to generate SSPI context.'](https://stackoverflow.com/questions/49031082/c-sharp-system-data-sqlclient-sqlexception-failed-to-generate-sspi-context)  无法更新数据库

  - 无法登陆。 identity-server 授权失败。 登陆成功无法获取权限

    - 因为abp框架集成了 identity-server4 修改了项目。数据库中 scope，client_id 还是原来的值。 需要 add-migraion 来修改
    - 整理一下EF更新数据库 用到的命令。
    - abp 会根据 自动迁移生成数据库的时候 会根据 数据层的配置来生成数据库。abp 已经帮你配置好了。你需要的就是 部署好之后。调用identity 开放的端口 域名来使用。重点还是需要了解 identtiy的使用。 同时也要熟悉模板中的代码。

  - 登陆的业务逻辑不知道原来是如何来实现的。

- 把核心层的部分类库 放在domain 里面处理。

  - **基础设施层**: 提供通用的技术功能,支持更高的层,主要使用第三方类库.
  - 让其他的层来依赖基础设置层。
  - 业务实体类也放在这里。其他的缓存 定时任务。邮件 等等 第三方服务也都放这里。

- 

- 