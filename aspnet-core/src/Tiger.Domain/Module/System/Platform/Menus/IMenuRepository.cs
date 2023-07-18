using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Tiger.Module.System.Platform.Menus
{
    public interface IMenuRepository:IBasicRepository<Menu, Guid>
    {
        /// <summary>
        /// 根据id数组查询菜单列表
        /// </summary>
        /// <param name="idList"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<List<Menu>> GetListAsync(
            IEnumerable<Guid> idList,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// 获取最后一个菜单
        /// </summary>
        /// <param name="parentId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Menu> GetLastMenuAsync(
            Guid? parentId = null,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// 根据名称查询菜单
        /// </summary>
        /// <param name="menuName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Menu> FindByNameAsync(
            string menuName,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// 查询主菜单,每一个布局页创建的时候都要创建路径为 / 的主菜单
        /// </summary>
        /// <param name="framework"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Menu> FindMainAsync(
            string framework = "",
            CancellationToken cancellationToken= default);

        /// <summary>
        /// 查询子菜单
        /// </summary>
        /// <param name="parentId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<List<Menu>> GetChildrenAsync(
            Guid? parentId,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// 通过父菜单编码查询子菜单
        /// </summary>
        /// <param name="code"></param>
        /// <param name="parentId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <remarks>
        /// 查询该编码下的所有子菜单返回
        /// </remarks>
        Task<List<Menu>> GetAllChildrenWithParentCodeAsync(
            string code,
            Guid? parentId,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// 查询用户可访问的菜单
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roles"></param>
        /// <param name="framework"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Menu> GetUserMenusAsync(
            Guid userId,
            string[] roles,
            string framework = "",
             CancellationToken cancellationToken = default);


        /// <summary>
        /// 查询角色可访问的菜单
        /// </summary>
        /// <param name="roles"></param>
        /// <param name="framework"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Menu> GetRoleMenusAsync(
            string roles,
            string framework = "",
            CancellationToken cancellationToken = default);

        /// <summary>
        /// 获取菜单数量
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="framework"></param>
        /// <param name="parentId"></param>
        /// <param name="layoutId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> GetCountAsync(
            string filter = "",
            string framework = "",
            Guid? parentId = null,
            Guid? layoutId = null,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// 查询菜单列表
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sorting"></param>
        /// <param name="reverse"></param>
        /// <param name="framework"></param>
        /// <param name="parentId"></param>
        /// <param name="layoutId"></param>
        /// <param name="skipCount"></param>
        /// <param name="maxResultCount"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> GetListAsync(
            string filter = "",
            string sorting = nameof(Menu.Code),
            bool reverse = false,
            string framework = "",
            Guid? parentId = null,
            Guid? layoutId = null,
            int skipCount = 0,
            int maxResultCount = 10,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// 查询所有菜单
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="sorting"></param>
        /// <param name="reverse"></param>
        /// <param name="framework"></param>
        /// <param name="parentId"></param>
        /// <param name="layoutId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<List<Menu>> GetAllAsync(
            string filter = "",
            string sorting = nameof(Menu.Code),
            bool reverse = false,
            string framework = "",
            Guid? parentId = null,
            Guid? layoutId = null,
            CancellationToken cancellationToken = default);

        Task RemoveAllRolesAsync(
            Menu menu,
            CancellationToken token = default);

        Task RemoveAllMembersAsync(
            Menu menu,
            CancellationToken token = default);
    }
}
