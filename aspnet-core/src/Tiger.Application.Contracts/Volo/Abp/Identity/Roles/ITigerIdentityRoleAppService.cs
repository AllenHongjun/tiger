﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tiger.Volo.Abp.Identity.ClaimTypes.Dto;
using Tiger.Volo.Abp.Identity.OrganizationUnits.Dto;
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
    }
}
