using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Tiger.Module.System.Platform.Menus
{
    public interface IRoleMenuRepository:IBasicRepository<RoleMenu, Guid>
    {
        /// <summary>
        /// 角色是否拥有菜单
        /// </summary>
        /// <param name="roleName"></param>
        /// <param name="menuName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> RoleHasInMenuAsync(
            string roleName,
            string menuName,
            CancellationToken cancellationToken = default);

        Task<List<RoleMenu>> GetListByRoleNameAsync(
            string roleName,
            CancellationToken cancellationToken = default);

        Task<Menu> GetStartUpMenuAsync(
            IEnumerable<string> roleNames,
            CancellationToken cancellationToken = default);
    }
}
