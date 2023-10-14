using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tiger.Infrastructure.Notification;
using Tiger.Module.Notifications.Dto;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Users;

namespace Tiger.Module.Notifications
{
    /// <summary>
    /// 我的订阅
    /// </summary>
    [Authorize]
    [RemoteService(IsEnabled = false)]
    public class MySubscriptionAppService : ApplicationService, IMySubscriptionAppService
    {
        #region 构造函数
        public MySubscriptionAppService(IUserSubscribeRepository userSubscribeRepository, INotificationSubscriptionManager notificationSubscriptionManager)
        {
            UserSubscribeRepository=userSubscribeRepository;
            NotificationSubscriptionManager=notificationSubscriptionManager;
        }

        protected IUserSubscribeRepository UserSubscribeRepository { get; }
        protected INotificationSubscriptionManager NotificationSubscriptionManager { get; }
        #endregion

        #region CRUD
        public async Task<ListResultDto<UserSubscreNotificationDto>> GetAllListAsync()
        {
            List<NotificationSubscriptionInfo> userSubscribes = await NotificationSubscriptionManager
                .GetUserSubscriptionsAsync(CurrentTenant.Id, CurrentUser.GetId());

            var list = userSubscribes.Select(msn => new UserSubscreNotificationDto { Name = msn.NotificationName }).ToList();

            return new ListResultDto<UserSubscreNotificationDto>(list);
        }

        public async Task<PagedResultDto<UserSubscreNotificationDto>> GetListAsync(SubscriptionsGetByPagedDto input)
        {
            var userSubscribedCount = await UserSubscribeRepository.GetCountAsync(CurrentUser.GetId());

            var userSubscribes = await UserSubscribeRepository
                .GetUserSubscribesAsync(CurrentUser.GetId(), input.Sorting,
                        input.SkipCount, input.MaxResultCount);

            return new PagedResultDto<UserSubscreNotificationDto>(userSubscribedCount,
                userSubscribes.Select(us => new UserSubscreNotificationDto { Name = us.NotificationName }).ToList());
        }

        public async Task<UserSubscriptionsResult> IsSubscribedAsync(SubscriptionsGetByNameDto input)
        {
            var isSubscribed = await NotificationSubscriptionManager
            .IsSubscribedAsync(CurrentTenant.Id, CurrentUser.GetId(), input.Name);

            return isSubscribed
                ? UserSubscriptionsResult.Subscribed()
                : UserSubscriptionsResult.UnSubscribed();
        }

        public async Task SubscribeAsync(SubscriptionsGetByNameDto input)
        {
            await NotificationSubscriptionManager
            .SubscribeAsync(
                CurrentTenant.Id,
                new UserIdentifier(CurrentUser.GetId(), CurrentUser.UserName),
                input.Name);
        }

        public async Task UnSubscribeAsync(SubscriptionsGetByNameDto input)
        {
            await NotificationSubscriptionManager
            .UnsubscribeAsync(
                CurrentTenant.Id,
                new UserIdentifier(CurrentUser.GetId(), CurrentUser.UserName),
                input.Name);
        } 
        #endregion
    }
}
