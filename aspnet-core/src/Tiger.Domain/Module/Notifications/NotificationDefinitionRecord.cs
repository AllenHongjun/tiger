using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tiger.Infrastructure.Notification;
using Volo.Abp;
using Volo.Abp.Data;
using Volo.Abp.Domain.Entities;

namespace Tiger.Module.Notifications
{
    /// <summary>
    /// 通知定义记录
    /// </summary>
    public class NotificationDefinitionRecord : BasicAggregateRoot<Guid>, IHasExtraProperties
    {
        /// <summary>
        /// 名称
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// 分组名
        /// </summary>
        public virtual string GroupName { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        public virtual string DisplayName { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public virtual string Description { get; set; } 

        /// <summary>
        /// 存货时间
        /// </summary>
        public virtual NotificationLifetime NotificationLifetime { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public virtual NotificationType NotificationType { get; set; }

        /// <summary>
        /// 内容类型
        /// </summary>
        public virtual NotificationContentType ContentType { get; set; }

        /// <summary>
        /// 提供者
        /// </summary>
        public virtual string Providers { get; protected set; }

        /// <summary>
        /// 允许客户端订阅
        /// </summary>
        public virtual bool AllowSubscriptionToClients { get; set; }
         
        /// <summary>
        /// 额外属性
        /// </summary>
        public Dictionary<string, object> ExtraProperties { get; set; }

        public NotificationDefinitionRecord(
            Guid id,
            string name,
            string groupName,
            string displayName = null,
            string description = null,
            NotificationLifetime lifetime = NotificationLifetime.Persistent,
            NotificationType notificationType = NotificationType.Application,
            NotificationContentType contentType = NotificationContentType.Text): base(id)
        {
            Name = Check.NotNullOrWhiteSpace(name, nameof(name), NotificationDefinitionRecordConsts.MaxNameLength);
            GroupName = Check.NotNullOrWhiteSpace(groupName, nameof(groupName), NotificationDefinitionGroupRecordConsts.MaxNameLength);
            DisplayName = Check.Length(displayName, nameof(displayName), NotificationDefinitionRecordConsts.MaxDisplayNameLength);
            Description = Check.Length(description, nameof(description), NotificationDefinitionRecordConsts.MaxDescriptionLength);
            NotificationLifetime = lifetime;
            NotificationType = notificationType;
            ContentType = contentType;

            ExtraProperties = new Dictionary<string, object>();
            this.SetDefaultsForExtraProperties();

            AllowSubscriptionToClients = true;
        }

        public void UseProviders(params string[] providers)
        {
            var currentProviders = Providers.IsNullOrWhiteSpace()
                ? new List<string>() : Providers.Split(";").ToList();

            if (!providers.IsNullOrEmpty())
            {
                currentProviders.AddIfNotContains(providers);
            }

            if (currentProviders.Any())
            {
                Providers = currentProviders.JoinAsString(";");
            }
        }

        public bool HasSameData(NotificationDefinitionRecord otherRecord)
        {
            if (Name != otherRecord.Name)
            {
                return false;
            }

            if (GroupName != otherRecord.GroupName)
            {
                return false;
            }

            if (DisplayName != otherRecord.DisplayName)
            {
                return false;
            }

            if (Providers != otherRecord.Providers)
            {
                return false;
            }

            //if (!this.HasSameExtraProperties(otherRecord))
            //{
            //    return false;
            //}

            return true;
        }

        public void Patch(NotificationDefinitionRecord otherRecord)
        {
            if (Name != otherRecord.Name)
            {
                Name = otherRecord.Name;
            }

            if (GroupName != otherRecord.GroupName)
            {
                GroupName = otherRecord.GroupName;
            }

            if (DisplayName != otherRecord.DisplayName)
            {
                DisplayName = otherRecord.DisplayName;
            }

            if (Providers != otherRecord.Providers)
            {
                Providers = otherRecord.Providers;
            }

            //if (!this.HasSameExtraProperties(otherRecord))
            //{
            //    ExtraProperties.Clear();

            //    foreach (var property in otherRecord.ExtraProperties)
            //    {
            //        ExtraProperties.Add(property.Key, property.Value);
            //    }
            //}
        }

    }
}
