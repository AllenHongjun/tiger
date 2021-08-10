using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Sms;

namespace TigerAdmin.Module.Helper
{
    public class MyCustomSmsSender : ISmsSender, ITransientDependency
    {
        public async Task SendAsync(SmsMessage smsMessage)
        {
            // Send sms
        }
    }
}
