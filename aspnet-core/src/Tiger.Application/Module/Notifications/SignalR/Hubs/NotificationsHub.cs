using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;
using Tiger.Module.Notifications.Enums;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.SignalR;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Uow;
using Volo.Abp.Users;

namespace Tiger.Infrastructure.Notification.SignalR.Hubs
{
    /// <summary>
    /// 通知Hub
    /// </summary>
    /// <remarks>
    /// signalR 模块集成 https://docs.abp.io/zh-Hans/abp/7.0/SignalR-Integration
    /// </remarks>
    [Authorize]
    public class NotificationsHub : AbpHub
    {
        protected INotificationStore _notificationStore;
        protected INotificationStore NotificationStore => LazyGetRequiredService(ref _notificationStore);

        /// <summary>
        /// 建立连接
        /// </summary>
        /// <returns></returns>
        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();

            if (CurrentTenant.IsAvailable)
            {
                // 以租户为分组，将用户加入租户通讯组
                await Groups.AddToGroupAsync(Context.ConnectionId, CurrentTenant.GetId().ToString());
            }
            else
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, "Global");
            }
        }

        /// <summary>
        /// 断开连接
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await base.OnDisconnectedAsync(exception);


            if (CurrentTenant.IsAvailable)
            {
                // 以租户为分组，将移除租户通讯组
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, CurrentTenant.GetId().ToString());
            }
            else
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, "Global");
            }
        }

        /// <summary>
        /// 获取我订阅的通知
        /// </summary>
        /// <returns></returns>
        [HubMethodName("my-subscriptions")]
        public async virtual Task<ListResultDto<NotificationSubscriptionInfo>> GetMySubscriptionsAsync()
        {
            var subscriptions = await NotificationStore
                .GetUserSubscriptionsAsync(CurrentTenant.Id, CurrentUser.GetId());

            return new ListResultDto<NotificationSubscriptionInfo>(subscriptions);
        }

        /// <summary>
        /// 获取我的通知列表
        /// </summary>
        /// <returns></returns>
        [UnitOfWork]
        [HubMethodName("get-notifications")]
        public async virtual Task<ListResultDto<NotificationInfo>> GetNotificationAsync()
        {
            var userNotifications = await NotificationStore
                .GetUserNotificationsAsync(CurrentTenant.Id, CurrentUser.GetId(), NotificationReadStatus.UnRead, 10);

            return new ListResultDto<NotificationInfo>(userNotifications);
        }

        /// <summary>
        /// 修改通知状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="readState"></param>
        /// <returns></returns>
        [HubMethodName("change-state")]
        public async virtual Task ChangeStateAsync(string id, NotificationReadStatus readState = NotificationReadStatus.Read)
        {
            await NotificationStore
                .ChangeUserNotificationReadStateAsync(
                    CurrentTenant.Id,
                    CurrentUser.GetId(),
                    long.Parse(id),
                    readState);
        }
    }
}
