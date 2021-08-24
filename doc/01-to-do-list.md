- # to do list

- **使用 c# js分别实现递归的方法。实现树和对象的遍历。**

  - 技术点结合功能来时间。
  - 商品分类功能开发

- > 使用ABP 有很多常用的组件。用熟悉。。会用。做好东西。。进阶学习一些微服务的知识。
  >
  > demo里面的代码看着确实写的舒服很多。。

- **整理开发文档。生成外键 生成数据库关系图 开发 营销 商品 订单 会员 统计 模块功能**

  - 添加表单验证
  - 添加数据库生成时候字段验证。主外键关系。相关配置 
  - automapper相关配置
  - 产品和订单模块用导航属性做。。学习使用领域驱动的方法来开发。另外的模块

- EF 一对多 多对多 关系 增删改查使用

  - DF 多对一关系。使用
  - 订单 产品 和订单明细的管理 
  - 商品SKU 商品 属性 属性值的关联

- 开发 商品属性 属性分类 商品管理 商品关联sku功能模块

- 循环修改数据。

  - 一条一条的循环修改数据
  - 批量修改数据功能

- abp 整合分词搜索

  - 商品分词搜索接口功能

- 实现定时任务

  - 定时发送邮件
  - 定时统计数据

- 实现redis缓存

