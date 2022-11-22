using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Tiger.Infrastructure.Timing;

namespace Tiger.Infrastructure.DependecyInjection.BlogModule
{
    public class TaxAppService 
    {
        private readonly ITaxCalculator _taxCalculator;

        // 注入依赖关系的3中方式

        //构造方法注入
        internal TaxAppService(ITaxCalculator taxCalculator)
        {
            // TaxAppService在构造方法中得到ITaxCalculator.依赖注入系统在运行时自动提供所请求的服务.
            _taxCalculator = taxCalculator;
        }

        public void DoSomething()
        {
            //...使用 _taxCalculator...
        }
    }



    public class MyService : ITransientDependency
    {
        // Microsoft依赖注入库不支持属性注入.但是,ABP可以与第三方DI提供商（例如Autofac）集成,以实现属性注入
        // 对于属性注入依赖项,使用公开的setter声明公共属性.这允许DI框架在创建类之后设置它.
        public ILogger<MyService> Logger { get; set; }


        private readonly IServiceProvider _serviceProvider;

        public MyService(IServiceProvider serviceProvider)
        {
            //Logger就是这样的依赖项,MyService可以继续工作而无需日志记录.
            Logger = NullLogger<MyService>.Instance;

            // 你可以将IServiceProvider注入到你的类并使用GetService方法
            _serviceProvider=serviceProvider;
        }

        public void DoSomething()
        {
            //...使用 Logger 写日志...

            var taxCalculator = _serviceProvider.GetService<ITaxCalculator>();
            //...


            // 如果你从IServiceProvider解析了服务,在某些情况下,你可能需要注意释放服务.
            using (var scope = _serviceProvider.CreateScope())
            {
                var service1 = scope.ServiceProvider.GetService<IMyService1>();
                var service2 = scope.ServiceProvider.GetService<IMyService2>();
            }


        }
    }

    internal interface IMyService2
    {
    }

    internal interface IMyService1
    {
    }
}
