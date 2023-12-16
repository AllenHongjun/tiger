using Microsoft.EntityFrameworkCore;
using System;
using Tiger.Volo.Abp.Identity.Users;
using Volo.Abp.Identity;
using Volo.Abp.ObjectExtending;
using Volo.Abp.TenantManagement;
using Volo.Abp.Threading;

namespace Tiger.EntityFrameworkCore
{
    public static class TigerEfCoreEntityExtensionMappings
    {
        private static readonly OneTimeRunner OneTimeRunner = new OneTimeRunner();

        public static void Configure()
        {
            TigerGlobalFeatureConfigurator.Configure();
            TigerModuleExtensionConfigurator.Configure();

            OneTimeRunner.Run(() =>
            {
                /* You can configure extra properties for the
                 * entities defined in the modules used by your application.
                 *
                 * This class can be used to map these extra properties to table fields in the database.
                 *
                 * USE THIS CLASS ONLY TO CONFIGURE EF CORE RELATED MAPPING.
                 * USE TigerModuleExtensionConfigurator CLASS (in the Domain.Shared project)
                 * FOR A HIGH LEVEL API TO DEFINE EXTRA PROPERTIES TO ENTITIES OF THE USED MODULES
                 *
                 * Example: Map a property to a table field:

                     ObjectExtensionManager.Instance
                         .MapEfCoreProperty<IdentityUser, string>(
                             "MyProperty",
                             (entityBuilder, propertyBuilder) =>
                             {
                                 propertyBuilder.HasMaxLength(128);
                             }
                         );

                 * See the documentation for more:
                 * https://docs.abp.io/en/abp/latest/Customizing-Application-Modules-Extending-Entities
                 */

                // AbpUser表添加扩展字段
                ObjectExtensionManager.Instance
                    .MapEfCoreProperty<IdentityUser, string>(
                        "Avatar",
                        (entityBuilder, propertyBuilder) =>
                        {
                            propertyBuilder.HasMaxLength(AppUserConsts.MaxAvatarLength);
                        }
                    )
                    .MapEfCoreProperty<IdentityUser, string>(
                        "Introduction",
                        (entityBuilder, propertyBuilder) =>
                        {
                            propertyBuilder.HasMaxLength(AppUserConsts.MaxIntroductionLength);
                        }
                    );




                ObjectExtensionManager.Instance
                .MapEfCoreProperty<Tenant, bool>(
                    nameof(Volo.Abp.Sass.Tenants.Tenant.IsActive),
                    (entityBuilder, propertyBuilder) =>
                    {
                        propertyBuilder.IsRequired().HasComment("是否激活");
                    }
                )
                .MapEfCoreProperty<Tenant, DateTime>(
                    nameof(Volo.Abp.Sass.Tenants.Tenant.EnableTime),
                    (entityBuilder, propertyBuilder) =>
                    {
                        propertyBuilder.HasComment("启用时间");
                    }
                )
                .MapEfCoreProperty<Tenant, DateTime>(
                    nameof(Volo.Abp.Sass.Tenants.Tenant.DisableTime),
                    (entityBuilder, propertyBuilder) =>
                    {
                        propertyBuilder.HasComment("截止时间");
                    }
                )
                .MapEfCoreProperty<Tenant, Guid>(
                    nameof(Volo.Abp.Sass.Tenants.Tenant.EditionId),
                    (entityBuilder, propertyBuilder) =>
                    {
                        propertyBuilder.HasComment("版本Id");
                    }
                );
            });
        }
    }
}
