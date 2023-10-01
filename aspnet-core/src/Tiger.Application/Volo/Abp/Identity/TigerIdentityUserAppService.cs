using ClosedXML.Excel;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Tiger.Infrastructure.ExportImport;
using Tiger.Infrastructure.ExportImport.Help;
using Tiger.Volo.Abp.Identity.ClaimTypes.Dto;
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
        private readonly ITigerIdentityUserRepository _tigerIdentityUserRepository;
        public TigerIdentityUserAppService(
            IdentityUserManager userManager,
            IIdentityUserRepository userRepository,
            IIdentityRoleRepository roleRepository,
            IBlobContainer blobContainer,
            IEmailSender emailSender,
            ISmsSender smsSender,
            IIdentityUserRepository identityUserRepository
,
            ITigerIdentityUserRepository tigerIdentityUserRepository) : base(userManager, userRepository, roleRepository)
        {
            _blobContainer = blobContainer;
            _emailSender = emailSender;
            _smsSender = smsSender;
            _identityUserRepository = identityUserRepository;
            _tigerIdentityUserRepository=tigerIdentityUserRepository;
        }


        #region IdentityUser
        /// <summary>
        /// 分页查询用户列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize(IdentityPermissions.Users.Default)]
        public async Task<PagedResultDto<IdentityUserDto>> GetListAsync(IdentityUserGetListInput input)
        {
            var count = await _tigerIdentityUserRepository.GetCountAsync(input.RoleId, input.OrganizationUnitId,
                input.UserName, input.PhoneNumber, input.Name,
                input.IsLockedOut, input.NotActive, input.EmailConfirmed, input.IsExternal,
                input.MinCreationTime, input.MaxCreationTime, input.MinModifitionTime, input.MaxModifitionTime, input.Filter);
            var list = await _tigerIdentityUserRepository.GetListAsync(input.RoleId, input.OrganizationUnitId,
                input.UserName, input.PhoneNumber, input.Name,
                input.IsLockedOut, input.NotActive, input.EmailConfirmed, input.IsExternal,
                input.MinCreationTime, input.MaxCreationTime, input.MinModifitionTime, input.MaxModifitionTime, input.Sorting, input.MaxResultCount, input.SkipCount, input.Filter);

            return new PagedResultDto<IdentityUserDto>(
                count,
                ObjectMapper.Map<List<IdentityUser>, List<IdentityUserDto>>(list)
            );
        }

        /// <summary>
        /// 从xlsx导入角色
        /// </summary>
        /// <param name="importexcelfile"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public virtual async Task ImportUserFromXlsxAsync(IFormFile importExcelFile)
        {

            if (importExcelFile != null && importExcelFile.Length > 0)
            {
                using var workbook = new XLWorkbook(importExcelFile.OpenReadStream());
                // get the first worksheet in the workbook
                var worksheet = workbook.Worksheets.FirstOrDefault();
                if (worksheet == null)
                    throw new Exception("No worksheet found");

                //the columns
                var properties = ImportManager.GetPropertiesByExcelCells<IdentityUserDto>(worksheet);

                var manager = new PropertyManager<IdentityUserDto>(properties);
                var iRow = 2;

                while (true)
                {
                    var allColumnsAreEmpty = manager.GetProperties
                        .Select(property => worksheet.Row(iRow).Cell(property.PropertyOrderPosition))
                        .All(cell => cell?.Value == null || string.IsNullOrEmpty(cell.Value.ToString()));

                    if (allColumnsAreEmpty)
                        break;

                    manager.ReadFromXlsx(worksheet, iRow);

                    var user = await _identityUserRepository.GetAsync(manager.GetProperty("Id").GuidValue);

                    var isNew = user == null;

                    user ??= new IdentityUser(
                        GuidGenerator.Create(), 
                        manager.GetProperty(L["UserName"]).StringValue,
                        manager.GetProperty(L["EmailAddress"]).StringValue, 
                        CurrentTenant.Id);

                    foreach (var property in manager.GetProperties)
                    {
                        if (property.PropertyName == L["DisplayName:Surname"]) user.Surname = property.StringValue;
                        if (property.PropertyName == L["DisplayName:Name"]) user.Surname = property.StringValue;
                        if (property.PropertyName == L["PhoneNumber"]) user.SetPhoneNumber(property.StringValue, false);
                    }

                    if (isNew)
                        await UserManager.CreateAsync(user, manager.GetProperty(L["Password"]).StringValue);
                    else
                    {
                        await ChangePasswordAsync(manager.GetProperty("Id").GuidValue, new IdentityUserSetPasswordInput { Password = manager.GetProperty(L["Password"]).StringValue });
                        await UserManager.UpdateAsync(user);
                    }
                    iRow++;
                }

            }
            else
            {
                throw new Exception("文件不能空");
            }
        }


        /// <summary>
        /// 将用户导出xlxs
        /// </summary>
        /// <param name="GetIdentityRolesInput">input</param>
        /// <returns>查询到的所有用户</returns>
        public virtual async Task<IActionResult> ExportUsersToXlsxAsync(IdentityUserGetListInput input)
        {
            var users = await _tigerIdentityUserRepository.GetListAsync(input.RoleId, input.OrganizationUnitId,
                input.UserName, input.PhoneNumber, input.Name,
                input.IsLockedOut, input.NotActive, input.EmailConfirmed, input.IsExternal,
                input.MinCreationTime, input.MaxCreationTime, input.MinModifitionTime, input.MaxModifitionTime, input.Sorting, int.MaxValue, 0, input.Filter);
            var list = ObjectMapper.Map<List<IdentityUser>, List<IdentityUserDto>>(users);

            //property manager 
            var manager = new PropertyManager<IdentityUserDto>(new[]
            {
                new PropertyByName<IdentityUserDto>("Id", p => p.Id),
                new PropertyByName<IdentityUserDto>(L["TenantId"], p => p.TenantId),
                new PropertyByName<IdentityUserDto>(L["UserName"], p => p.UserName),
                new PropertyByName<IdentityUserDto>(L["Password"], p => ""),
                new PropertyByName<IdentityUserDto>(L["DisplayName:Surname"], p => p.Surname),
                new PropertyByName<IdentityUserDto>(L["DisplayName:Name"], p => p.Name),
                new PropertyByName<IdentityUserDto>(L["EmailAddress"], p => p.Email),
                new PropertyByName<IdentityUserDto>(L["PhoneNumber"], p => p.PhoneNumber),
                new PropertyByName<IdentityUserDto>(L["LockoutEnd"], p => p.LockoutEnd),
                new PropertyByName<IdentityUserDto>(L["CreationTime"], p => p.CreationTime),
            });

            var bytes = await manager.ExportToXlsxAsync(list);

            return new FileContentResult(bytes, MimeTypes.TextXlsx);
        } 
        #endregion


        #region IdentityUserOrganizationUnits
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

        public async virtual Task<ListResultDto<IdentityUserClaimDto>> GetClaimsAsync(Guid id)
        {
            var user = await UserRepository.GetAsync(id);

            return new ListResultDto<IdentityUserClaimDto>(ObjectMapper.Map<ICollection<IdentityUserClaim>, List<IdentityUserClaimDto>>(user.Claims));
        }

        [Authorize(TigerIdentityPermissions.Users.ManageClaims)]
        public async virtual Task AddClaimAsync(Guid id, IdentityUserClaimCreateDto input)
        {
            var user = await UserRepository.GetAsync(id);
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
            var user = await UserRepository.GetAsync(id);
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
