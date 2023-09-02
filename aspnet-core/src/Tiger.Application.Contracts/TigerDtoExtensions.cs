using System;
using Volo.Abp.Identity;
using Volo.Abp.ObjectExtending;
using Volo.Abp.Threading;

namespace Tiger
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// 实体扩展 https://docs.abp.io/zh-Hans/abp/latest/Object-Extensions
    /// </remarks>
    public static class TigerDtoExtensions
    {
        private static readonly OneTimeRunner OneTimeRunner = new OneTimeRunner();

        public static void Configure()
        {
            OneTimeRunner.Run(() =>
            {
                /* You can add extension properties to DTOs
                 * defined in the depended modules.
                 *
                 * Example:
                 *
                 * ObjectExtensionManager.Instance
                 *   .AddOrUpdateProperty<IdentityRoleDto, string>("Title");
                 *
                 * See the documentation for more:
                 * https://docs.abp.io/en/abp/latest/Object-Extensions
                 */

                // 用户dto实体类添加扩展字段 Dto也需要添加扩展属性，不然就算你实体中已经有了新字段，但接口依然获取不到。
                ObjectExtensionManager.Instance
                    .AddOrUpdateProperty<string>(
                        new[]
                        {
                            typeof(IdentityUserDto),
                            typeof(IdentityUserCreateDto),
                            typeof(IdentityUserUpdateDto),
                            typeof(ProfileDto),
                            typeof(UpdateProfileDto)
                        },
                        "Avatar",
                        options =>
                        {   
                            //添加默认值
                            options.DefaultValueFactory = () => "";
                        }
                    )
                    .AddOrUpdateProperty<string>(
                        new[]
                        {
                            typeof(IdentityUserDto),
                            typeof(IdentityUserCreateDto),
                            typeof(IdentityUserUpdateDto),
                            typeof(ProfileDto),
                            typeof(UpdateProfileDto)
                        },
                        "Introduction",
                        options =>
                        {
                            options.DefaultValueFactory = () => "";
                        }
                    );

            });
        }
    }
}