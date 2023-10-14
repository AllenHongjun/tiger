using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Tiger.Infrastructure.Notification;
using Tiger.Infrastructure.Notification.Core;
using Tiger.Module.Notifications.Dto;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.TextTemplating;

namespace Tiger.Module.Notifications
{
    /// <summary>
    /// 通知服务
    /// </summary>
    [Authorize]
    [RemoteService(IsEnabled = false)]
    public class NotificationAppService : ApplicationService, INotificationAppService
    {
        #region 构造函数
        public NotificationAppService(
            ITemplateContentProvider templateContentProvider,
            INotificationSender notificationSender,
            INotificationDefinitionManager notificationDefinitionManager)
        {
            TemplateContentProvider=templateContentProvider;
            NotificationSender=notificationSender;
            NotificationDefinitionManager=notificationDefinitionManager;
        }

        protected ITemplateContentProvider TemplateContentProvider { get; }
        protected INotificationSender NotificationSender { get; }
        protected INotificationDefinitionManager NotificationDefinitionManager { get; } 
        #endregion

        /// <summary>
        /// 获取通知定义
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        protected async virtual Task<NotificationDefinition> GetNotificationDefinition(string name)
        {
            return await NotificationDefinitionManager.GetAsync(name);
        }

        /// <summary>
        /// 获取可分配的通知者
        /// </summary>
        /// <returns></returns>
        public async Task<ListResultDto<NotificationGroupDto>> GetAssignableNotifiersAsync()
        {
            var groups = new List<NotificationGroupDto>();
            var defineGroups = await NotificationDefinitionManager.GetGroupsAsync();

            foreach (var group in defineGroups)
            {
                if (!group.AllowSubscriptionToClients) continue;

                var notificationGroup = new NotificationGroupDto
                {
                    Name = group.Name,
                    DisplayName = group.DisplayName.Localize(StringLocalizerFactory)
                };

                foreach (var notification in group.Notifications)
                {
                    if (!notification.AllowSubscriptionToClients) continue;

                    var notificationChildren = new NotificationDto
                    {
                        Name = notification.Name,
                        DisplayName = notification.DisplayName.Localize(StringLocalizerFactory),
                        Description = notification.Description?.Localize(StringLocalizerFactory) ?? notification.Name,
                        Lifetime = notification.NotificationLifetime,
                        Type = notification.NotificationType,
                        ContentType = notification.ContentType
                    };

                    notificationGroup.Notifications.Add(notificationChildren);
                }

                groups.Add(notificationGroup);
            }
            
            return new ListResultDto<NotificationGroupDto>(groups);
        }

        /// <summary>
        /// 获取可分配的通知模板
        /// </summary>
        /// <returns></returns>
        public async Task<ListResultDto<NotificationTemplateDto>> GetAssignableTemplatesAsync()
        {
            var templates = new List<NotificationTemplateDto>();
            var notifications = (await NotificationDefinitionManager.GetNotificationsAsync())
                .Where(x => x.Template != null);
            
            foreach (var notification in notifications)
            {
                if(!notification.AllowSubscriptionToClients)
                {
                    continue;
                }

                templates.Add(
                    new NotificationTemplateDto
                    {
                        Name = notification.Name,
                        Culture = CultureInfo.CurrentCulture.Name,
                        Title = notification.DisplayName.Localize(StringLocalizerFactory),
                        Description = notification.Description?.Localize(StringLocalizerFactory)
                    });
            }

            return new ListResultDto<NotificationTemplateDto>(templates);
        }

        public async Task SendAsync(NotificationSendDto input)
        {
            var notificaton = await GetNotificationDefinition(input.Name);

            UserIdentifier user = null;
            if (input.ToUserId.HasValue)
            {
                user = new UserIdentifier(input.ToUserId.Value, input.ToUserName);
            }

            if (input.TemplateName.IsNullOrWhiteSpace()) 
            { 
                var notificationData = new NotificationData();
                notificationData.ExtraProperties.AddIfNotContains(input.Data);

                notificationData = NotificationData.ToStandardData(notificationData);

                await NotificationSender
                    .SendNofiterAsync(
                    name: input.Name,
                    data: notificationData,
                    user: user,
                    tenantId: CurrentTenant.Id,
                    severity: input.Severity);
            }
            else
            {
                if (notificaton.Template == null)
                {
                    throw new BusinessException(
                    NotificationsErrorCodes.NotificationTemplateNotFound,
                    $"The notification template {input.TemplateName} does not exist!")
                    .WithData("Name", input.TemplateName);
                }

                var notificationTemplate = new NotificationTemplate(
                    notificaton.Name,
                    culture: input.Culture ?? CultureInfo.CurrentCulture.Name,
                    formUser: CurrentUser.Name ?? CurrentUser.UserName,
                    data: input.Data);

                await NotificationSender
                    .SendNofiterAsync(
                    name:input.Name,
                    template: notificationTemplate,
                    user: user,
                    tenantId: CurrentTenant.Id,
                    severity: input.Severity);
            }

        }


    }
}
