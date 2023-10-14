using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Tiger.Module.Notifications.Enums;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Tiger.Module.Notifications
{
    public static class NotificationsDbContextModelCreatingExtensions
    {
        public static void ConfigureNotifications(
           this ModelBuilder builder,
           Action<AbpModelBuilderConfigurationOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new AbpModelBuilderConfigurationOptions();

            optionsAction?.Invoke(options);

            builder.Entity<Notification>(b =>
            {
                b.ToTable(options.TablePrefix + "Notifications", options.Schema);

                b.Property(p => p.NotificationName)
                .HasMaxLength(NotificationConsts.MaxNameLength).IsRequired().HasComment("通知名称");
                b.Property(p => p.NotificationTypeName).HasMaxLength(NotificationConsts.MaxTypeNameLength).IsRequired();
                //b.Property(p => p.NotificationData).HasMaxLength(NotificationConsts.MaxDataLength).IsRequired();

                b.Property(p => p.ContentType)
                 .HasDefaultValue(NotificationContentType.Text);

                b.ConfigureByConvention();

                b.HasIndex(p => new { p.TenantId, p.NotificationName });
            });

            builder.Entity<UserNotification>(b =>
            {
                b.ToTable(options.TablePrefix + "UserNotifications", options.Schema);

                b.ConfigureByConvention();

                b.HasIndex(p => new { p.TenantId, p.UserId, p.NotificationId })
                 .HasName("IX_Tenant_User_Notification_Id");
            });

            builder.Entity<UserSubscribe>(b =>
            {
                b.ToTable(options.TablePrefix + "UserSubscribes", options.Schema);

                b.Property(p => p.NotificationName).HasMaxLength(SubscribeConsts.MaxNotificationNameLength).IsRequired();
                b.Property(p => p.UserName)
                    .HasMaxLength(SubscribeConsts.MaxUserNameLength)
                    .HasDefaultValue("/")// 不是必须的
                    .IsRequired();

                b.ConfigureByConvention();

                b.HasIndex(p => new { p.TenantId, p.UserId, p.NotificationName })
                 .HasName("IX_Tenant_User_Notification_Name")
                 .IsUnique();
            });

            builder.Entity<NotificationDefinitionGroupRecord>(b =>
            {
                b.ToTable(options.TablePrefix + "NotificationDefinitionGroups", options.Schema);
                b.Property(p => p.Name)
                 .HasMaxLength(NotificationDefinitionGroupRecordConsts.MaxNameLength)
                 .IsRequired();

                b.Property(p => p.DisplayName)
                 .HasMaxLength(NotificationDefinitionGroupRecordConsts.MaxDisplayNameLength);
                b.Property(p => p.Description)
                 .HasMaxLength(NotificationDefinitionGroupRecordConsts.MaxDescriptionLength);

                b.ConfigureByConvention();
            });

            builder.Entity<NotificationDefinitionRecord>(b =>
            {
                b.ToTable(options.TablePrefix + "NotificationDefinitions", options.Schema);
                b.Property(p => p.Name)
                 .HasMaxLength(NotificationDefinitionRecordConsts.MaxNameLength)
                 .IsRequired();
                b.Property(p => p.GroupName)
                 .HasMaxLength(NotificationDefinitionGroupRecordConsts.MaxNameLength)
                 .IsRequired();

                b.Property(p => p.DisplayName)
                 .HasMaxLength(NotificationDefinitionRecordConsts.MaxDisplayNameLength);
                b.Property(p => p.Description)
                 .HasMaxLength(NotificationDefinitionRecordConsts.MaxDescriptionLength);
                b.Property(p => p.Providers)
                 .HasMaxLength(NotificationDefinitionRecordConsts.MaxProvidersLength);

                b.Property(p => p.ContentType)
                 .HasDefaultValue(NotificationContentType.Text);

                b.ConfigureByConvention();
            });
        }
    }
}
