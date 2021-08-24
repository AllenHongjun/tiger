/**
 * 类    名：MyIdentityUserManager   
 * 作    者：花生了什么树       
 * 创建时间：2021/8/16 21:47:09       
 * 说    明: 
 * 
 */

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Identity;
using Volo.Abp.Settings;
using Volo.Abp.Threading;
using Volo.Abp.Validation;
using IdentityUser = Volo.Abp.Identity.IdentityUser;

namespace Tiger.Business.Demo
{
    /// <summary>
    /// 示例: 重写领域服务
    /// 
    /// 
    /// 这里需要 [ExposeServices(typeof(IdentityUserManager))] attribute,因为 IdentityUserManager 没有定义接口 (像 IIdentityUserManager) ,依赖注入系统并不会按照约定公开继承类的服务(如已实现的接口).
    /// </summary>
    [Dependency(ReplaceServices = true)]
    [ExposeServices(typeof(IdentityUserManager))]
    public class MyIdentityUserManager : IdentityUserManager
    {
        public MyIdentityUserManager(
            IdentityUserStore store,
            IIdentityRoleRepository roleRepository,
            IIdentityUserRepository userRepository,
            IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<IdentityUser> passwordHasher,
            IEnumerable<IUserValidator<IdentityUser>> userValidators,
            IEnumerable<IPasswordValidator<IdentityUser>> passwordValidators,
            ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors,
            IServiceProvider services,
            ILogger<IdentityUserManager> logger,
            ICancellationTokenProvider cancellationTokenProvider,
            IOrganizationUnitRepository organizationUnitRepository, 
            ISettingProvider settingProvider

            ) :
            base(store,
                roleRepository,
                userRepository,
                optionsAccessor,
                passwordHasher,
                userValidators,
                passwordValidators,
                keyNormalizer,
                errors,
                services,
                logger,
                cancellationTokenProvider,
                organizationUnitRepository,
                settingProvider
                )
        {
        }

        public override async Task<IdentityResult> CreateAsync(IdentityUser user)
        {
            //if (user.PhoneNumber.IsNullOrWhiteSpace())
            //{
            //    throw new AbpValidationException(
            //        "Phone number is required for new users!",
            //        new List<ValidationResult>
            //        {
            //        new ValidationResult(
            //            "Phone number can not be empty!",
            //            new []{"PhoneNumber"}
            //        )
            //        }
            //    );
            //}

            return await base.CreateAsync(user);
        }
    }

}
