/**
 * 类    名：MyService   
 * 作    者：花生了什么树       
 * 创建时间：2021/8/16 14:57:12       
 * 说    明: 
 * 
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Settings;
using Volo.Abp.Users;

namespace Tiger.Books.Demo
{
    /// <summary>
    /// 获取当前用户相关的信息
    /// 
    /// 在Web应用程序中检索有关已登录用户的信息是很常见的. 当前用户是与Web应用程序中的当前请求相关的活动用户.
    /// </summary>
    public class MyService : ITransientDependency
    {
        // ICurrentUser 是主要的服务,用于获取有关当前活动的用户信息.
        private readonly ICurrentUser _currentUser;

        private readonly ISettingProvider _settingProvider;

        //Inject ISettingProvider in the constructor
        public MyService(ICurrentUser currentUser, ISettingProvider settingProvider)
        {
            _currentUser = currentUser;

            _settingProvider = settingProvider;
        }

        public void Foo()
        {
            Guid? userId = _currentUser.Id;
            string email = _currentUser.Email;
        }


        public async Task FooAsync()
        {
            //Get a value as string.
            string userName = await _settingProvider.GetOrNullAsync("Smtp.UserName");

            //Get a bool value and fallback to the default value (false) if not set.
            bool enableSsl = await _settingProvider.GetAsync<bool>("Smtp.EnableSsl");

            //Get a bool value and fallback to the provided default value (true) if not set.
            bool enableSsl2 = await _settingProvider.GetAsync<bool>(
                "Smtp.EnableSsl", defaultValue: true);

            //Get a bool value with the IsTrueAsync shortcut extension method
            bool enableSsl3 = await _settingProvider.IsTrueAsync("Smtp.EnableSsl");

            //Get an int value or the default value (0) if not set
            int port = (await _settingProvider.GetAsync<int>("Smtp.Port"));

            //Get an int value or null if not provided
            int? port2 = (await _settingProvider.GetOrNullAsync("Smtp.Port"))?.To<int>();
        }
    }


    /// <summary>
    /// 公共基类已经将此服务作为基本属性注入. 例如你可以直接在应用服务中使用 CurrentUser 属性:
    /// </summary>
    public class MyAppService : ApplicationService
    {
        public void Foo()
        {
            /**
             
             属性
                以下是 ICurrentUser 接口的基本属性:

                IsAuthenticated 如果当前用户已登录(已认证),则返回 true. 如果用户尚未登录,则 Id 和 UserName 将返回 null.
                Id (Guid?): 当前用户的Id,如果用户未登录,返回 null.
                UserName (string): 当前用户的用户名称. 如果用户未登录,返回 null.
                TenantId (Guid?): 当前用户的租户Id. 对于多租户 应用程序很有用. 如果当前用户未分配给租户,返回 null.
                Email (string): 当前用户的电子邮件地址. 如果当前用户尚未登录或未设置电子邮件地址,返回 null.
                EmailVerified (bool): 如果当前用户的电子邮件地址已经过验证,返回 true.
                PhoneNumber (string): 当前用户的电话号码. 如果当前用户尚未登录或未设置电话号码,返回 null.
                PhoneNumberVerified (bool): 如果当前用户的电话号码已经过验证,返回 true.
                Roles (string[]): 当前用户的角色. 返回当前用户角色名称的字符串数组.
                

                Methods
                ICurrentUser 是在 ICurrentPrincipalAccessor 上实现的(请参阅以下部分),并可以处理声明. 实际上所有上述属性都是从当前经过身份验证的用户的声明中检索的.

                如果你有自定义声明或获取其他非常见声明类型, ICurrentUser 有一些直接使用声明的方法.

                FindClaim: 获取给定名称的声明,如果未找到返回 null.
                FindClaims: 获取具有给定名称的所有声明(允许具有相同名称的多个声明值).
                GetAllClaims: 获取所有声明.
                IsInRole: 一种检查当前用户是否在指定角色中的简化方法.
                除了这些标准方法,还有一些扩展方法:

                FindClaimValue: 获取具有给定名称的声明的值,如果未找到返回 null. 它有一个泛型重载将值强制转换为特定类型.
                GetId: 返回当前用户的 Id. 如果当前用户没有登录它会抛出一个异常(而不是返回null). 仅在你确定用户已经在你的代码上下文中进行了身份验证时才使用此选项.

             */
            Guid? userId = CurrentUser.Id;
        }
    }
}



