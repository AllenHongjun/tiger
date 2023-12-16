using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tiger.Volo.Abp.Identity.OrganizationUnits.Dto;
using Tiger.Volo.Abp.Identity.Users.Dto;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Identity;
using IdentityUserDto = Volo.Abp.Identity.IdentityUserDto;

namespace Tiger.Volo.Abp.Identity.Users
{
    public interface ITigerIdentityUserAppService : IApplicationService
    {

        /// <summary>
        /// 分页获取用户列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<IdentityUserDto>> GetListAsync(IdentityUserGetListInput input);

        /// <summary>
        /// 从xlsx导入角色
        /// </summary>
        /// <param name="importExcelFile"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        Task ImportUserFromXlsxAsync(IFormFile importExcelFile);

        /// <summary>
        /// 将用户导出xlxs
        /// </summary>
        /// <param name="input">input</param>
        /// <returns>查询到的所有用户</returns>
        Task<IActionResult> ExportUsersToXlsxAsync(IdentityUserGetListInput input);

        /// <summary>
        /// 添加用户同时关联组织机构
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ouId"></param>
        /// <returns></returns>
        Task AddToOrganizationUnitsAsync(Guid userId, List<Guid> ouId);


        /// <summary>
        /// </summary>
        /// <param name="id">user id</param>
        /// <param name="includeDetails"></param>
        /// <returns></returns>
        Task<ListResultDto<OrganizationUnitDto>> GetListOrganizationUnitsAsync(Guid id, bool includeDetails = false);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<IdentityUserDto> CreateAsync(IdentityUserOrgCreateDto input);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<IdentityUserDto> UpdateAsync(Guid id, IdentityUserOrgUpdateDto input);
        Task ChangePasswordAsync(Guid id, IdentityUserSetPasswordInput input);

        /// <summary>
        /// 启用双因素认证
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        Task ChangeTwoFactorEnableAsync(Guid id, TwoFactorEnabledDto input);

        Task LockAsync(Guid id, int seconds);
        Task UnlockAsync(Guid id);

        Task<ListResultDto<IdentityUserClaimDto>> GetClaimsAsync(Guid id);
        Task AddClaimAsync(Guid id, IdentityUserClaimCreateDto input);
        Task UpdateClaimAsync(Guid id, IdentityUserClaimUpdateDto input);
        Task DeleteClaimAsync(Guid id, IdentityUserClaimDeleteDto input);
    }
}
