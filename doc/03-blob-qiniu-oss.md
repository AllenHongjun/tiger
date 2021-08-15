# oss blob qiniu 七牛 对象存储 abp 

- 
- abp 对接七牛云文件存储 这个估计要2天**
  - [七牛开发者中心](https://developer.qiniu.com/kodo)
  - [七牛第三方sdk](https://github.com/qiniu/csharp-sdk)
  - [七牛sdk使用说明](https://developer.qiniu.com/kodo/1237/csharp)
  - [七牛.net core sdk](https://www.cnblogs.com/omango/p/8447480.html)
  - 对接开发一些第三方的服务接口。
  - [七牛使用教程](https://blog.csdn.net/guoer9973/article/details/44410959)
  - [hexo 博客配置使用七牛上传图片管理工具](https://marvae.github.io/2017-12-01/qiqiu/)
  - [asp.net core文件上传](https://blog.csdn.net/wf824284257/article/details/102880064)
  - [asp.net  单文件，多文件上传](https://www.cnblogs.com/Can-daydayup/p/12637100.html)
  - [dot.net core  获取网站根路径](https://www.cnblogs.com/wintertone/p/12906464.html)
  - [blob源码分析](https://www.cnblogs.com/myzony/p/13387382.html)
  - [blob官方使用教程](https://docs.abp.io/zh-Hans/abp/3.2/Blob-Storing)
  - 优化的点
    - 区域设置代码里面写死了。
    - 生成的文件key 没有带上后缀。不知道文件类型。
    - 
- 租户的数据没有做隔离。 给租户添加用户

  - 通过状态来前端缓存租户信息。然后请求的时候带上租户的信息
- 学习abp 的源码 demo 熟悉使用。