- > 弄一个扩展方法。缓存的对象都是string类型 来保存。生成自定义的的key  缓存的时候 读取的时候解析出来。
  >
  > 商品列表数据缓存。后台数据缓存 等

  - [官方 abp redis使用教程](https://docs.abp.io/en/abp/3.2/Redis-Cache)

  - [redis-缓存继承教程](https://www.cnblogs.com/meowv/p/12956696.html)

  - redis缓存的key 如何来定义

  - IDistributedCache<OrganizationUnitDto>  这个泛型服务 如果有服务里有另外一个类型要存储 如何来处理。

    - 这样如果有page 有 list 的缓存如何来处理？

    - 如何给abp框架里面的方法写一个扩展方法。

    - [IDistributedCache封装使用](https://www.cnblogs.com/fanfan-90/p/12151924.html)

    - abp 缓存的用法 。看源码是如何来使用。

      - 缓存是专门封装一个缓存类来使用 缓存的key也在类中来生成

      - ```C#
        [Serializable]
            public class PermissionGrantCacheItem
            {
                private const string CacheKeyFormat = "pn:{0},pk:{1},n:{2}";
        
                public bool IsGranted
                {
                    get;
                    set;
                }
        
                public PermissionGrantCacheItem()
                {
                }
        
                public PermissionGrantCacheItem(bool isGranted)
                {
                    IsGranted = isGranted;
                }
        
                public static string CalculateCacheKey(string name, string providerName, string providerKey)
                {
                    return $"pn:{providerName},pk:{providerKey},n:{name}";
                }
        
                public static string GetPermissionNameFormCacheKeyOrNull(string cacheKey)
                {
                    FormattedStringValueExtracter.ExtractionResult extractionResult = FormattedStringValueExtracter.Extract(cacheKey, "pn:{0},pk:{1},n:{2}", ignoreCase: true);
                    if (!extractionResult.IsMatch)
                    {
                        return null;
                    }
        
                    return extractionResult.Matches.Last().Value;
                }
            }
        }
        ```

      - 

- 整合mongodb实现文档操作

- 结合id4实现授权登录

- [短信和邮件的支持]

  > 有点小麻烦 需要花费一点时间。

  - [短信和邮件](https://www.cnblogs.com/myzony/p/abp-vnext-email-and-sms-source-analyzsis.html)
    - [阿里云短信 服务](https://help.aliyun.com/document_detail/44350.html?spm=5176.8195934.J_5834642020.5.65044378DBQXtd#section-kwn-68l-hxi)
  - [abp vnext 发送邮件](https://www.cnblogs.com/webenh/p/13140460.html)
  - [asp.net core 发送邮件。](https://www.cnblogs.com/savorboard/p/aspnetcore-email.html)

- abp github登录功能。

  - 对接github 授权登录功能

    - [授权配置页面](https://github.com/settings/developers)
    - [官方文档](https://docs.github.com/en/developers/apps/building-oauth-apps/authorizing-oauth-apps)
    - [官方文档-中文](https://docs.github.com/cn/developers/apps/building-oauth-apps/authorizing-oauth-apps)
    - [ASP.NET Core 中的 Facebook、Google 和外部提供程序身份验证](https://docs.microsoft.com/zh-cn/aspnet/core/security/authentication/social/?view=aspnetcore-5.0&tabs=visual-studio)
    - [github授权登录教程 博客系统](https://mp.weixin.qq.com/s/ZOX9D4ncqqeXxipYapTeBA)
    - 如何对接
      - [AspNet.Security.OAuth.Providers](https://github.com/aspnet-contrib/AspNet.Security.OAuth.Providers)  使用第三方组件快速开发。
      - [AspNet.Security.OAuth.Providers使用教程](https://www.cnblogs.com/igeekfan/p/12110012.html)
      - 获取 code 
      - 获取token 
      - 获取用户信息
      - 其他使用处理
        - token 过期处理
      - 兼容qq 等其他第三方登录

- **如何集成帮助类。比如生成二维码，导出excel 等等。**

  - 导入导出 excel 文档操作

- 整理参考一些好的开源web项目。

- **添加租户完成之后。默认添加一个租户的超级管理员。**

- **实现 elementUI- 三级联动地址选择器。**

  - 用户地址管理功能开发

- **文件上传。单图片上传。多图片自动上传。单文件excel上传。**

  - 分类图片编辑。

- **增加一个mock.列表来显示**

- **开发一个后台的dashboard主页。**

- **npm 命令  整理npm 前端常用的命令 设置 镜像等。**

  - npm uninstall --save echarts
  - npm install echarts@4.2.1

- 菜单管理。后台菜单控制

- **如何使用消息队列** 

- **如何使用mongo db** 

- **如何部署到linux** 

- **多对多数据表管理列表。显示。**

- **开发系统设置功能**

- **开发特性功能。abp**







- 为什么有代码调试还需要单元测试。

- - 

- **学习 微软 asp.net core 官方文档的资料** 

  - 日志 
  - 选项模式
  - - - 
  - log  自带的 如何按天分文件 按类型 单独写入日志 
  - [asp.net core configuration](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration/?view=aspnetcore-5.0)

- 熟悉官方的文档  教程。跟着操作一遍。练习。还有微软官方的。。找到自己需要掌握的地方。

- 查看类关系图。  vitual studio 

- 熟悉linqpad 工具的使用。

- 给接口分api admin 区域。确认接口写在哪一层里面。

  - #### HttpApi 项目  主要编写接口的地方

  - #### Application 项目 service服务层

  - #### Web 项目 mvc 界面显示层。

- ABP整合存储过程

  - 考虑弃用存储过程。查询全部写在linq当中。



- 

- **[给swagger-ui分组显示](https://mp.weixin.qq.com/s/cNB469s18plbCLbHxL1QUA)**  配置默认不展开。

  > 配置根据版本来过滤接口。

  - [swagger-ui接口分组显示--第一次尝试失败。](https://blog.csdn.net/qq_35655841/article/details/102838850)

  - [Swashbuckle.AspNetCore](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)

  - [ABP api控制器官方文档](https://docs.abp.io/zh-Hans/abp/3.2/API/Auto-API-Controllers)

  - [swaggui-ui接口分组配置展示](https://www.cnblogs.com/FateHuli/p/10821018.html)

  - ```c#
     options.SwaggerDoc("auth", new OpenApiInfo { Title = "TinyErpAuth", Version = "auth" });
                        options.SwaggerDoc("gp", new OpenApiInfo { Title = "登录模块", Version = "GP" });
                        options.SwaggerDoc("mom", new OpenApiInfo { Title = "业务模块", Version = "YW" });
                        options.SwaggerDoc("dm", new OpenApiInfo { Title = "其他模块", Version = "QT" });
     ```

    


                        //根据分组来设置要展示的接口
                        options.DocInclusionPredicate((docName, apiDes) =>
                        {
                            if (!apiDes.TryGetMethodInfo(out MethodInfo method))
                                return false;
                            /*使用ApiExplorerSettingsAttribute里面的GroupName进行特性标识
                             * DeclaringType只能获取controller上的特性
                             * 我们这里是想以action的特性为主
                             * */
                            var version = method.DeclaringType.GetCustomAttributes(true).OfType<ApiExplorerSettingsAttribute>().Select(m => m.GroupName);
                            if (docName == "v1" && !version.Any())
                                return true;
                            //这里获取action的特性
                            var actionVersion = method.GetCustomAttributes(true).OfType<ApiExplorerSettingsAttribute>().Select(m => m.GroupName);
                            if (actionVersion.Any())
                                return actionVersion.Any(v => v == docName);
                            return version.Any(v => v == docName);
                        });
    ```

  - 
    ```

    ```
    
- 项目开源 如何忽略key 等配置文件？****

  - [如何看待大疆的服务器 key 在 Github 上泄露事件？](https://www.zhihu.com/question/68495272)
  - 不要偷懒。其他的key.公布的就修改一下。
  - [git .gitignore 忽略的文件还会被提交](https://blog.csdn.net/zzk220106/article/details/108639115)
  
- **