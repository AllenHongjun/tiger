using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Data;
using Volo.Abp.EventBus;
using Volo.Abp;

namespace Tiger.Infrastructure.Notification.Core
{
    /// <summary>
    /// 通知模板消息
    /// </summary>
    [Serializable]
    [EventName("notifications.template")]
    public class NotificationTemplate : IHasExtraProperties
    {
        public string Name { get; set; }
        public string Culture { get; set; }
        public string FormUser { get; set; }
        public object this[string key]
        {
            get
            {
                return this.GetProperty(key);
            }
            set
            {
                this.SetProperty(key, value);
            }
        }

        public Dictionary<string, object> ExtraProperties { get; set; }

        public NotificationTemplate() { }

        public NotificationTemplate(
            string name,
            string culture = null,
            string formUser = null,
            IDictionary<string, object> data = null)
        {
            Name = Check.NotNullOrWhiteSpace(name, nameof(name));

            Culture = culture;
            FormUser = formUser;

            if (data != null)
            {
                ExtraProperties = new Dictionary<string, object>(data);
            }
            else
            {
                ExtraProperties = new Dictionary<string, object>();
                this.SetDefaultsForExtraProperties();
            }
        }
    }
}
