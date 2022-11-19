using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tiger.BackgroundJob;
using Volo.Abp.Application.Services;
using Volo.Abp.BackgroundJobs;

namespace Tiger.Infrastructure.BackgroundJob
{
    /// <summary>
    /// 使用 IBackgroundJobManager 服务向队列中添加一个发送电子邮件作业
    /// </summary>
    public class RegistrationService : ApplicationService
    {
        private readonly IBackgroundJobManager _backgroundJobManager;

        public RegistrationService(IBackgroundJobManager backgroundJobManager)
        {
            _backgroundJobManager = backgroundJobManager;
        }

        public async Task RegisterAsync(string userName, string emailAddress, string password)
        {
            //TODO: 创建一个新用户到数据库中...

            await _backgroundJobManager.EnqueueAsync(
                new EmailSendingArgs
                {
                    EmailAddress = emailAddress,
                    Subject = "You've successfully registered!",
                    Body = "..."
                },
                BackgroundJobPriority.Normal
                
            );
        }
    }

}
