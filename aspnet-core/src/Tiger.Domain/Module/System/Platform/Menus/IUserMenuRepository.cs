using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Tiger.Module.System.Platform.Menus
{
    public interface IUserMenuRepository : IBasicRepository<UserMenu, Guid>
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
