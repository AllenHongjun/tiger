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

        [HttpGet]
        [Route("all")]
        public async virtual Task<ListResultDto<UserSubscreNotificationDto>> GetAllListAsync()
        {
            return await _subscriptionAppService.GetAllListAsync();
        }

        [HttpGet]
        public async virtual Task<PagedResultDto<UserSubscreNotificationDto>> GetListAsync(SubscriptionsGetByPagedDto input)
        {
            return await _subscriptionAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("is-subscribed/{Name}")]
        public async virtual Task<UserSubscriptionsResult> IsSubscribedAsync(SubscriptionsGetByNameDto input)
        {
            return await _subscriptionAppService.IsSubscribedAsync(input);
        }

        [HttpPost]
        public async virtual Task SubscribeAsync(SubscriptionsGetByNameDto input)
        {
            await _subscriptionAppService.SubscribeAsync(input);
        }

        [HttpDelete]
        public async virtual Task UnSubscribeAsync(SubscriptionsGetByNameDto input)
        {
            await _subscriptionAppService.UnSubscribeAsync(input);
        }
    }

}
