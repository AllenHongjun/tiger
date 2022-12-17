using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tiger.Volo.Abp.Identity.OrganizationUnits.Dto;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Auditing;
using Volo.Abp.Identity;

namespace Tiger.Volo.Abp.Identity.OrganizationUnits
{
    /// <summary>
    /// 组织机构
    /// </summary>
    public interface IOrganizationUnitAppService :
        ICrudAppService<OrganizationUnitDto, Guid, GetOrganizationUnitInput, OrganizationUnitCreateDto, OrganizationUnitUpdateDto>
    {
        ///// <summary>
        ///// 获取根组织列表
        ///// </summary>
        ///// <returns></returns>
        //Task<ListResultDto<OrganizationUnitDto>> GetRootListAsync();

        /// <summary>
        /// 获取组织明细
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<OrganizationUnitDto> GetDetailsAsync(Guid id);

        //Task<PagedResultDto<OrganizationUnitDto>> GetListDetailsAsync(GetOrganizationUnitInput input);

        /// <summary>
        /// 获取所有组织列表数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ListResultDto<OrganizationUnitDto>> GetAllListAsync(GetAllOrgnizationUnitInput input);

        ///// <summary>
        ///// 获取所有组织列表明细数据
        ///// </summary>
        ///// <param name="input"></param>
        ///// <returns></returns>
        //Task<ListResultDto<OrganizationUnitDto>> GetAllListDetailsAsync(GetAllOrgnizationUnitInput input);

        ///// <summary>
        ///// 获取子集组织
        ///// </summary>
        ///// <param name="parentId"></param>
        ///// <param name="recursive"></param>
        ///// <returns></returns>
        //Task<List<OrganizationUnitDto>> GetChildrenAsync(Guid parentId, bool recursive = false);

        Task<string> GetNextChildCodeAsync(Guid? parentId);

        Task MoveAsync(Guid id, Guid? parentId);

        /// <summary>
        /// 获取组织关联的用户
        /// </summary>
        /// <param name="ouId"></param>
        /// <param name="userInput"></param>
        /// <returns></returns>
        Task<PagedResultDto<IdentityUserDto>> GetUsersAsync(Guid? ouId, GetIdentityUsersInput userInput);

        /// <summary>
        /// 获取组织关联的角色
        /// </summary>
        /// <param name="ouId"></param>
        /// <param name="roleInput"></param>
        /// <returns></returns>
        Task<PagedResultDto<IdentityRoleDto>> GetRolesAsync(Guid? ouId, PagedAndSortedResultRequestDto roleInput);
        Task<PagedResultDto<IdentityRoleDto>> GetUnaddedRolesAsync(Guid id, GetOrganizationUnitInput input);
        Task<ListResultDto<OrganizationUnitDto>> GetRootAsync();
        Task<ListResultDto<OrganizationUnitDto>> FindChildrenAsync(GetOrganizationUnitChildrenDto input);
        Task<OrganizationUnitDto> GetLastChildOrNullAsync(Guid? parentId);
        
        Task<ListResultDto<string>> GetRoleNamesAsync(Guid ouid);
        Task AddRolesAsync(Guid id, OrganizationUnitAddRolesDto input);
        Task RemoveRoleAsync(Guid id, Guid RoleId);
        Task<PagedResultDto<IdentityUserDto>> GetUnaddedUsersAsync(Guid id, GetOrganizationUnitInput input);
        Task AddUsersAsync(Guid id, OrganizationUnitAddUsersDto input);
        Task RemoveUserAsync(Guid ouId, Guid userId);
        
    }
}
