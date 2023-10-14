using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
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
    /// 我的通知服务
    /// </summary>
    [Authorize]
    [RemoteService(IsEnabled = false)]
    public class MyNotificationAppService : ApplicationService, IMyNotificationAppService
    {
        #region 构造函数
        public MyNotificationAppService(INotificationSender notificationSender, INotificationStore notificationStore, IUserNotificationRepository userNotificationRepository, INotificationDefinitionManager notificationDefinitionManager)
        {
            NotificationSender=notificationSender;
            NotificationStore=notificationStore;
            UserNotificationRepository=userNotificationRepository;
            NotificationDefinitionManager=notificationDefinitionManager;
        }

        protected INotificationSender NotificationSender { get; }
        protected INotificationStore NotificationStore { get; }
        protected IUserNotificationRepository UserNotificationRepository { get; }
        protected INotificationDefinitionManager NotificationDefinitionManager { get; }
        #endregion

        #region CRUD
        public async Task DeleteAsync(long id)
        {
            await NotificationStore
                .DeleteUserNotificationAsync(
                CurrentTenant.Id, CurrentUser.GetId(), id);
        }

        public async Task<UserNotificationDto> GetAsync(long id)
        {
            var notification = await UserNotificationRepository.GetByIdAsync(CurrentUser.GetId(), id);

            return ObjectMapper.Map<UserNotificationInfo, UserNotificationDto>(notification);
        }

        public async Task<PagedResultDto<UserNotificationDto>> GetListAsync(UserNotificationGetByPagedDto input)
        {
            var totalCount = await UserNotificationRepository.GetCountAsync(
                CurrentUser.GetId(),
                input.Filter,
                input.ReadState);

            var notifications = await UserNotificationRepository.GetListAsync(
                CurrentUser.GetId(), input.Filter, input.Sorting, input.ReadState, input.SkipCount, input.MaxResultCount);

            return new PagedResultDto<UserNotificationDto>(totalCount,
                ObjectMapper.Map<List<UserNotificationInfo>, List<UserNotificationDto>>(notifications));

        }

        /// <summary>
        /// 修改用户通知读取状态
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task MarkReadStateAsync(NotificationMarkReadStateInput input)
        {
            await NotificationStore.ChangeUserNotificationsReadStateAsync(
                CurrentTenant.Id,
                CurrentUser.GetId(),
                input.IdList,
                input.State);
            throw new NotImplementedException();
        } 
        #endregion
    }
}
