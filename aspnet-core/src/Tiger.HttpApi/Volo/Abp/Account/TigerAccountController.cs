using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Account;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Identity;

namespace Tiger.Volo.Abp.Account
{
    [RemoteService(Name = AccountRemoteServiceConsts.RemoteServiceName)]
    [Area("tiger_account")]
    [Route("api/account1")]
    public class TigerAccountController : AccountController
    {
        protected new IAccountAppService AccountAppService { get; }

        public TigerAccountController(IAccountAppService accountAppService) : base(accountAppService)
        {
            AccountAppService = accountAppService;
        }

        //[HttpPost]
        //[Route("register1")]
        //public virtual async Task<IdentityUserDto> RegisterAsync(RegisterDto input)
        //{   
        //    // TODO: 需要列表 identity server 配置 root url 等
        //    return await AccountAppService.RegisterAsync(input);
        //}

        //[HttpPost]
        //[Route("send-password-reset-code2")]
        //public virtual async Task SendPasswordResetCodeAsync(SendPasswordResetCodeDto input)
        //{
        //    return await AccountAppService.SendPasswordResetCodeAsync(input);
        //}

        ///// <summary>
        ///// 重置密码
        ///// </summary>
        ///// <param name="input"></param>
        ///// <returns></returns>
        //[HttpPost]
        //[Route("reset-password3")]
        //public virtual Task ResetPasswordAsync(ResetPasswordDto input)
        //{
        //    return AccountAppService.ResetPasswordAsync(input);
        //}
    }
}
