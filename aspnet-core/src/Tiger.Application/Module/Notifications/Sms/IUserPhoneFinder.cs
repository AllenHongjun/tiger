using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Tiger.Infrastructure.Notification.Sms
{
    public interface IUserPhoneFinder
    {
        Task<IEnumerable<string>> FindByUserIdsAsync(
            IEnumerable<Guid> userIds,
            CancellationToken cancellation = default);
    }
}
