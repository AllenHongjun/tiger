using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Identity;

namespace Tiger.Volo.Abp.Identity
{   
    /// <summary>
    /// 角色仓储接口
    /// </summary>
    public interface ITigerIdentityRoleRepository: IIdentityRoleRepository
    {


        Task<Dictionary<Guid, int>> GeUserCountOfRoleAsync(List<Guid> roleIds);

        /// <summary>
        /// 根据角色id获取角色列表
        /// </summary>
        /// <param name="roleIds"></param>
        /// <param name="includeDetails"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        Task<List<IdentityRole>> GetListByIdListAsync(
            List<Guid> roleIds,
            bool includeDetails = false,
            CancellationToken cancellation = default);


        /// <summary>
        /// 根据id获取角色关联的部门
        /// </summary>
        /// <param name="id"></param>
        /// <param name="includeDetails"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        Task<List<OrganizationUnit>> GetOrganizationUnitListAsync(
            Guid roleId,
            bool includeDetails = false,
            CancellationToken cancellation = default);


        /// <summary>
        /// 根据角色名称获取关联的组织
        /// </summary>
        /// <param name="roleNames"></param>
        /// <param name="includeDetails"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<List<OrganizationUnit>> GetOrganizationUnitListAsync(
            IEnumerable<string> roleNames,
            bool includeDetails = false,
            CancellationToken cancellationToken = default);


        /// <summary>
        /// 根据组织id获取关联的角色列表
        /// </summary>
        /// <param name="organizationUnitId"></param>
        /// <param name="cancelToken"></param>
        /// <returns></returns>
        Task<List<IdentityRole>> GetRolesInOrganizationUnitAsync(
            Guid organizationUnitId,
            CancellationToken cancelToken = default);


        /// <summary>
        /// 根据组织id列表获取关联角色列表
        /// </summary>
        /// <param name="organizationUnitIds"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<List<IdentityRole>> GetRolesInOrganizationsListAsync(
            List<Guid> organizationUnitIds,
            CancellationToken cancellationToken = default);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<List<IdentityRole>> GetRolesInOrganizationUnitWithChildrenAsync(
            string code,
            CancellationToken cancellationToken = default);

    }
}
