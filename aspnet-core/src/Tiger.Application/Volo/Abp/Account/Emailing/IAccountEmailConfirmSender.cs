using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Identity;

namespace Tiger.Volo.Abp.Account.Emailing
{
    public interface IAccountEmailConfirmSender
    {
        Task SendEmailConfirmLinkAsync(
        IdentityUser user,
        string confirmToken,
        string appName,
        string returnUrl = null,
        string returnUrlHash = null
    );
    }
}
