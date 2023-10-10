using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tiger.Infrastructure.Notification
{
    public class NotificationDataMappingDictionary:Dictionary<string, List<NotificationDataMappingDictionaryItem>>
    {
        public static string DefaultKey { get; set; } = "Default";

        public void Mapping(string provider, string name, 
            Func<NotificationData, NotificationData> func)
        {
            if(!ContainsKey(provider))
            {
                this[provider] = new List<NotificationDataMappingDictionaryItem>();
            }

            var mapItem = this[provider].FirstOrDefault(item => item.Name.Equals(name));

            if(mapItem == null)
            {
                this[provider].Add(new NotificationDataMappingDictionaryItem(name, func));
            }
            else
            {
                mapItem.Replace(func);
            }
        }

        public void MappingDefault(string provider, Func<NotificationData, NotificationData> func)
        {
            Mapping(provider, DefaultKey, func);
        }

        public NotificationDataMappingDictionaryItem GetMapItemOrDefault(string provider, string name)
        {
            if (ContainsKey(provider))
            {
                return this[provider].GetOrNullDefault(name);
            }
            else
            {
                return null;
            }
        }
    }
}
