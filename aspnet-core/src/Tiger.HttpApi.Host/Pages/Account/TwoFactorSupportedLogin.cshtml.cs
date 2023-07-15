using IdentityServer4.Services;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using Volo.Abp.Account.Web;
using Volo.Abp.Account.Web.Pages.Account;
using Volo.Abp.DependencyInjection;

namespace Tiger.Pages.Account
{
    /// <summary>
    /// ��д��¼ģ��,ʵ��˫���ص�¼
    /// </summary>
    [Dependency(ReplaceServices = true)]
    [ExposeServices(typeof(LoginModel), typeof(IdentityServerSupportedLoginModel))]
    public class TwoFactorSupportedLoginModel : IdentityServerSupportedLoginModel
    {
        public TwoFactorSupportedLoginModel(
            IAuthenticationSchemeProvider schemeProvider,
            IOptions<AbpAccountOptions> accountOptions,
            IOptions<IdentityOptions> identityOptions,
            IIdentityServerInteractionService interaction,
            IClientStore clientStore,
            IEventService identityServerEvents)
            : base(schemeProvider, accountOptions, interaction, clientStore, identityServerEvents)
        {

        }

        protected override Task<IActionResult> TwoFactorLoginResultAsync()
        {
            // �ض���˫������֤ҳ��
            return Task.FromResult<IActionResult>(RedirectToPage("SendCode", new
            {
                returnUrl = ReturnUrl,
                returnUrlHash = ReturnUrlHash,
                rememberMe = LoginInput.RememberMe
            }));
        }
    }
}
