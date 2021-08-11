# Tiger Admin
## 基于 vue-template-admin abp vnext 开发的基础后台项目

> 学习abp 和 vue 的入门项目

[演示地址](http://tiger_fe.hongjy.cn/)

[接口地址](https://localhost:44306/swagger/index.html?docExpansion=none)



## 开发一些后台的通用功能。打架一个基于框架的脚手架项目。

- 列表头部处理 开发一节通用的结构和模式。
- 参考vol开发一些通用的后台功能





### 官方文档-使用教程

- [abp官方文档](https://docs.abp.io/zh-Hans/abp/latest/Module-Development-Basics)
- [awesome-abp](https://github.com/EasyAbp/awesome-abp)
- [abp对接vue的思路](https://www.cnblogs.com/xhznl/tag/ABP%20vNext/)
- [基于 abp vNext 和 .NET Core 开发博客项目 - 使用Redis缓存数据](https://www.cnblogs.com/meowv/p/12956696.html)



### 参考项目

- [vue-elemnt-admin-demo](https://panjiachen.github.io/vue-element-admin/#/login?redirect=%2Fdashboard)
- [volcore快速业务开发框架](http://www.volcore.xyz/#/home)
- [crmeb商城](https://pro.crmeb.net/admin/home/)
- [EasyAbp](https://github.com/EasyAbp)
- [abp-vue-admin-element-typescript](https://github.com/colinin/abp-vue-admin-element-typescript)
- [HelloAbp](https://github.com/xiajingren/HelloAbp)



### 使用的主要技术

> 采用现阶主流技术实现

### 后端技术

| 技术            | 版本    | 说明                        |
| --------------- | ------- | --------------------------- |
| Asp.Net core    |         | MVC框架                     |
| IDentityServer4 |         | 认证和授权框架              |
| EntityFreamwork |         | ORM框架                     |
| ABP Freamwork   | 3.2.1   | 基于Asp.Net coreweb应用框架 |
| AutoFac         |         | 容器                        |
| Swagger-UI      |         | 文档生产工具                |
| Elasticsearch   | 7.6.2   | 搜索引擎                    |
| RabbitMq        | 3.7.14  | 消息队列                    |
| Redis           | 5.0     | 分布式缓存                  |
| MongoDb         | 4.2.5   | NoSql数据库                 |
| Docker          | 18.09.0 | 应用容器引擎                |
|                 |         | 数据库连接池                |
| OSS             | 2.5.0   | 对象存储                    |
| JWT             | 0.9.0   | JWT登录支持                 |
|                 |         | 简化对象封装工具            |



#### 前端技术

| 技术                                                         | 说明             | 官网                                                  |      |
| :----------------------------------------------------------- | ---------------- | ----------------------------------------------------- | ---- |
|                                                              |                  |                                                       |      |
| [vue](https://cn.vuejs.org/index.html)                       | 前端框架         | https://vuejs.org/                                    |      |
| [vue-router](https://next.router.vuejs.org/)                 | 路由框架         | https://router.vuejs.org/                             |      |
| [vuex](https://vuex.vuejs.org/zh/guide/)                     | 全局状态管理框架 | https://vuex.vuejs.org/                               |      |
| Element                                                      | 前端UI框架       | [https://element.eleme.io](https://element.eleme.io/) |      |
| [axios](https://axios-http.com/zh/)                          | 前端HTTP框架     | https://github.com/axios/axios                        |      |
| [echarts 4.1.3](https://echarts.apache.org/v4/examples/zh/editor.html?c=pie-legend) | 图表框架         | https://v-charts.js.org/                              |      |
| Js-cookie                                                    | cookie管理工具   | https://github.com/js-cookie/js-cookie                |      |
| nprogress                                                    | 进度条控件       | https://github.com/rstacruz/nprogress                 |      |
| [vue-cli](https://cli.vuejs.org/zh/)                         |                  |                                                       |      |
| [mockjs](http://mockjs.com/)                                 |                  |                                                       |      |
| [vue-admin-template](https://github.com/PanJiaChen/vue-admin-template/blob/master/README-zh.md) |                  |                                                       |      |
| [vue-elment-admin](https://panjiachen.github.io/vue-element-admin-site/zh/) |                  |                                                       |      |
| [elemnt-ui](https://element.eleme.cn/2.13/#/zh-CN)           |                  |                                                       |      |
|                                                              |                  |                                                       |      |



## 环境搭建

### 开发工具

| 工具          | 说明                | 官网                                                  |
| ------------- | ------------------- | ----------------------------------------------------- |
| Visual Studio | 开发IDE             | https://www.jetbrains.com/idea/download               |
| RedisDesktop  | redis客户端连接工具 | https://github.com/qishibo/AnotherRedisDesktopManager |
| Robomongo     | mongo客户端连接工具 | https://robomongo.org/download                        |
| SwitchHosts   | 本地host管理        | https://oldj.github.io/SwitchHosts/                   |
| Putty         | Linux远程连接工具   | http://www.netsarang.com/download/software.html       |
| Navicat       | 数据库连接工具      | http://www.formysql.com/xiazai.html                   |
| PowerDesigner | 数据库设计工具      | http://powerdesigner.de/                              |
| Axure         | 原型设计工具        | https://www.axure.com/                                |
| XMind         | 思维导图设计工具    | http://www.edrawsoft.cn/mindmaster                    |
| ScreenToGif   | gif录制工具         | https://www.screentogif.com/                          |
| ProcessOn     | 流程图绘制工具      | https://www.processon.com/                            |
| PicPick       | 图片处理工具        | https://picpick.app/zh/                               |
| Snipaste      | 屏幕截图工具        | https://www.snipaste.com/                             |
| Postman       | API接口调试工具     | https://www.postman.com/                              |
| Typora        | Markdown编辑器      | https://typora.io/                                    |



### 开发环境

| 工具          | 版本号 | 下载                                                         |
| ------------- | ------ | ------------------------------------------------------------ |
| .net core     | 3.1.0  | https://www.oracle.com/technetwork/java/javase/downloads/jdk8-downloads-2133151.html |
| SqlServer     | 2012   | https://www.mysql.com/                                       |
| Redis         | 5.0    | https://redis.io/download                                    |
| MongoDB       | 4.2.5  | https://www.mongodb.com/download-center                      |
| RabbitMQ      | 3.7.14 | http://www.rabbitmq.com/download.html                        |
| Nginx         | 1.10   | http://nginx.org/en/download.html                            |
| Elasticsearch | 7.6.2  | https://www.elastic.co/downloads/elasticsearch               |
| Logstash      | 7.6.2  | https://www.elastic.co/cn/downloads/logstash                 |
| Kibana        | 7.6.2  | https://www.elastic.co/cn/downloads/kibana                   |



### 搭建步骤

> Windows环境部署

- Windows环境搭建



> Linux 部署



> dock部署



### 前端



### 