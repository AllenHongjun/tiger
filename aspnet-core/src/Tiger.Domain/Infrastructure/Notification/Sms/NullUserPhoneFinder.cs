using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Volo.Abp.DependencyInjection;

namespace Tiger.Infrastructure.Notification.Sms
{
    public class NullUserPhoneFinder : IUserPhoneFinder, ISingletonDependency
    {
        public Task<IEnumerable<string>> FindByUserIdsAsync(IEnumerable<Guid> userIds, CancellationToken cancellation = default)
        {
            IEnumerable<string> emptyPhoneList = new string[0];

            return Task.FromResult(emptyPhoneList);
        }
    }
}
