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
    /// 我的订阅
    /// </summary>
    [Controller]
    [RemoteService(Name = AbpNotificationsRemoteServiceConsts.RemoteServiceName)]
    [Area(AbpNotificationsRemoteServiceConsts.ModuleName)]
    [Route("api/notifications/my-subscribes")]
    public class MySubscriptionController : AbpController, IMySubscriptionAppService
    {
        private readonly IMySubscriptionAppService _subscriptionAppService;

        public MySubscriptionController(
            IMySubscriptionAppService subscriptionAppService)
        {
            _subscriptionAppService = subscriptionAppService;
        }

        /// <summary>
        /// 获取所有我的订阅列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("all")]
        public async virtual Task<ListResultDto<UserSubscreNotificationDto>> GetAllListAsync()
        {
            return await _subscriptionAppService.GetAllListAsync();
        }

        /// <summary>
        /// 分页获取我的订阅列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        public async virtual Task<PagedResultDto<UserSubscreNotificationDto>> GetListAsync(SubscriptionsGetByPagedDto input)
        {
            return await _subscriptionAppService.GetListAsync(input);
        }

        /// <summary>
        /// 是否订阅
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("is-subscribed/{Name}")]
        public async virtual Task<UserSubscriptionsResult> IsSubscribedAsync(SubscriptionsGetByNameDto input)
        {
            return await _subscriptionAppService.IsSubscribedAsync(input);
        }

        /// <summary>
        /// 订阅通知
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async virtual Task SubscribeAsync(SubscriptionsGetByNameDto input)
        {
            await _subscriptionAppService.SubscribeAsync(input);
        }

        /// <summary>
        /// 取消订阅
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpDelete]
        public async virtual Task UnSubscribeAsync(SubscriptionsGetByNameDto input)
        {
            await _subscriptionAppService.UnSubscribeAsync(input);
        }
    }

}
