### 0828

- 产品管理模块

  - 产品相册 多图片上传

  - 产品主图

    - 图片域名前端配置。返回数据库保存没有域名的路径。

    - [process.env配置文件](https://segmentfault.com/a/1190000011683741)

    -  [vue项目中在绑定图片时如何添加图片域名](https://segmentfault.com/q/1010000009606827)

  - 富文本编辑器图片上传。保存

  - 带步骤的表单验证效果。

- DTO 类添加必要的表单验证 和字段注释
- 完善业务流程需要的功能。
- 完成前端界面的功能开发。和对接。
- 调试接口。整理除了增删改查之外。还需要开发的功能接口。
- 分类管理开发
  - 分类列表使用 table-tree懒加载
  - 编辑分类只返回当前分类的父级分类树。
  - **使用 watch 或者 computed  来修改子组件父级分类的值**
  - 区分全局组件 和 模块组件。
    - 暂时作为模块组件使用。开发库存等用到的时候然后 复制一个全局组件使用。
  - 设置父级分类可以为空
    - 取消为空的判断也能够保存
  - 树节点的值。修改。本地修改列表中的值
    - 通过从重新加载
  - [【VUE-01】报错信息“Cannot read property ‘reduce‘ of null”](https://blog.csdn.net/m0_37374307/article/details/103970728)
    - list 数据 默认值为null  会报错。
  - 清除调试的数据。清楚没有必要的打印的数据
  - 父子组件使用的问题
    - [vue中ref的使用（this.$refs获取为空）](https://blog.csdn.net/dwf_H/article/details/79837861)
    - [[vue父组件如何修改子组件的值，不是通过props传值](https://segmentfault.com/q/1010000019212890)](https://segmentfault.com/q/1010000019212890)
      - 修改子组件的值
    - [vue.nextTick()方法的使用详解（简单明了）](https://blog.csdn.net/zhouzuoluo/article/details/84752280)
      - 比如ref 对象无法读取到组件的值的时候。可以这么使用。

### 0812 to do list

- 

- abp 源码构建 build 脚本无法运行

  - [ 无法加载文件C:\Users\xxx\AppData\Roaming\npm\nodemon.ps1。未对文件...进行数字签名。无法在当前系统上运行该脚本。](https://blog.csdn.net/weixin_42199245/article/details/106199624)
  - 

- 身份认证模块前端需要修改的

  - 不是admin账号无法登陆 

    - abp 框架 前端 也把 abp angu identity 的包全部都模块化封装了。

    - 用户登录修改完善。使用identi接口

      - 使用odic-client 前端来验证 登录。

    - 完善添加租户登录的功能。

    - 完善测试权限功能。

    - # [IdentityServer4-前后端分离之Vue（七）](https://www.cnblogs.com/FireworksEasyCool/p/10576911.html)

    - abp ef 迁移命令 修改命令

      - ```
        Add-Migration "identity-change"
        Update-Database
        ```
      ```
        
      - [Net Core Add-Migration Build failed.最全解决排查方案](https://www.cnblogs.com/TSir/p/12152392.html)  如果解决方案不能够编译成功。。那么会build失败
      
      - # [: Error: Invalid grant type for client: implicit](https://stackoverflow.com/questions/50487639/authorizerequestvalidator-error-invalid-grant-type-for-client-implicit)
      ```

    - [abp id4配置](https://cloud.tencent.com/developer/article/1582264)

    - [vue使用odic配置说明](https://blog.csdn.net/u012869793/article/details/115393178)

    - # [Abp VNext微服务-从身份认证及授权开始(一)](https://www.cnblogs.com/liujiabing/p/14077818.html)

    - [id4-abp 配置获取token授权登录。](https://www.cnblogs.com/william-xu/p/11606117.html)

    - [abp博客教程](https://www.cnblogs.com/william-xu/category/1557954.html)

  - 审计页面优化 搜索。导出  done

    - [vue中将（2019-08-27T15:11:24.172+08:00）时间格式转换成常用日期格式](https://blog.csdn.net/weixin_43931617/article/details/100115899)
    - [momentjs时间格式转换](http://momentjs.cn/) 通过过滤器来时间格式话
    - 前端日志关闭再搜索。不能还原。

  - 切换租户功能完成。

    - 切换租户比较简单，就是根据输入的租户名称获取到租户ID，然后调用`/abp/application-configuration`接口，把租户ID放到请求Header的**__tenant**字段中即可，之后的请求中也需要这个参数，不传的话就是默认的宿主端。
    - [多租户切换功能实现](https://www.cnblogs.com/xhznl/p/13566246.html#%E7%A7%9F%E6%88%B7%E5%88%87%E6%8D%A2)
    - 和用户认证没有关系只要在head里面上传就可以。

  - **给用户，租户添加自定义字段** 后端

    - 数据层如何给原来的类添加自定义字段。

  - 组织界面显示列表。组织树增加显示的字段。 组织树编辑完成不要收缩

    - 添加创建时间,最后修改时间。
    - 设置默认展开 添加删除组织功能
    - 不是默认展开。而是修改完成之后。前端本地数据没有更新。
    - 如何找到对应的这个树节点，然后修改掉他的值。
    - **[element-ui 表格树编辑后 本地刷新](https://www.google.com.hk/search?q=element-ui+%E8%A1%A8%E6%A0%BC%E6%A0%91%E7%BC%96%E8%BE%91%E5%90%8E+%E6%9C%AC%E5%9C%B0%E5%88%B7%E6%96%B0&newwindow=1&biw=1827&bih=921&sxsrf=ALeKk02YmUKZCrKBWmgUSqJ3LNrw_So1cA%3A1628853971906&ei=01YWYd_kNpjO-QaLx47wBQ&oq=element-ui+%E8%A1%A8%E6%A0%BC%E6%A0%91%E7%BC%96%E8%BE%91%E5%90%8E+%E6%9C%AC%E5%9C%B0%E5%88%B7%E6%96%B0&gs_lcp=Cgdnd3Mtd2l6EAM6BQgAEM0CSgUIOhIBMUoECEEYAVC-tANY79IDYMvVA2gBcAB4AIABsAGIAYcPkgEEMC4xNZgBAKABAqABAcABAQ&sclient=gws-wiz&ved=0ahUKEwifmovz8a3yAhUYZ94KHYujA14Q4dUDCA4&uact=5)  需要有空研究一下 **

  - 用户列表显示。用户过滤添加组织树。

  - **添加锁定。修改密码功能。**

    - 在数据层如何增加repository ?
    - 如何增加数据层的方法来调用?
    - 如何继承原来的仓储层方法。?

  - **使用abp实际开发一点功能模块。熟悉使用。**

    - 添加一个产品，订单，订单明细 基础的多对多关系

- ABP当中的权限定义和使用。

- 整理一个ABP框架使用的常见问题列表。

- 完善abp 基础模块的功能

  - 使用服务器上的数据库作为测试库。不要使用本地的数据库
    - 好处开发的时候就有测试数据。不需要重新再来生成测试数据。
  - 如何在仓储层扩展原来的代码。给原来的实体类添加字段。
  - task await list  如何来使用。。
    - task  将原来的返回对象做一个封装， async 和 await 需要成对来出现。
  - abp权限配置使用。如何来配置权限。 权限配置步骤
    - 更加模块来定义全系。。比如identity  是一个模块 下面可以定义 user, role ,org的权限
    - 在代码中配置好要用到哪些权限
    - 然后使用权限。
    - 获取权限列表。更新授权权限。请求过滤判断是否授权。
  - 添加权限的时候。组织机构的权限没有显示出来。
  - 新添加组织权限显示的名称不正确。
    - 显示的都是英文。注意请求头传递参数 accept-language:zh-Hans

- 如何来继承和拓展之前abp的模块

  - 实现接口的意义  就是这个类 所有继承的接口里面有的方法都需要 实现一遍
  - 注意使用resful 风格的api 查询 get 修改单条使用put
  - put请求参数传递。 put和 params参数和body 里面一起来传递参数。
  - 需要找出登录使用的是哪个service
  - 仓储层添加添加扩展方法。来提交代码
  - 找到api 和配置接口 如何扩展和继承使用原来的方法。

- 如何来在abp 的基础上拓展原来的接口。

  - 继承abp 原有的service 扩展功能
  - 在service 里面写好扩展要用的方法。post新建 put修改
  - 在控制器层。将路由定义成和原来的的地方。
  - [restful风格API](https://www.jianshu.com/p/73d2415956bd)  概念理解

- - - -

  

- swagger接口导入postman 结构乱

- 

- abp 跨域问题

  - 本地调试不会跨域。。
  - 线上域名访问就跨域。。本地访问线上也会跨域。
    - 可能是ABP还有其他的地方要配置 identityserver 的时候。数据库可能不一样。
    - 之前使用tiger_fe域名有部署过。是可以使用。
  - 原因。identity server 的种子数据库正式库没有更新。授权认证的数据是测试库的。本地的端口就无法使用了。
  - [asp.net core 跨域问题](https://www.cnblogs.com/stulzq/p/9392150.html)
  - 服务器 IIS6 比较老 不支持 put delete 添加了 webconfig的配置。导致本地跨域



- 


- 





- 

  - 





- 

