using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Validation.StringValues;

namespace Tiger.Infrastructure.Notification
{
    [Dependency(TryRegister = true)]
    public class DefaultNotificationDataSerializer : INotificationDataSerializer, ISingletonDependency
    {
        public NotificationData Serialize(NotificationData source)
        {
            if (source != null)
            {
                if (source.NeedLocalizer())
                {
                    // 潜在的空对象引用修复
                    if (source.ExtraProperties.TryGetValue("title", out var title) && title != null)
                    {
                        var titleObj = JsonConvert.DeserializeObject<LocalizableStringInfo>(title.ToString());
                        source.TrySetData("title", titleObj);
                    }
                    if (source.ExtraProperties.TryGetValue("message", out var message) && message != null)
                    {
                        var messageObj = JsonConvert.DeserializeObject<LocalizableStringInfo>(message.ToString());
                        source.TrySetData("message", messageObj);
                    }

                    if (source.ExtraProperties.TryGetValue("description", out var description) && description != null)
                    {
                        var descriptionObj = JsonConvert.DeserializeObject<LocalizableStringInfo>(description.ToString());
                        source.TrySetData("description", descriptionObj);
                    }
                }
            }
            else
            {
                source = new NotificationData();
            }
            return source;
        }
    }
}
