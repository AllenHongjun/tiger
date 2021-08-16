using System;
using System.Collections.Generic;
using System.IO;
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
    [ApiExplorerSettings(GroupName = "api")]
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


        /// <summary>
        /// 上传 文件,并返回相对url(不包含 host port wwwroot)
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [Route("UploadFile")]
        [HttpPost]
        public async Task<string> UploadFile(IFormFile file)
        {
            if (file == null)
            {
                return null;
            }
            string webRootPath = "/wwwroot"; // wwwroot 文件夹
            string uploadPath = Path.Combine("uploads", DateTime.Now.ToString("yyyyMMdd"));
            string dirPath = Path.Combine(webRootPath, uploadPath);
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }
            string fileExt = Path.GetExtension(file.FileName).Trim('.'); //文件扩展名，不含“.”
            string newFileName = Guid.NewGuid().ToString().Replace("-", "") + "." + fileExt; //随机生成新的文件名
            var filePath = Path.Combine(dirPath, newFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            var url = $@"\{uploadPath}\{newFileName}";
            return url;
        }
    }
}
