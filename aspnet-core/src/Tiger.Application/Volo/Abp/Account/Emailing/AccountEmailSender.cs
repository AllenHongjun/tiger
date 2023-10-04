using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Volo.Abp.Account.Emailing.Templates;
using Volo.Abp.Account.Emailing;
using Volo.Abp.Account.Localization;
using Volo.Abp.Account;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Emailing;
using Volo.Abp.MultiTenancy;
using Volo.Abp.TextTemplating;
using Volo.Abp.UI.Navigation.Urls;
using Volo.Abp.Identity;
using System.Web;
using Tiger.Volo.Abp.Account.Emailing.Templates;

namespace Tiger.Volo.Abp.Account.Emailing
{
    [Dependency(ReplaceServices = true)]
    [ExposeServices(
    typeof(IAccountEmailConfirmSender),
    typeof(IAccountEmailVerifySender),
    typeof(IAccountEmailer),
    typeof(AccountEmailSender))]
    public class AccountEmailSender :
    AccountEmailer,
    IAccountEmailConfirmSender,
    IAccountEmailVerifySender,
    ITransientDependency
    {
        public AccountEmailSender(
            IEmailSender emailSender,
            ITemplateRenderer templateRenderer,
            IStringLocalizer<AccountResource> accountLocalizer,
            IAppUrlProvider appUrlProvider,
            ICurrentTenant currentTenant)
            : base(emailSender, templateRenderer, accountLocalizer, appUrlProvider, currentTenant)
        {
        }


        public override async  Task SendPasswordResetLinkAsync(IdentityUser user, string resetToken, string appName, string returnUrl = null, string returnUrlHash = null)
        {
            await base.SendPasswordResetLinkAsync(user,resetToken,appName,returnUrl,returnUrlHash);

            Debug.Assert(CurrentTenant.Id == user.TenantId, "This method can only work for current tenant!");

            var url = await AppUrlProvider.GetResetPasswordUrlAsync(appName);

            var link = $"{url}?userId={user.Id}&tenantId={user.TenantId}&resetToken={UrlEncoder.Default.Encode(resetToken)}";

            if (!returnUrl.IsNullOrEmpty())
            {
                link += "&returnUrl=" + NormalizeReturnUrl(returnUrl);
            }

            if (!returnUrlHash.IsNullOrEmpty())
            {
                link += "&returnUrlHash=" + returnUrlHash;
            }

            var emailContent = await TemplateRenderer.RenderAsync(
                AccountEmailTemplates.PasswordResetLink,
                new { link = link }
            );

            await EmailSender.SendAsync(
                user.Email,
                StringLocalizer["PasswordReset"],
                emailContent
            );
        }


        private string NormalizeReturnUrl(string returnUrl)
        {
            if (returnUrl.IsNullOrEmpty())
            {
                return returnUrl;
            }

            //Handling openid connect login
            if (returnUrl.StartsWith("/connect/authorize/callback", StringComparison.OrdinalIgnoreCase))
            {
                if (returnUrl.Contains("?"))
                {
                    var queryPart = returnUrl.Split('?')[1];
                    var queryParameters = queryPart.Split('&');
                    foreach (var queryParameter in queryParameters)
                    {
                        if (queryParameter.Contains("="))
                        {
                            var queryParam = queryParameter.Split('=');
                            if (queryParam[0] == "redirect_uri")
                            {
                                return HttpUtility.UrlDecode(queryParam[1]);
                            }
                        }
                    }
                }
            }

            return returnUrl;
        }

        public async virtual Task SendMailLoginVerifyCodeAsync(
            string code,
            string userName,
            string emailAddress)
        {
            

            var emailContent = await TemplateRenderer.RenderAsync(
                AppAccountEmailTemplates.MailSecurityVerifyLink,
                new { code = code, user = userName }
            );

            await EmailSender.SendAsync(
                emailAddress,
                StringLocalizer["MailSecurityVerify"],
                emailContent
            );
        }

        public async virtual Task SendEmailConfirmLinkAsync(
            IdentityUser user,
            string confirmToken,
            string appName,
            string returnUrl = null,
            string returnUrlHash = null)
        {

            Debug.Assert(CurrentTenant.Id == user.TenantId, "This method can only work for current tenant!");

            //AppName, AppUrlOptions https://github.com/abpframework/abp/issues/6178
            var url = await AppUrlProvider.GetUrlAsync(appName, AppAccountUrlNames.EmailConfirm);

            var link = $"{url}?userId={user.Id}&{TenantResolverConsts.DefaultTenantKey}={user.TenantId}&confirmToken={UrlEncoder.Default.Encode(confirmToken)}";

            if (!returnUrl.IsNullOrEmpty())
            {
                link += "&returnUrl=" + NormalizeReturnUrl(returnUrl);
            }

            if (!returnUrlHash.IsNullOrEmpty())
            {
                link += "&returnUrlHash=" + returnUrlHash;
            }

            var emailContent = await TemplateRenderer.RenderAsync(
                AppAccountEmailTemplates.MailConfirmLink,
                new { link = link }
            );

            await EmailSender.SendAsync(
                user.Email,
                StringLocalizer["EmailConfirm"],
                emailContent
            );
        }
    }

}
