using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.DependencyInjection;

namespace Tiger.Infrastructure.DependecyInjection.BlogModule
{
    [Dependency(ReplaceServices = true)]
    [ExposeServices(typeof(ITaxCalculator))]
    public class TaxCalculato3 : ICalculator, ITaxCalculator, ICanCalculate, ITransientDependency
    {

        //虽然继承了其他的类 但是只能注入公开的类
        // ExposeServicesAttribute用于控制相关类提供了什么服务
        // TaxCalculator类只公开ITaxCalculator接口.这意味着你只能注入ITaxCalculator,但不能注入TaxCalculator或ICalculator到你的应用程序中.
    }
}
