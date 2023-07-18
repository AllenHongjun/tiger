using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Tiger.Module.System.Platform.Menus
{
    public interface IUserMenuRepository
    {
        /// <summary>
        /// 用户是否拥有菜单
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="menuName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> UserHasInMenuAsync(
            Guid userid,
            string menuName,
            CancellationToken cancellationToken =default);

        Task<List<UserMenu>> GetListByUserIdAsycn(
            Guid userId,
            CancellationToken cancellationToken = default);

        Task<Menu> GetStartUpMenuAsync(
            Guid userId,
            CancellationToken cancellationToken = default);
    }
}
