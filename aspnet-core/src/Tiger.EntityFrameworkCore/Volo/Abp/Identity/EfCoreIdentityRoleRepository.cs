using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;

namespace Tiger.Volo.Abp.Identity
{
    /// <summary>
    /// Efcore 角色仓储实现
    /// </summary>
    public class EfCoreTigerIdentityRoleRepository
        : EfCoreIdentityRoleRepository, ITigerIdentityRoleRepository
    {
        public EfCoreTigerIdentityRoleRepository(
            IDbContextProvider<IIdentityDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }


        /// <summary>
        /// 获取每个角色的用户数量
        /// </summary>
        /// <param name="roleIds"></param>
        /// <returns></returns>
        /// <remarks>
        /// bug-fix: https://stackoverflow.com/questions/58138556/client-side-groupby-is-not-supported
        /// </remarks>
        public async Task<Dictionary<Guid,int>> GeUserCountOfRoleAsync(List<Guid> roleIds)
        {
            var list = await DbContext.Set<IdentityUserRole>()
               .Where(x => roleIds.Any(roleId => roleId == x.RoleId))
               .ToListAsync();
             var roleUserCountDic = list.GroupBy(x => x.RoleId).ToDictionary(g => g.Key, g => g.Count());
            return roleUserCountDic;
        }


        public async Task<List<IdentityRole>> GetListByIdListAsync(List<Guid> roleIds, bool includeDetails = false, CancellationToken cancellation = default)
        {
            return await (DbContext.Set<IdentityRole>()
                .IncludeDetails(includeDetails)
                .Where(role => roleIds.Contains(role.Id))
                .ToListAsync(GetCancellationToken(cancellation)));
        }


        /// <summary>
        /// 移动当前角色的所有用户到目标角色
        /// </summary>
        /// <param name="roleId">角色id</param>
        /// <param name="targetRoleId">目标角色id</param>
        /// <param name="cancelAssign">取消分配用户</param>
        /// <returns></returns>
        public  async Task MoveAllUsersAsync(Guid roleId, Guid targetRoleId, bool cancelAssign)
        {
            var users = await DbContext.Set<IdentityUser>()
               .Where(x => x.Roles.Any(role => role.RoleId == roleId) ).ToListAsync();
            foreach (var user in users)
            {
                user.RemoveRole(roleId);
                if (!cancelAssign) user.AddRole(targetRoleId);
            }
        }



        /// <summary>
        /// 根据角色id查询关联的组织列表
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="includeDetails"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<OrganizationUnit>> GetOrganizationUnitListAsync(Guid roleId, bool includeDetails = false, CancellationToken cancellation = default)
        {
            // 使用表名的缩写命名
            var query = from roleOU in DbContext.Set<OrganizationUnitRole>()
                        join ou in DbContext.OrganizationUnits.IncludeDetails(includeDetails) 
                        on roleOU.OrganizationUnitId equals ou.Id
                        where roleOU.RoleId == roleId
                        select ou;

            return await query.ToListAsync(GetCancellationToken(cancellation));
        }

        /// <summary>
        /// 获取角色名称关联的组织
        /// </summary>
        /// <param name="roleNames"></param>
        /// <param name="includeDetails"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<List<OrganizationUnit>> GetOrganizationUnitListAsync(IEnumerable<string> roleNames, bool includeDetails = false, CancellationToken cancellationToken = default)
        {
            var query = from ruleOu in DbContext.Set<OrganizationUnitRole>()
                        join role in DbContext.Roles on ruleOu.RoleId equals role.Id

                        join ou in DbContext.OrganizationUnits.IncludeDetails(includeDetails)
                            // 通过关联表的组织id和组织表的id关联
                            on ruleOu.OrganizationUnitId equals ou.Id
                        where roleNames.Contains(role.Name)
                        select ou;

            return await query.ToListAsync(GetCancellationToken(cancellationToken));
        }


        /// <summary>
        /// 通过组织id列表获取关联的角色列表
        /// </summary>
        /// <param name="organizationUnitIds"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<List<IdentityRole>> GetRolesInOrganizationsListAsync(List<Guid> organizationUnitIds, CancellationToken cancellationToken = default)
        {
            var query = from roleOU in DbContext.Set<OrganizationUnitRole>()
                        join role in DbContext.Set<IdentityRole>() on roleOU.OrganizationUnitId equals role.Id
                        where organizationUnitIds.Contains(role.Id)
                        select role;
            return await query.ToListAsync(GetCancellationToken(cancellationToken));

        }

        /// <summary>
        /// 通过组织id获取关联角色列表
        /// </summary>
        /// <param name="organizationUnitId"></param>
        /// <param name="cancelToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<IdentityRole>> GetRolesInOrganizationUnitAsync(Guid organizationUnitId, CancellationToken cancelToken = default)
        {
            var query = from roleOu in DbContext.Set<OrganizationUnitRole>()
                        join role in DbContext.Set<IdentityRole>() on roleOu.RoleId equals role.Id
                        where roleOu.RoleId == organizationUnitId
                        select role;
            return await query.ToListAsync(GetCancellationToken(cancelToken));
        }

        /// <summary>
        /// 通过组织code(包含子组织)获取关联的角色列表
        /// </summary>
        /// <param name="code"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<IdentityRole>> GetRolesInOrganizationUnitWithChildrenAsync(string code, CancellationToken cancellationToken = default)
        {

            var query = from roleOU in DbContext.Set<OrganizationUnitRole>()
                        join role in DbContext.Roles on roleOU.RoleId equals role.Id
                        join ou in DbContext.OrganizationUnits on roleOU.OrganizationUnitId equals ou.Id
                        where ou.Code.StartsWith(code)
                        select role;
            return await query.ToListAsync(GetCancellationToken(cancellationToken));
        }
    }
}
