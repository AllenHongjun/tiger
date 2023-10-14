using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Tiger.Infrastructure.Notification.Sms
{
    public class SmsNotificationPublishProvider : NotificationPublishProvider
    {
        public const string ProviderName = NotificationProviderNames.Sms;

        protected IUserPhoneFinder UserPhoneFinder { get; }
        protected ISmsNotificationSender Sender { get; }

        protected AbpNotificationsSmsOptions Options { get; }

        public SmsNotificationPublishProvider(
            IUserPhoneFinder userPhoneFinder,
            ISmsNotificationSender sender,
            IOptions<AbpNotificationsSmsOptions> options)
        {
            UserPhoneFinder = userPhoneFinder;
            Sender = sender;
            Options = options.Value;
        }

        public override string Name => ProviderName;

        protected override async Task PublishAsync(
            NotificationInfo notification,
            IEnumerable<UserIdentifier> identifiers,
            CancellationToken cancellationToken = default)
        {
            if (!identifiers.Any())
            {
                return;
            }

            var sendToPhones = await UserPhoneFinder.FindByUserIdsAsync(identifiers.Select(usr => usr.UserId), cancellationToken);
            if (!sendToPhones.Any())
            {
                return;
            }
            await Sender.SendAsync(notification, sendToPhones.JoinAsString(","));
        }
    }
}
