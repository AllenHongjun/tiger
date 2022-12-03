using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Identity;

namespace Tiger.Volo.Abp.Identity
{   
    /// <summary>
    /// 用户
    /// </summary>
    public interface ITigerIdentityUserRepository : IIdentityUserRepository
    {

        /// <summary>
        /// 判断手机号是否被使用
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> IsPhoneNumberUsedAsync(
            string phoneNumber,
            CancellationToken cancellationToken
            );

        /// <summary>
        /// 手机号是否绑定
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> IsPhoneNumberConfirmedAsync(
            string phoneNumber,
            CancellationToken cancellationToken = default
            );


        /// <summary>
        /// 邮箱是否绑定
        /// </summary>
        /// <param name="normalizedEmail"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> IsEmailConfirmedAsync(
            string normalizedEmail,
            CancellationToken cancellationToken = default
            );

        /// <summary>
        /// 通过手机号查询用户
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <param name="isConfiremed"></param>
        /// <param name="includeDetails"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IdentityUser> FindByPhoneNumberAsync(
            string phoneNumber,
            bool isConfiremed = true,
            bool includeDetails = false,
            CancellationToken cancellationToken = default
            );

        /// <summary>
        /// 通过用户主键id列表获取用户
        /// </summary>
        /// <param name="userIds"></param>
        /// <param name="includeDetails"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<List<IdentityUser>> GetListByIdListAsync(
            List<Guid> userIds,
            bool includeDetails = false,
            CancellationToken cancellationToken = default
            );


        /// <summary>
        /// 获取用户所属的组织机构列表
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="filter"></param>
        /// <param name="includeDetails"></param>
        /// <param name="skipCount"></param>
        /// <param name="maxResultCount"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<List<OrganizationUnit>> GetUserOrganizationUnitListAsync(
            Guid userId,
            string filter = null,
            bool includeDetails = false,
            int skipCount = 1,
            int maxResultCount = 10,
            CancellationToken cancellationToken = default
            );


        /// <summary>
        /// 获取组织机构中用户的数量
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="filter"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<long> GetUsersInOrganizationUnitCountAsync(
            Guid organizationId,
            string filter = null,
            CancellationToken cancellationToken = default
            );

        /// <summary>
        /// 获取组织机构中的用户列表
        /// </summary>
        /// <param name="organizationUnitId"></param>
        /// <param name="filter"></param>
        /// <param name="skipCount"></param>
        /// <param name="maxResultCount"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        Task<List<IdentityUser>> GetUsersInOrganiztionUnitAsync(
            Guid organizationUnitId,
            string filter = null,
            int skipCount = 1,
            int maxResultCount = 50,
            CancellationToken cancellation = default
            );



        Task<long> GetUsersInOrganizationUnitWithChildrenCountAsync(
            string code,
            string filter = null,
            CancellationToken cancellationToken = default
        );

        Task<List<IdentityUser>> GetUsersInOrganizationUnitWithChildrenAsync(
            string code,
            string filter = null,
            int skipCount = 1,
            int maxResultCount = 10,
            CancellationToken cancellationToken = default
        );

    }
}
