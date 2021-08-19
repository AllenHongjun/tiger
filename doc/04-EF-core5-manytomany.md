EF core 



## 

- 基本使用 安装包 创建连接

- 创建实体类
  - 配置熟悉 限制 长度 默认值 
  - 配置关系 
  - code first 生成数据库。
  - 手动生成数据库 多对多关系查询。
  
- 查询数据
  - 基本查询
  - 复杂查询
  - **[复杂查询](https://docs.microsoft.com/zh-cn/ef/core/querying/complex-query-operators)**
  
- 保存（修改删除添加）数据

- 其他
  - 日志
  - 跟踪
  
- LINQ查询关键字

  - > 所有能用sql 语句查询的 都能够使用linq 来实现  查询扩展方法的实例和用法。 demo 

  - [Enumerable 类 查询扩展方法 api demo 示例](https://docs.microsoft.com/zh-cn/dotnet/api/system.linq.enumerable?view=net-5.0)

  - [into 子句临时存储查询结果](https://docs.microsoft.com/zh-cn/dotnet/csharp/language-reference/keywords/into)

  - [C#-linq 查询关键字参考](https://docs.microsoft.com/zh-cn/dotnet/csharp/language-reference/keywords/query-keywords)

### 问题

> 基础的 linq用法 还不扎实呀。。很多查询。都不知道用linq如何来实现。有了   join group group join 就可以实现 所有存储过程里的查询了。

- linqpad 绿色 智能提示板 安装包 找不到
- linq groupjoin 如何来写 有什么做用
  - group join 和  left join还是有很大的不同 group join 相当于一对多 筛选1个 和这一个分组下的所有的元素
- linq select many 如何实现如何来使用
- linq 分组查询 里面 group  如何来查询使用
- linq join into group 如何来使用查询。这个又什么作用。
- linq 查询扩展方法的学习 资料 源码 在哪里？





- [ASP.NET Core 中的 Razor Pages 和 Entity Framework Core](https://docs.microsoft.com/zh-cn/aspnet/core/data/ef-rp/intro?view=aspnetcore-5.0&tabs=visual-studio)
- [EntityFramework.Docs](https://github.com/dotnet/EntityFramework.Docs)
- [abp 使用EF 领域层搭建](https://docs.abp.io/en/abp/3.2/Tutorials/Part-6?UI=NG&DB=EF)
- [EF官方使用文档](https://docs.microsoft.com/zh-cn/ef/)
- 一对多
- 多对一
- 多对多教程
  - EF 1对多  hasmany 这个又点搞不清楚
- 问题
  - 软件包不兼容 使用.net 50
  - https://stackoverflow.com/questions/64144137/package-microsoft-entityframeworkcore-sqlite-5-0-0-rc-1-20451-13-is-not-compatib





### 工作备忘

- ECC_SplitCouponDiscountAmount  优惠券折扣平台。多张全循环计算返回。
- 之前的退款逻辑。。如何退款。。拆分了优惠券。。退款金额如何退款。