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

