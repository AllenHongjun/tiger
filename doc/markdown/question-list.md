-   
- [vue element-ui列表中el-switch 开关,使用0和1](https://blog.csdn.net/qq_35430000/article/details/99760226)
- 新添加的数据。无法立即更新因为tempdata 的id数据错误。需要根据返回的数据重新赋值id
- 
- 订单管理数据查询
  - 如何管理查询来筛选数据
  - auto mapper 如何关联显示 关联表的字段。
  - 如何注入生成订单号的代码
  - 采购模块业务流程
    - 如何到货。如何退货。采购单。退货单。表结构设计。
      - 需求功能分析清楚。表架构设计好。流程搞清楚再开发。
  - 优化完善的功能
    - 时间过滤
    - 根据收货人 姓名过滤
      - 关联数据查询 。在 repository 这一层 关联多张表查询的数据 如何返回。返回的类定义在哪里
      - 可以在 service 这一层 来 写linq 关联多张表的查询
      - 单张表的数据 可以通过导航属性。include 或者 关联查询。筛选出单个表 的数据 然后通过 automapper 来对应需要显示的字段
    - 显示自动生成的单号
    - 付款时间为付款显示为空
    - **支付方式类型 不同的状态显示不同的颜色**
    - **批量发货 批量关闭功能**
    - 显示昵称
    - 优化 获取分页数量的方式 方法
      - 过滤条件的方法和合并抽取一个公用的方法
      - 或者totalCount 使用 out 参数输出出来
    - 整理一篇automapper 的使用教程
      - [automapper使用教程](https://www.cnblogs.com/mushroom/p/4291975.html)
    - 





### EF core 领域驱动模型开发方式

- 不要使用导航属性来开发 数据库关联模型的查询使用 join 关联查询 写在EF core这一层里面 
- 创建 对象的时候 不要使用一个空的构造函数。。使用一个带参数的构造函数来赋值创建对象。。
- new 一个对象的时候。都是通过带参数的构造函数。


- Manager 这个类的作用。。可以放置一些 IRepository 的组合 在一起 处理。
- Service 里面 不要放数据库 查询相关的操作
- 使用使用外键的取舍。

  - 数据库正式库 可以不建立外键 。但是使用 导航属性或者linq 也是可以关联的。
- 设计数据的操作的。全部写在 仓储这一层里面处理

  - 关联查询 关联删除 增加 修改 等操作。
- 函数的参数比较的多的时候 就一行写一个。这样就整齐清楚。
- 比较核心的操作就是 数据库相关的操作。关联查询 关联插入。然后把这些操作 移动到IRepository 这一层里面操作。使用
- 绝大部分的操作。肯定是 和数据有关的操作。。这一步分的代码需要多加完善




- 

- - ```
  the instance of entity type 'OrderItem' cannot be tracked because another instance with the same key value for {'Id'} is already being tracked. When attaching existing entities, ensure that only one entity instance with a given key value is attached. Consider using 'DbContextOptionsBuilder.EnableSensitiveDataLogging' to see the conflicting key values.
  System.InvalidOperationException: The instance of entity type 'OrderItem' cannot be tracked because another instance with the same key value for {'Id'} is already being tracked. When attaching existing entities, ensure that only one entity instance with a given key value is attached. Consider using 'DbContextOptionsBuilder.EnableSensitiveDataLogging' to see the conflicting key values.
     at Microsoft.EntityFrameworkCore.ChangeTracking.Internal.IdentityMap`1.ThrowIdentityConflict(InternalEntityEntry entry)
     at Microsoft.EntityFrameworkCore.ChangeTracking.Internal.IdentityMap`1.Add(TKey key, InternalEntityEntry entry, Boolean updateDuplicate)
     at Microsoft.EntityFrameworkCore.ChangeTracking.Internal.IdentityMap`1.Add(TKey key, InternalEntityEntry entry)
     at Microsoft.EntityFrameworkCore.ChangeTracking.Internal.IdentityMap`1.Add(InternalEntityEntry entry)
     at Microsoft.EntityFrameworkCore.ChangeTracking.Internal.StateManager.StartTracking(InternalEntityEntry entry)
     at Microsoft.EntityFrameworkCore.ChangeTracking.Internal.InternalEntityEntry.SetEntityState(EntityState oldState, EntityState newState, Boolean acceptChanges, Boolean modifyProperties)
     at Microsoft.EntityFrameworkCore.ChangeTracking.Internal.InternalEntityEntry.SetEntityState(EntityState entityState, Boolean acceptChanges, Boolean modifyProperties, Nullable`1 forceStateWhenUnknownKey)
     at Microsoft.EntityFrameworkCore.ChangeTracking.Internal.EntityGraphAttacher.PaintAction(EntityEntryGraphNode`1 node)
    ```
  ```
  
  ```



## [EF架构~简洁关联表插入，优越的代码性能！](https://www.cnblogs.com/lori/archive/2013/03/07/2947675.html)



-  [instance of entity type cannot be tracked because another instance with same key value is tracked](https://stackoverflow.com/questions/48202403/instance-of-entity-type-cannot-be-tracked-because-another-instance-with-same-key)



- 接口文档 返回的类型是一个数字。然后写清楚 这个是什么含义
  - 前端根据这个注释。灵活的来显示判断。

- 控制器无法注入
  - 原因 ProductAttributeType 直接代码 是 ProductAttributeTpye 重名名之后 还有部分实体类没有修改过来。命名空间也没有修改 。导致不能注入方法。
  - 可能和命名空间中的名字有关系。

```
2021-08-31 09:19:27.680 +08:00 [ERR] An exception was thrown while activating Tiger.Controllers.Admin.Basics.ProductAttributeTypeController.
Autofac.Core.DependencyResolutionException: An exception was thrown while activating Tiger.Controllers.Admin.Basics.ProductAttributeTypeController.
 ---> Autofac.Core.DependencyResolutionException: None of the constructors found with 'Autofac.Core.Activators.Reflection.DefaultConstructorFinder' on type 'Tiger.Controllers.Admin.Basics.ProductAttributeTypeController' can be invoked with the available services and parameters:
Cannot resolve parameter 'Tiger.Basic.ProductAttributeTpyes.IProductAttributeTypeAppService productAttributeTypeAppService' of constructor 'Void .ctor(Tiger.Basic.ProductAttributeTpyes.IProductAttributeTypeAppService)'.
   at Autofac.Core.Activators.Reflection.ReflectionActivator.GetValidConstructorBindings(ConstructorInfo[] availableConstructors, IComponentContext context, IEnumerable`1 parameters)
   at Autofac.Core.Activators.Reflection.ReflectionActivator.ActivateInstance(IComponentContext context, IEnumerable`1 parameters)
   at Autofac.Core.Resolving.InstanceLookup.CreateInstance(IEnumerable`1 parameters)
   --- End of inner exception stack trace ---
   at Autofac.Core.Resolving.InstanceLookup.CreateInstance(IEnumerable`1 parameters)
   at Autofac.Core.Resolving.InstanceLookup.Execute()
   at Autofac.Core.Resolving.ResolveOperation.GetOrCreateInstance(ISharingLifetimeScope currentOperationScope, ResolveRequest request)
   at Autofac.Core.Resolving.ResolveOperation.Execute(ResolveRequest request)
   at Autofac.ResolutionExtensions.TryResolveService(IComponentContext context, Service service, IEnumerable`1 parameters, Object& instance)
   at Autofac.ResolutionExtensions.ResolveService(IComponentContext context, Service service, IEnumerable`1 parameters)
   at Microsoft.AspNetCore.Mvc.Controllers.ServiceBasedControllerActivator.Create(ControllerContext actionContext)
   at Microsoft.AspNetCore.Mvc.Controllers.ControllerFactoryProvider.<>c__DisplayClass5_0.<CreateControllerFactory>g__CreateController|0(ControllerContext controllerContext)
   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.Next(State& next, Scope& scope, Object& state, Boolean& isCompleted)
   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.InvokeInnerFilterAsync()
--- End of stack trace from previous location where exception was thrown ---
   at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.<InvokeNextExceptionFilterAsync>g__Awaited|25_0(ResourceInvoker invoker, Task lastTask, State next, Scope scope, Object state, Boolean isCompleted)
2021-08-31 09:19:27.680 +08:00 [ERR] ---------- Exception Data ----------
ActivatorChain = Tiger.Controllers.Admin.Basics.ProductAttributeTypeController
```

