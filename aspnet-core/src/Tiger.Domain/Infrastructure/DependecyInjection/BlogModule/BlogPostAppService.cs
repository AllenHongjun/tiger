using System;
using System.Collections.Generic;
using System.Text;

namespace Tiger.Infrastructure.DependecyInjection.BlogModule
{
    internal class BlogPostAppService
    {
        // BlogPostAppService 由于它是从已知的基类派生的,因此会自动注册为transient生命周期.

        /*
         一些特定类型会默认注册到依赖注入.例子:

            模块类注册为singleton.
            MVC控制器（继承Controller或AbpController）被注册为transient.
            MVC页面模型（继承PageModel或AbpPageModel）被注册为transient.
            MVC视图组件（继承ViewComponent或AbpViewComponent）被注册为transient.
            应用程序服务（实现IApplicationService接口或继承ApplicationService类）注册为transient.
            存储库（实现IRepository接口）注册为transient.
            域服务（实现IDomainService接口）注册为transient.
         
         */
    }
}
