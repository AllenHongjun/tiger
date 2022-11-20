/**
 * 类    名：TaxAppService   
 * 作    者：花生了什么树       
 * 创建时间：2021/8/16 22:15:53       
 * 说    明: 
 * 
 */

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp;
using Volo.Abp.Application.Services;

namespace Tiger.Books.BlogModule
{
    /// <summary>
    /// 注入依赖关系
    /// 使用已注册的服务有三种常用方法.
    /// 
    /// 继承 autofac 很简单 主要是使用。 有些时候 不是按照规范来依赖注入组件的话 就会无法启用。
    /// 
    /// 
    /// </summary>

    [RemoteService(false)]
    public class TaxAppService : ApplicationService
    {
        private readonly ITaxCalculator _taxCalculator;

        //Microsoft依赖注入库不支持属性注入.但是,ABP可以与第三方DI提供商（例如Autofac）集成,以实现属性注入.例:
        public ILogger<TaxAppService> LoggerTest { get; set; }

        //构造方法注入  这是将服务注入类的最常用方法.例如

        public TaxAppService(ITaxCalculator taxCalculator)
        {
            _taxCalculator = taxCalculator;

            LoggerTest = NullLogger<TaxAppService>.Instance;


            // 释放/处理（Releasing/Disposing）服务

            /*
             如果你使用了构造函数或属性注入,则无需担心释放服务的资源.但是,如果你从IServiceProvider解析了服务,在某些情况下,你可能需要注意释放服务.
             

            ASP.NET Core会在当前HTTP请求结束时释放所有服务,即使你直接从IServiceProvider解析了服务（假设你注入了IServiceProvider）.但是,在某些情况下,你可能希望释放/处理手动解析的服务:

                你的代码在AspNet Core请求之外执行,执行者没有处理服务范围.
                你只有对根服务提供者的引用.
                你可能希望立即释放和处理服务（例如,你可能会创建太多具有大量内存占用且不想过度使用内存的服务）.
                在任何情况下,你都可以使用这样的using代码块来安全地立即释放服务:
             */
        }

        public void DoSomething()
        {
            //...使用 _taxCalculator...
        }


        public void DoSomething2()
        {
            //...使用 Logger 写日志...
        }
    }
}
