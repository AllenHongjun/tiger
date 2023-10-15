using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Tiger.Module.Notifications.Dto;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp;

namespace Tiger.Module.Notifications
{
    /// <summary>
    /// 通知定义
    /// </summary>
    [Controller]
    [RemoteService(Name = AbpNotificationsRemoteServiceConsts.RemoteServiceName)]
    [Area(AbpNotificationsRemoteServiceConsts.ModuleName)]
    [Route("api/notifications")]
    public class NotificationController : AbpController, INotificationAppService
    {
        protected INotificationAppService NotificationAppService { get; }

        public NotificationController(
            INotificationAppService notificationAppService)
        {
            NotificationAppService = notificationAppService;
        }

        /// <summary>
        /// 发送通知
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async virtual Task SendAsync(NotificationSendDto input)
        {
            await NotificationAppService.SendAsync(input);
        }

        /// <summary>
        /// 获取可分配的通知者
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("assignables")]
        public async virtual Task<ListResultDto<NotificationGroupDto>> GetAssignableNotifiersAsync()
        {
            return await NotificationAppService.GetAssignableNotifiersAsync();
        }

        /// <summary>
        /// 获取可分配的通知模板
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("assignable-templates")]
        public async virtual Task<ListResultDto<NotificationTemplateDto>> GetAssignableTemplatesAsync()
        {
            return await NotificationAppService.GetAssignableTemplatesAsync();
        }
    }
}
