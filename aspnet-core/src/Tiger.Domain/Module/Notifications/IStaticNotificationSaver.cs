using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Tiger.Module.Notifications
{
    public interface IStaticNotificationSaver
    {
        Task SaveAsync();
    }
}
