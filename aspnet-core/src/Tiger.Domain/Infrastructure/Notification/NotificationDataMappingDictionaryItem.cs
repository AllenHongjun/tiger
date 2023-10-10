using System;
using System.Collections.Generic;
using System.Text;

namespace Tiger.Infrastructure.Notification
{
    public class NotificationDataMappingDictionaryItem
    {
        public NotificationDataMappingDictionaryItem(string name, Func<NotificationData, NotificationData> mappingFunc)
        {
            Name=name;
            MappingFunc=mappingFunc;
        }

        public string Name { get; }

        public Func<NotificationData, NotificationData> MappingFunc { get; protected set; }

        public void Replace(Func<NotificationData, NotificationData> mappingFunc)
        {
            MappingFunc = mappingFunc;
        }

    }
}
