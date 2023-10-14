using Aliyun.Acs.Core.Logging;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tiger.Volo.Abp.Identity;
using Volo.Abp.Emailing;
using Volo.Abp.Identity;
using Volo.Abp.Localization;
using Microsoft.Extensions.Logging;

namespace Tiger.Infrastructure.Notification.Emailing
{
    /// <summary>
    /// 邮件通知发布提供者
    /// </summary>
    public class EmailingNotificationPublishProvider : NotificationPublishProvider
    {
        public const string ProviderName = NotificationProviderNames.Emailing;

        public EmailingNotificationPublishProvider(
            IOptions<AbpLocalizationOptions> localizationOptions,
            IEmailSender emailSender,
            IStringLocalizerFactory localizerFactory,
            IIdentityUserRepository identityUserRepository,
            ITigerIdentityUserRepository tigerIdentityUserRepository)
        {
            LocalizationOptions=localizationOptions.Value;
            EmailSender=emailSender;
            LocalizerFactory=localizerFactory;
            IdentityUserRepository=identityUserRepository;
            TigerIdentityUserRepository=tigerIdentityUserRepository;
        }

        public override string Name => ProviderName;

        protected AbpLocalizationOptions LocalizationOptions { get; set; }

        protected IEmailSender EmailSender { get; set; }

        protected IStringLocalizerFactory LocalizerFactory { get; set; }

        protected IIdentityUserRepository IdentityUserRepository { get; set; }
        protected ITigerIdentityUserRepository TigerIdentityUserRepository { get; set; }




        protected override async Task PublishAsync(
            NotificationInfo notification, 
            IEnumerable<UserIdentifier> identifiers, 
            CancellationToken cancellationToken = default)
        {
            var userIds = identifiers.Select(x => x.UserId).ToList();
            var userList = await TigerIdentityUserRepository.GetListByIdListAsync(userIds, cancellationToken: cancellationToken);
            var emailAddress = userList.Where(x => x.EmailConfirmed).Select(x => x.Email).Distinct()
                .JoinAsString(",");

            if (emailAddress.IsNullOrWhiteSpace())
            {
                //Logger.LogWarning("The subscriber did not confirm the email address and could not send email notifications!");
                return;
            }


            if (!notification.Data.NeedLocalizer())
            {
                var title = notification.Data.TryGetData("title").ToString();
                var message = notification.Data.TryGetData("message").ToString();

                await EmailSender.SendAsync(emailAddress, title, message);
            }
            else
            {
                throw new NotImplementedException();

            }
        }
    }
}
