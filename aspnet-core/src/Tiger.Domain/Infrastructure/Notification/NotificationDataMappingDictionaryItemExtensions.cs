using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tiger.Infrastructure.Notification
{
    public static class NotificationDataMappingDictionaryItemExtensions
    {
        public static NotificationDataMappingDictionaryItem GetOrNullDefault(
            this IEnumerable<NotificationDataMappingDictionaryItem> items,
            string name)
        {
            var item = items.FirstOrDefault(i => i.Name.Equals(name));
            if (item == null)
            {
                return items.FirstOrDefault(i => i.Name.Equals(NotificationDataMappingDictionary.DefaultKey));
            }
            else
            {
                return item;
            }
            
        }
    }
}
