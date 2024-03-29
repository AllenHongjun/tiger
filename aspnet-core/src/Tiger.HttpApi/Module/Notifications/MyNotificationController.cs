﻿using Microsoft.AspNetCore.Authorization;
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
    /// 我的通知
    /// </summary>
    [Authorize]
    [Controller]
    [RemoteService(Name = AbpNotificationsRemoteServiceConsts.RemoteServiceName)]
    [Area(AbpNotificationsRemoteServiceConsts.ModuleName)]
    [Route("api/notifications/my-notifilers")]
    public class MyNotificationController : AbpController, IMyNotificationAppService
    {
        protected IMyNotificationAppService MyNotificationAppService { get; }

        public MyNotificationController(
            IMyNotificationAppService myNotificationAppService)
        {
            MyNotificationAppService = myNotificationAppService;
        }

        /// <summary>
        /// 标记阅读状态
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("mark-read-state")]
        public async virtual Task MarkReadStateAsync(NotificationMarkReadStateInput input)
        {
            await MyNotificationAppService.MarkReadStateAsync(input);
        }

        /// <summary>
        /// 删除我的通知
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        public async virtual Task DeleteAsync(long id)
        {
            await MyNotificationAppService.DeleteAsync(id);
        }

        /// <summary>
        /// 获取我的通知详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public async virtual Task<UserNotificationDto> GetAsync(long id)
        {
            return await MyNotificationAppService.GetAsync(id);
        }

        /// <summary>
        /// 获取我的通知列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        public async virtual Task<PagedResultDto<UserNotificationDto>> GetListAsync(UserNotificationGetByPagedDto input)
        {
            return await MyNotificationAppService.GetListAsync(input);
        }
    }
}
