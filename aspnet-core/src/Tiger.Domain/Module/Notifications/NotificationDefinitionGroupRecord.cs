using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp;
using Volo.Abp.Data;
using Volo.Abp.Domain.Entities;

namespace Tiger.Module.Notifications
{
    /// <summary>
    /// 通知定义组记录
    /// </summary>
    public class NotificationDefinitionGroupRecord:BasicAggregateRoot<Guid>,IHasExtraProperties
    {
        public NotificationDefinitionGroupRecord()
        {
            ExtraProperties = new Dictionary<string, object>();
            this.SetDefaultsForExtraProperties();
        }

        public NotificationDefinitionGroupRecord(
            Guid id,
            string name, 
            string displayName, 
            string description):base(id)
        {
            Name = Check.NotNullOrWhiteSpace(name, nameof(name), NotificationDefinitionGroupRecordConsts.MaxNameLength);
            DisplayName = Check.Length(displayName, nameof(displayName), NotificationDefinitionGroupRecordConsts.MaxDisplayNameLength);
            Description = Check.Length(description, nameof(description), NotificationDefinitionGroupRecordConsts.MaxDescriptionLength);

            ExtraProperties = new Dictionary<string, object>();
            this.SetDefaultsForExtraProperties();

            AllowSubscriptionToClients = true;
        }

        /// <summary>
        /// 分组名称
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        public virtual string DisplayName { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public virtual string Description { get; set; }

        /// <summary>
        /// 允许客户端订阅
        /// </summary>
        public virtual bool AllowSubscriptionToClients { get; set; }

        public Dictionary<string, object> ExtraProperties {get; set; }

        public bool HasSameData(NotificationDefinitionGroupRecord otherRecord)
        {
            if (Name != otherRecord.Name)
            {
                return false;
            }

            if (DisplayName != otherRecord.DisplayName)
            {
                return false;
            }

            //if (!this.HasSameExtraProperties(otherRecord))
            //{
            //    return false;
            //}

            return true;
        }

        public void Patch(NotificationDefinitionGroupRecord otherRecord)
        {
            if (Name != otherRecord.Name)
            {
                Name = otherRecord.Name;
            }

            if (DisplayName != otherRecord.DisplayName)
            {
                DisplayName = otherRecord.DisplayName;
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
