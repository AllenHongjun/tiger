using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AspNet.Security.OAuth.GitHub;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Volo.Abp.AspNetCore.Mvc;

namespace Tiger.Controllers
{   
    [ApiExplorerSettings(GroupName = "auth")]
    [Route("api/[controller]")]
    [ApiController]
    public class Authentication : AbpController
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IConfiguration _configuration;

        public Authentication(IHttpContextAccessor contextAccessor, IConfiguration configuration)
        {
            _contextAccessor = contextAccessor;
            _configuration = configuration;
        }

        [HttpGet("~/signin")]
        public async Task<IActionResult> SignIn(string provider, string redirectUrl)
        {
            var request = _contextAccessor.HttpContext.Request;
            var url =
            $"{request.Scheme}://{request.Host}{request.PathBase}{request.Path}-callback?provider={provider}&redirectUrl={redirectUrl}";
            var properties = new AuthenticationProperties { RedirectUri = url };
            properties.Items["LoginProviderKey"] = provider;
             var res = Challenge(properties, provider);
            return res;
        }


        [HttpGet("~/signin-callback")]
        public async Task<IActionResult> Home(string provider = null, string redirectUrl = "")
        {
            var authenticateResult = await _contextAccessor.HttpContext.AuthenticateAsync(provider);
            if (!authenticateResult.Succeeded) return Redirect(redirectUrl);
            var openIdClaim = authenticateResult.Principal.FindFirst(ClaimTypes.NameIdentifier);
            if (openIdClaim == null || string.IsNullOrWhiteSpace(openIdClaim.Value))
                return Redirect(redirectUrl);

            //TODO 记录授权成功后的信息  

            //string email = authenticateResult.Principal.FindFirst(ClaimTypes.Email)?.Value;
            //string name = authenticateResult.Principal.FindFirst(ClaimTypes.Name)?.Value;
            string gitHubName = authenticateResult.Principal.FindFirst(GitHubAuthenticationConstants.Claims.Name)?.Value;
            string gitHubUrl = authenticateResult.Principal.FindFirst(GitHubAuthenticationConstants.Claims.Url)?.Value;
            //startup 中 AddGitHub配置项  options.ClaimActions.MapJsonKey(LinConsts.Claims.AvatarUrl, "avatar_url");  
            //string avatarUrl = authenticateResult.Principal.FindFirst(LinConsts.Claims.AvatarUrl)?.Value;

            return Redirect($"{redirectUrl}?openId={openIdClaim.Value}");
        }


        [HttpGet("~/OpenId")]
        public async Task<string> OpenId(string provider = null)
        {
            var authenticateResult = await _contextAccessor.HttpContext.AuthenticateAsync(provider);
            if (!authenticateResult.Succeeded) return null;
            var openIdClaim = authenticateResult.Principal.FindFirst(ClaimTypes.NameIdentifier);
            return openIdClaim?.Value;
        }
    }
}
