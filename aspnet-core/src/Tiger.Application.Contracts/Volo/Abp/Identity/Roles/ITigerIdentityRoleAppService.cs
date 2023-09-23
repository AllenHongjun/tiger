﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tiger.Volo.Abp.Identity.ClaimTypes.Dto;
using Tiger.Volo.Abp.Identity.OrganizationUnits.Dto;
using Tiger.Volo.Abp.Identity.Roles.Dto;
using Tiger.Volo.Abp.IdentityServer.IdentityResources;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Identity;

namespace Tiger.Volo.Abp.Identity.Roles
{
    public interface ITigerIdentityRoleAppService : IApplicationService
    {
        

        /// <summary>
        /// 角色管理组织
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="ouId"></param>
        /// <returns></returns>
        Task AddToOrganizationUnitAsync(Guid roleId, Guid ouId);

        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<IdentityRoleDto> CreateAsync(IdentityRoleOrgCreateDto input);
        
        Task<PagedResultDto<IdentityRoleDto>> GetListAsync(GetIdentityRolesInput input);

        /// <summary>
        /// 添加角色声明
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        Task AddClaimAsync(Guid id, IdentityRoleClaimCreateDto input);

        /// <summary>
        /// 更新角色声明
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        Task UpdateClaimAsync(Guid id, IdentityRoleClaimUpdateDto input);

        /// <summary>
        /// 删除角色声明
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        Task DeleteClaimAsync(Guid id, IdentityRoleClaimDeleteDto input);

        /// <summary>
        /// 获取角色声明
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ListResultDto<IdentityRoleClaimDto>> GetClaimsAsync(Guid id);
    }
}
