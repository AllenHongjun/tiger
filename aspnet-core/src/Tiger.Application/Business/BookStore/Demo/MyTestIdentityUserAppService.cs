using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.Abp.Identity;
using Volo.Abp.Validation;

namespace Tiger.Books.Demo
{
    ///// <summary>
    ///// 大多数情况 是继承 父类的服务。然后扩展 重写父类的方法。 或者添加自己的方法 来扩展自己的服务
    ///// </summary>
    ////[RemoteService(IsEnabled = false)] // 如果你在使用动态控制器,为了避免为应用服务创建重复的控制器, 你可以禁用远程访问.
    //[Dependency(ReplaceServices = true)]
    //[ExposeServices(typeof(IIdentityUserAppService), typeof(IdentityUserAppService), typeof(MyIdentityUserAppService))]
    //public class MyTestIdentityUserAppService : IdentityUserAppService
    //{
    //    //...
    //    public MyTestIdentityUserAppService(
    //        IdentityUserManager userManager,
    //        IIdentityUserRepository userRepository,
    //        IIdentityRoleRepository identityRoleRepository,
    //        IGuidGenerator guidGenerator
    //    ) : base(
    //        userManager,
    //        userRepository,
    //        identityRoleRepository)
    //    {
    //    }

    //    public override async Task<IdentityUserDto> CreateAsync(IdentityUserCreateDto input)
    //    {
    //        if (input.PhoneNumber.IsNullOrWhiteSpace())
    //        {
    //            throw new AbpValidationException(
    //                "Phone number is required for new users!",
    //                new List<ValidationResult>
    //                {
    //                new ValidationResult(
    //                    "Phone number can not be empty!",
    //                    new []{"PhoneNumber"}
    //                )
    //                }
    //            );
    //        }

    //        // 然后调用了基类方法继续基本业务逻辑.
    //        // 你也可以完全重写整个业务逻辑去创建用户,而不是调用基类方法.
    //        return await base.CreateAsync(input);
    //    }
    //}
}
