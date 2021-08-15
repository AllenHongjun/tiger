- # to do list

- 使用 c# js分别实现递归的方法。实现树和对象的遍历。

- 熟悉linqpad 工具的使用。

- ABP整合存储过程

  - 考虑弃用存储过程。查询全部写在linq当中。
  
- abp 整合分词搜索

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

- 整理参考一些好的开源web项目。

- **添加租户完成之后。默认添加一个租户的超级管理员。**

- **实现 elementUI- 三级联动地址选择器。**

- **文件上传。单图片上传。多图片自动上传。单文件excel上传。**

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

- **[给swagger-ui分组显示](https://mp.weixin.qq.com/s/cNB469s18plbCLbHxL1QUA)**

  - [swagger-ui接口分组显示--第一次尝试失败。](https://blog.csdn.net/qq_35655841/article/details/102838850)
  - [Swashbuckle.AspNetCore](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)
  - [ABP api控制器官方文档](https://docs.abp.io/zh-Hans/abp/3.2/API/Auto-API-Controllers)

- 项目开源 如何忽略key 等配置文件？****

  - [如何看待大疆的服务器 key 在 Github 上泄露事件？](https://www.zhihu.com/question/68495272)
  - 不要偷懒。其他的key.公布的就修改一下。
  - [git .gitignore 忽略的文件还会被提交](https://blog.csdn.net/zzk220106/article/details/108639115)
  
- **abp 对接七牛云文件存储 这个估计要2天**
  - [七牛开发者中心](https://developer.qiniu.com/kodo)
  - 对接开发一些第三方的服务接口。
  
- 租户的数据没有做隔离。 给租户添加用户

  - 通过状态来前端缓存租户信息。然后请求的时候带上租户的信息

- 学习abp 的源码 demo 熟悉使用。