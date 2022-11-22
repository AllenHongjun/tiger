using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.DependencyInjection;

namespace Tiger.Infrastructure.DependecyInjection.BlogModule
{
    [Dependency(ServiceLifetime.Transient, ReplaceServices = true)]
    internal class TaxCalculator2
    {

        // 配置依赖注入服务的另一种方法是使用DependencyAttribute.它具有以下属性

        /*
         
         Lifetime: 注册的生命周期:Singleton,Transient或Scoped.
         TryRegister: 设置true则只注册以前未注册的服务.使用IServiceCollection的TryAdd ... 扩展方法.
         ReplaceServices: 设置true则替换之前已经注册过的服务.使用IServiceCollection的Replace扩展方法.

         */

    }
}
