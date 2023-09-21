using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Data;
using Volo.Abp.EventBus;

namespace Tiger.Infrastructure.Notification.Core
{

    /// <summary>
    /// 通知数据
    /// </summary>
    /// <remarks>
    /// 把通知的标题和内容设计为 <see cref="LocalizableStringInfo"/> 让客户端自行本地化
    /// </remarks>
    [Serializable]
    [EventName("notification")]
    public class NotificationData : IHasExtraProperties
    {
        public const string LocalizerKey = "L";

        public virtual string Type => GetType().FullName;

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

        public Dictionary<string, object> ExtraProperties { get;set; }

        public NotificationData()
        {
            ExtraProperties = new Dictionary<string, object>();
            this.SetDefaultsForExtraProperties();

            TrySetData(LocalizerKey, false);
        }


        public void TrySetData(string key, object value)
        {
            this.SetProperty(key, value);
        }
    }
}
