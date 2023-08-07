using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System;
using Tiger.Volo.Abp.Account.Dto;
using Volo.Abp.Account.Localization;
using Volo.Abp.Account.Web.Pages.Account;
using Volo.Abp.Identity;
using Volo.Abp.Validation;

namespace Tiger.Pages.Account
{
    public class EmailConfirmModel : AccountPageModel
    {
        [Required]
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid UserId { get; set; }

        [Required]
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public string ConfirmToken { get; set; }

        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public string ReturnUrl { get; set; }

        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public string ReturnUrlHash { get; set; }

        //public IMyProfileAppService MyProfileAppService { get; set; }

        public EmailConfirmModel()
        {
            LocalizationResourceType = typeof(AccountResource);
        }

        public virtual IActionResult OnPost()
        {
            try
            {
                ValidateModel();

                //await MyProfileAppService.ConfirmEmailAsync(
                //    new ConfirmEmailInput
                //    {
                //        UserId = UserId,
                //        ConfirmToken = ConfirmToken,
                //    });
            }
            catch (AbpIdentityResultException e)
            {
                if (!string.IsNullOrWhiteSpace(e.Message))
                {
                    //Alerts.Warning(GetLocalizeExceptionMessage(e));
                    return Page();
                }

                throw;
            }
            catch (AbpValidationException)
            {
                return Page();
            }

            return RedirectToPage("./ConfirmEmailConfirmation", new
            {
                returnUrl = ReturnUrl,
                returnUrlHash = ReturnUrlHash
            });
        }
    }
}
