using System;
using System.Collections.Generic;
using System.Text;

namespace Tiger.Infrastructure.DependecyInjection.BlogModule
{
    internal class TaxCalculator
    {
        /*
         如果实现这些接口,则会自动将类注册到依赖注入:

            ITransientDependency 注册为transient生命周期.
            ISingletonDependency 注册为singleton生命周期.
            IScopedDependency 注册为scoped生命周期.
         
         */
    }
}
