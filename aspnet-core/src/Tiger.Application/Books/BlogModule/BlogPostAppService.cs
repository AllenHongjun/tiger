/**
 * 类    名：BlogPostAppService   
 * 作    者：花生了什么树       
 * 创建时间：2021/8/16 22:04:18       
 * 说    明: 
 * 
 */

using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;

namespace Tiger.Books.BlogModule
{
    /// <summary>
    /// BlogPostAppService 由于它是从已知的基类派生的,因此会自动注册为transient生命周期.
    /// </summary>
    public class BlogPostAppService:ApplicationService
    {
    }

    /*
     如果实现这些接口,则会自动将类注册到依赖注入:

    ITransientDependency 注册为transient生命周期.
    ISingletonDependency 注册为singleton生命周期.
    IScopedDependency 注册为scoped生命周期.
     
     */

    public interface ITaxCalculator
    {

    }

    public class TaxCalculator : ITransientDependency, ITaxCalculator
    {
        public TaxCalculator(double taxRatio)
        {
            this.taxRatio = taxRatio;
        }

        public double taxRatio { get; set; }
    }

    //public class TaxCalculator2 : ISingletonDependency
    //{

    //}

    ///// <summary>
    ///// ExposeServicesAttribute用于控制相关类提供了什么服务.例:  显示的制定相关的类提供什么样子的服务
    ///// </summary>
    //[ExposeServices(typeof(ITaxCalculator))]
    //public class TaxCalculator3 : IScopedDependency
    //{

    //}

    ///// <summary>
    ///// 
    ///// Singleton  指定将创建服务的单个实例。
    ///// Transient或Scoped  指定将为每个范围创建服务的新实例。
    ///// Lifetime: 注册的生命周期:Singleton,Transient或Scoped.
    /////TryRegister: 设置true则只注册以前未注册的服务.使用IServiceCollection的TryAdd...扩展方法.
    /////ReplaceServices: 设置true则替换之前已经注册过的服务.使用IServiceCollection的Replace扩展方法.
    ///// 
    ///// Transient  指定每次创建服务的新实例
    ///// </summary>
    //[Dependency(ServiceLifetime.Transient, ReplaceServices = true)]
    //public class TaxCalculator4
    //{

    //}
}
