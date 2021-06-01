using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Account;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Identity;

namespace TigerAdmin.Volo.Abp.Account
{
    //[RemoteService(Name = AccountRemoteServiceConsts.RemoteServiceName)]
    //[Area("tiger_account")]
    //[Route("api/account")]
    //public class TigerAccountController :AccountController 
    //{
    //    //protected IAccountAppService AccountAppService { get; }

    //    public TigerAccountController(IAccountAppService accountAppService):base(accountAppService)
    //    {
    //        //AccountAppService = accountAppService;
    //    }

    //    //[HttpPost]
    //    //[Route("register")]
    //    //public virtual Task<IdentityUserDto> RegisterAsync(RegisterDto input)
    //    //{
    //    //    return AccountAppService.RegisterAsync(input);
    //    //}

    //    //[HttpPost]
    //    //[Route("send-password-reset-code")]
    //    //public virtual Task SendPasswordResetCodeAsync(SendPasswordResetCodeDto input)
    //    //{
    //    //    return AccountAppService.SendPasswordResetCodeAsync(input);
    //    //}

    //    //[HttpPost]
    //    //[Route("reset-password")]
    //    //public virtual Task ResetPasswordAsync(ResetPasswordDto input)
    //    //{
    //    //    return AccountAppService.ResetPasswordAsync(input);
    //    //}
    //}
}
