using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Tiger.Infrastructure.DependecyInjection.BlogModule;
using Volo.Abp.Modularity;

namespace Tiger.Infrastructure.DependecyInjection
{

    /// <summary>
    /// 测试博客模块类
    /// </summary>
    /// <remark>
    /// abp 比较核心的特点就模块话 
    /// 按照业务模块开发功能 比如 订单模块 邮件模块 报表模块 可以按照需求在指定的地方引入自己的需要的组件包
    /// 
    /// 由于ABP是一个模块化框架,因此每个模块都定义它自己的服务并在它自己的单独模块类中通过依赖注入进行注册
    /// </remark>
    internal class BlogModule1:AbpModule
    {

        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            // ABP引入了依照约定的服务注册.依照约定你无需做任何事,它会自动完成.如果要禁用它,你可以通过重写PreConfigureServices方法,设置SkipAutoServiceRegistration为true.
            SkipAutoServiceRegistration = true;
        }



        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            // 由于ABP是一个模块化框架,因此每个模块都定义它自己的服务并在它自己的单独模块类中通过依赖注入进行注册
            base.ConfigureServices(context);

            // 在此处注入依赖项

            //AddAssemblyOf扩展方法可以帮助你依照约定注册所有服务.例:
            context.Services.AddAssemblyOf<BlogModule1>();


            //注册一个singleton实例
            context.Services.AddSingleton<TaxCalculator>(new TaxCalculator());

            //注册一个从IServiceProvider解析得来的工厂方法
            context.Services.AddScoped<ITaxCalculator>(sp => (ITaxCalculator)sp.GetRequiredService<TaxCalculator>());

        }
    }
}
