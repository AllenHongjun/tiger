/**
 * 类    名：BlogModule   
 * 作    者：花生了什么树       
 * 创建时间：2021/8/16 22:01:00       
 * 说    明: 
 * 
 */

using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Modularity;

namespace Tiger.Books.BlogModule
{
    /// <summary>
    /// 由于ABP是一个模块化框架,因此每个模块都定义它自己的服务并在它自己的单独模块类中通过依赖注入进行注册.例:
    /// </summary>
    public class BlogModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            //在此处注入依赖项

            //一旦跳过自动注册,你应该手动注册你的服务.在这种情况下,AddAssemblyOf扩展方法可以帮助你依照约定注册所有服务.例:
            context.Services.AddAssemblyOf<BlogModule>();



            /*
             固有的注册类型
            一些特定类型会默认注册到依赖注入.例子:

            模块类注册为singleton.
            MVC控制器（继承Controller或AbpController）被注册为transient.
            MVC页面模型（继承PageModel或AbpPageModel）被注册为transient.
            MVC视图组件（继承ViewComponent或AbpViewComponent）被注册为transient.
            应用程序服务（实现IApplicationService接口或继承ApplicationService类）注册为transient.
            存储库（实现IRepository接口）注册为transient.
            域服务（实现IDomainService接口）注册为transient.
             
             
             */




            //注册一个singleton实例
            context.Services.AddSingleton<TaxCalculator>(new TaxCalculator(taxRatio: 0.18));

            //注册一个从IServiceProvider解析得来的工厂方法
            context.Services.AddScoped<ITaxCalculator>(sp => (ITaxCalculator)sp.GetRequiredService<TaxCalculator>());
        }

        /// <summary>
        /// ABP引入了依照约定的服务注册.依照约定你无需做任何事,它会自动完成.如果要禁用它,你可以通过重写PreConfigureServices方法,设置SkipAutoServiceRegistration为true.
        /// </summary>
        /// <param name="context"></param>
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            SkipAutoServiceRegistration = true;
        }
    }
}
