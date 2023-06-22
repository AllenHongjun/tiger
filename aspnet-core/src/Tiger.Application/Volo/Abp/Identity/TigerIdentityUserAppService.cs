using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Tiger.Volo.Abp.Identity.OrganizationUnits.Dto;
using Tiger.Volo.Abp.Identity.Users;
using Tiger.Volo.Abp.Identity.Users.Dto;
using Tiger.Volo.Abp.IdentityServer.IdentityResources;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.BlobStoring;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Emailing;
using Volo.Abp.Identity;
using Volo.Abp.Sms;
using IdentityUser = Volo.Abp.Identity.IdentityUser;

namespace Tiger.Volo.Abp.Identity
{   
    /// <summary>
    /// 用户管理
    /// </summary>
    [RemoteService(IsEnabled = false)]
    [Dependency(ReplaceServices = true)]
    [ExposeServices(typeof(IIdentityUserAppService),
        typeof(IdentityUserAppService),
        typeof(ITigerIdentityUserAppService),
        typeof(TigerIdentityUserAppService))]
    public class TigerIdentityUserAppService : IdentityUserAppService, 
        ITigerIdentityUserAppService
    {
        private readonly IBlobContainer _blobContainer;
        private readonly IEmailSender _emailSender;
        private readonly ISmsSender _smsSender;
        private readonly IIdentityUserRepository _identityUserRepository;
        public TigerIdentityUserAppService(
            IdentityUserManager userManager, 
            IIdentityUserRepository userRepository, 
            IIdentityRoleRepository roleRepository,
            IBlobContainer blobContainer,
            IEmailSender emailSender,
            ISmsSender smsSender,
            IIdentityUserRepository identityUserRepository
            ) : base(userManager, userRepository, roleRepository)
        {
            _blobContainer = blobContainer;
            _emailSender = emailSender;
            _smsSender = smsSender;
            _identityUserRepository = identityUserRepository;
        }




        #region 测试代码
        /// <summary>
        /// 发送短信
        /// </summary>
        /// <returns></returns>
        public async Task SmsSend()
        {
            await _smsSender.SendAsync(
                "15958456864",        // target phone number
                "This is test sms..."   // message text
            );
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <returns></returns>
        public async Task EmailSend()
        {
            await _emailSender.SendAsync(
                "hongjy1991@gmail.com",     // target email address
                "这是一封测试邮件",         // subject
                "这里是邮件的内容"  // email body
            );
        }

        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public async Task SaveBytesAsync(byte[] bytes)
        {
            await _blobContainer.SaveAsync("my-blob-1", bytes);
        }

        /// <summary>
        /// 获取文件
        /// </summary>
        /// <returns></returns>
        public async Task<byte[]> GetBytesAsync()
        {
            return await _blobContainer.GetAllBytesOrNullAsync("my-blob-1");
        }

        /*
         子类继承父类来，来扩展abp原有的方法
         可以从写覆盖原来的方法。也可以扩展原有的功能。
         
         */
        #endregion

        #region IdentityUser
        /// <summary>
        /// 将用户关联组织机构
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ouId"></param>
        /// <returns></returns>
        public virtual async Task AddToOrganizationUnitsAsync(Guid userId, List<Guid> ouId)
        {
            await UserManager.SetOrganizationUnitsAsync(userId, ouId.ToArray());
        }

        public override async Task<IdentityUserDto> CreateAsync(IdentityUserCreateDto input)
        {
            /*
             移除用户关联的角色
             新增用户关联的新角色
             
             */
            return await base.CreateAsync(input);
        }

        /// <summary>
        /// 获取用户关联的组织机构
        /// </summary>
        /// <param name="id">组织机构id</param>
        /// <param name="includeDetails"></param>
        /// <returns></returns>
        public virtual async Task<ListResultDto<OrganizationUnitDto>> GetListOrganizationUnitsAsync(Guid id, bool includeDetails = false)
        {
            var list = await UserRepository.GetOrganizationUnitsAsync(id, includeDetails);
            return new ListResultDto<OrganizationUnitDto>(
                ObjectMapper.Map<List<OrganizationUnit>, List<OrganizationUnitDto>>(list)
            );
        }

        /// <summary>
        /// 添加用户，同时用户关联组织id
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public virtual async Task<IdentityUserDto> CreateAsync(IdentityUserOrgCreateDto input)
        {
            var identity = await CreateAsync(
                ObjectMapper.Map<IdentityUserOrgCreateDto, IdentityUserCreateDto>(input)
            );
            if (input.OrgIds != null)
            {
                await UserManager.SetOrganizationUnitsAsync(identity.Id, input.OrgIds.ToArray());
            }
            return identity;
        }

        /// <summary>
        /// 修改用户及用户关联的组织
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public virtual async Task<IdentityUserDto> UpdateAsync(Guid id, IdentityUserOrgUpdateDto input)
        {
            var update = ObjectMapper.Map<IdentityUserOrgUpdateDto, IdentityUserUpdateDto>(input);
            var result = await base.UpdateAsync(id, update);
            await UserManager.SetOrganizationUnitsAsync(result.Id, input.OrgIds.ToArray());
            await CurrentUnitOfWork.SaveChangesAsync();
            return result;
        }



        /// <summary>
        /// 设置用户关联的组织机构
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize(TigerIdentityPermissions.Users.ManageOrganizationUnits)]
        public async virtual Task SetOrganizationUnitsAsync(Guid id, IdentityUserOrganizationUnitUpdateDto input)
        {
            var user = await UserManager.GetByIdAsync(id);

            await UserManager.SetOrganizationUnitsAsync(user, input.OrganizationUnitIds);

            await CurrentUnitOfWork.SaveChangesAsync();
        }

        /// <summary>
        /// 移除用户关联的组织
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ouId"></param>
        /// <returns></returns>
        [Authorize(TigerIdentityPermissions.Users.ManageOrganizationUnits)]
        public async virtual Task RemoveOrganizationUnitsAsync(Guid id, Guid ouId)
        {
            await UserManager.RemoveFromOrganizationUnitAsync(id, ouId);

            await CurrentUnitOfWork.SaveChangesAsync();
        } 
        #endregion

        #region Claim

        public async virtual Task<ListResultDto<IdentityClaimDto>> GetClaimsAsync(Guid id)
        {
            var user = await UserManager.GetByIdAsync(id);

            return new ListResultDto<IdentityClaimDto>(ObjectMapper.Map<ICollection<IdentityUserClaim>, List<IdentityClaimDto>>(user.Claims));
        }

        [Authorize(TigerIdentityPermissions.Users.ManageClaims)]
        public async virtual Task AddClaimAsync(Guid id, IdentityUserClaimCreateDto input)
        {
            var user = await UserManager.GetByIdAsync(id);
            var claim = new Claim(input.ClaimType, input.ClaimValue);
            if (user.FindClaim(claim) != null)
            {
                throw new UserFriendlyException(L["UserClaimAlreadyExists"]);
            }
            user.AddClaim(GuidGenerator, claim);
            (await UserManager.UpdateAsync(user)).CheckErrors();

            await CurrentUnitOfWork.SaveChangesAsync();
        }

        [Authorize(TigerIdentityPermissions.Users.ManageClaims)]
        public async virtual Task UpdateClaimAsync(Guid id, IdentityUserClaimUpdateDto input)
        {
            var user = await UserManager.GetByIdAsync(id);
            var oldClaim = new Claim(input.ClaimType, input.ClaimValue);
            var newClaim = new Claim(input.ClaimType, input.NewClaimValue);
            user.ReplaceClaim(oldClaim, newClaim);
            (await UserManager.UpdateAsync(user)).CheckErrors();

            await CurrentUnitOfWork.SaveChangesAsync();
        }

        [Authorize(TigerIdentityPermissions.Users.ManageClaims)]
        public async virtual Task DeleteClaimAsync(Guid id, IdentityUserClaimDeleteDto input)
        {
            var user = await UserManager.GetByIdAsync(id);
            user.RemoveClaim(new Claim(input.ClaimType, input.ClaimValue));
            (await UserManager.UpdateAsync(user)).CheckErrors();

            await CurrentUnitOfWork.SaveChangesAsync();
        }

        #endregion

        #region 账号安全
        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        [Authorize(TigerIdentityPermissions.Users.ResetPassword)]
        public async Task ChangePasswordAsync(Guid id, IdentityUserSetPasswordInput input)
        {
            var user = await GetUserAsync(id);

            if (user.IsExternal)
            {
                throw new BusinessException(code: Volo.Abp.Identity.IdentityErrorCodes.ExternalUserPasswordChange);
            }

            if (user.PasswordHash == null)
            {
                (await UserManager.AddPasswordAsync(user, input.Password)).CheckErrors();
            }
            else
            {
                var token = await UserManager.GeneratePasswordResetTokenAsync(user);

                (await UserManager.ResetPasswordAsync(user, token, input.Password)).CheckErrors();
            }

            await CurrentUnitOfWork.SaveChangesAsync();

        }

        /// <summary>
        /// 启用双因素认证
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize(IdentityPermissions.Users.Update)]
        public async Task ChangeTwoFactorEnableAsync(Guid id, TwoFactorEnabledDto input)
        {
            var user = await GetUserAsync(id);

            (await UserManager.SetTwoFactorEnabledAsync(user, input.Enabled)).CheckErrors();

            await CurrentUnitOfWork.SaveChangesAsync();
        }



        /// <summary>
        /// 锁定用户
        /// </summary>
        /// <param name="id"></param>
        /// <param name="seconds">锁定时间</param>
        /// <returns></returns>
        public async Task LockAsync(Guid id, int seconds)
        {
            var user = await GetUserAsync(id);

            var endDate = new DateTimeOffset(Clock.Now).AddSeconds(seconds);
            (await UserManager.SetLockoutEndDateAsync(user, endDate)).CheckErrors();

            await CurrentUnitOfWork.SaveChangesAsync();
        }



        /// <summary>
        /// 解锁用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(IdentityPermissions.Users.Update)]
        public async Task UnlockAsync(Guid id)
        {
            var user = await GetUserAsync(id);
            (await UserManager.SetLockoutEndDateAsync(user, null)).CheckErrors();

            await CurrentUnitOfWork.SaveChangesAsync();
        }


        protected async virtual Task<IdentityUser> GetUserAsync(Guid id)
        {
            //await IdentityOptions.SetAsync();
            var user = await UserManager.GetByIdAsync(id);

            return user;
        } 
        #endregion

    }
}
