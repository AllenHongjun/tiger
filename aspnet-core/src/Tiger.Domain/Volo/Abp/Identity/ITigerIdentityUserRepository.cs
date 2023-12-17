using System;
using System.Collections.Generic;
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
        /// 查询用户数量
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="organizationUnitId"></param>
        /// <param name="userName"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="name"></param>
        /// <param name="isLockedOut"></param>
        /// <param name="notActive"></param>
        /// <param name="emailConfirmed"></param>
        /// <param name="isExternal"></param>
        /// <param name="minCreationTime"></param>
        /// <param name="maxCreationTime"></param>
        /// <param name="minModifitionTime"></param>
        /// <param name="maxModifitionTime"></param>
        /// <param name="filter"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<long> GetCountAsync(Guid? roleId, Guid? organizationUnitId, string userName, string phoneNumber, string name, bool? isLockedOut, bool? notActive, bool? emailConfirmed, bool? isExternal, DateTime? minCreationTime, DateTime? maxCreationTime, DateTime? minModifitionTime, DateTime? maxModifitionTime, string filter = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// 分页查询用户列表
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="organizationUnitId"></param>
        /// <param name="userName"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="name"></param>
        /// <param name="isLockedOut"></param>
        /// <param name="notActive"></param>
        /// <param name="emailConfirmed"></param>
        /// <param name="isExternal"></param>
        /// <param name="minCreationTime"></param>
        /// <param name="maxCreationTime"></param>
        /// <param name="minModifitionTime"></param>
        /// <param name="maxModifitionTime"></param>
        /// <param name="sorting"></param>
        /// <param name="maxResultCount"></param>
        /// <param name="skipCount"></param>
        /// <param name="filter"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<List<IdentityUser>> GetListAsync(Guid? roleId, Guid? organizationUnitId, string userName, string phoneNumber, string name, bool? isLockedOut, bool? notActive, bool? emailConfirmed, bool? isExternal, DateTime? minCreationTime, DateTime? maxCreationTime, DateTime? minModifitionTime, DateTime? maxModifitionTime, string sorting = null, int maxResultCount = 50, int skipCount = 0, string filter = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// 判断手机号是否被使用
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> IsPhoneNumberUsedAsync(
            string phoneNumber,
            CancellationToken cancellationToken = default
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
        Task<List<IdentityUser>> GetListByIdsAsync(
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
        Task<List<IdentityUser>> GetUsersInOrganizationUnitListAsync(
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
