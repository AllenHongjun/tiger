using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tiger.Volo.Abp.Identity.OrganizationUnits.Dto;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Identity;

namespace Tiger.Volo.Abp.Identity.Users
{
    public interface ITigerIdentityUserAppService : IApplicationService
    {
        /// <summary>
        /// 用户管理组织
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ouId"></param>
        /// <returns></returns>
        Task AddToOrganizationUnitsAsync(Guid userId, List<Guid> ouId);


        /// <summary>
        /// get list OrganizationUnits
        /// </summary>
        /// <param name="id">user id</param>
        /// <param name="includeDetails"></param>
        /// <returns></returns>
        Task<ListResultDto<OrganizationUnitDto>> GetListOrganizationUnitsAsync(Guid id, bool includeDetails = false);

        /// <summary>
        /// 添加组织
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<IdentityUserDto> CreateAsync(IdentityUserOrgCreateDto input);

        /// <summary>
        /// 修改组织
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<IdentityUserDto> UpdateAsync(Guid id, IdentityUserOrgUpdateDto input);
    }
}
