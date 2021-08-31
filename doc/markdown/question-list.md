

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

