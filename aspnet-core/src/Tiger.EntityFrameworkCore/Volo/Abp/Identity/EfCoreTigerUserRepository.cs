using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Tiger.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;

namespace Tiger.Volo.Abp.Identity
{
    /// <summary>
    /// 用户仓储实现（扩展abp用户仓储）
    /// </summary>
    public class EfCoreTigerIdentityUserRepository: EfCoreIdentityUserRepository, ITigerIdentityUserRepository
    {
        public EfCoreTigerIdentityUserRepository(
            IDbContextProvider<TigerDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }


        /// <summary>
        /// 查询用户的数量
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<long> GetCountAsync(Guid? roleId, Guid? organizationUnitId,
            string userName,
            string phoneNumber,
            string name,
            bool? isLockedOut,
            bool? notActive,
            bool? emailConfirmed,
            bool? isExternal,
            DateTime? minCreationTime,
            DateTime? maxCreationTime,
            DateTime? minModifitionTime,
            DateTime? maxModifitionTime,
            string filter = null, CancellationToken cancellationToken = default)
        {
            return await DbContext.Set<IdentityUser>()
                    .WhereIf(roleId.HasValue, x => x.Roles.Any(role => role.RoleId == roleId))
                    .WhereIf(organizationUnitId.HasValue, x => x.OrganizationUnits.Any(og => og.OrganizationUnitId == organizationUnitId))
                    .WhereIf(!userName.IsNullOrEmpty(), x => x.UserName.Contains(userName))
                    .WhereIf(!phoneNumber.IsNullOrEmpty(), x => x.PhoneNumber.Contains(phoneNumber))
                    .WhereIf(!name.IsNullOrEmpty(), x => !x.Name.Contains(name))
                    .WhereIf(isLockedOut.HasValue, x =>
                        (isLockedOut == true && x.LockoutEnd != null &&  x.LockoutEnd >= DateTime.Now) ||
                        (isLockedOut == false && x.LockoutEnd == null || x.LockoutEnd < DateTime.Now))
                    //.WhereIf(notActive.HasValue, x => x.Active == notActive)  用户禁用标识
                    .WhereIf(emailConfirmed.HasValue, x => x.EmailConfirmed == emailConfirmed)
                    .WhereIf(isExternal.HasValue, x => x.IsExternal == isExternal)
                    .WhereIf(minCreationTime.HasValue, x => x.CreationTime >= minCreationTime)
                    .WhereIf(maxCreationTime.HasValue, x => x.CreationTime <= maxCreationTime)
                    .WhereIf(minModifitionTime.HasValue, x => x.LastModificationTime >= minModifitionTime)
                    .WhereIf(maxModifitionTime.HasValue, x => x.LastModificationTime <= maxModifitionTime)
                    .WhereIf(!filter.IsNullOrEmpty(), x => x.UserName.Contains(filter) || x.PhoneNumber.Contains(filter) || x.Email.Contains(filter) || x.Surname.Contains(filter) || x.Name.Contains(filter))
                    .LongCountAsync(GetCancellationToken(cancellationToken));
        }

        /// <summary>
        /// 分页查询用户列表
        /// </summary>
        /// <param name="roleId">角色</param>
        /// <param name="organizationUnitId">组织单位</param>
        /// <param name="userName">用户名</param>
        /// <param name="phoneNumber">手机号码</param>
        /// <param name="name">名称</param>
        /// <param name="isLockedOut">锁定</param>
        /// <param name="notActive">用户不可用</param>
        /// <param name="emailConfirmed">Email confirmed</param>
        /// <param name="isExternal">External</param>
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
        public async Task<List<IdentityUser>> GetListAsync(
            Guid? roleId, Guid? organizationUnitId,
            string userName,
            string phoneNumber,
            string name,
            bool? isLockedOut,
            bool? notActive,
            bool? emailConfirmed,
            bool? isExternal,
            DateTime? minCreationTime,
            DateTime? maxCreationTime,
            DateTime? minModifitionTime,
            DateTime? maxModifitionTime,
            string sorting = null, int maxResultCount = 50, int skipCount = 0, 
            string filter = null,  CancellationToken cancellationToken = default)
        {
            return await DbContext.Set<IdentityUser>().Include(x => x.Roles).Include(x => x.OrganizationUnits)
                    .WhereIf(roleId.HasValue, x => x.Roles.Any( role => role.RoleId == roleId))
                    .WhereIf(organizationUnitId.HasValue, x=> x.OrganizationUnits.Any( og => og.OrganizationUnitId == organizationUnitId))
                    .WhereIf( !userName.IsNullOrEmpty(), x => x.UserName.Contains(userName))
                    .WhereIf( !phoneNumber.IsNullOrEmpty(), x => x.PhoneNumber.Contains(phoneNumber))
                    .WhereIf(!name.IsNullOrEmpty(), x =>  !x.Name.Contains(name))
                    .WhereIf(isLockedOut.HasValue, x => 
                        (isLockedOut == true && x.LockoutEnd != null &&  x.LockoutEnd >= DateTime.Now) || 
                        (isLockedOut == false && x.LockoutEnd == null || x.LockoutEnd < DateTime.Now))
                    //.WhereIf(notActive.HasValue, x => x.Active == notActive)  用户禁用标识
                    .WhereIf(emailConfirmed.HasValue, x => x.EmailConfirmed == emailConfirmed)
                    .WhereIf(isExternal.HasValue, x => x.IsExternal == isExternal  )
                    .WhereIf(minCreationTime.HasValue, x => x.CreationTime >= minCreationTime)
                    .WhereIf(maxCreationTime.HasValue, x => x.CreationTime <= maxCreationTime)
                    .WhereIf(minModifitionTime.HasValue, x => x.LastModificationTime >= minModifitionTime)
                    .WhereIf(maxModifitionTime.HasValue, x => x.LastModificationTime <= maxModifitionTime)
                    .WhereIf(!filter.IsNullOrEmpty(), x => x.UserName.Contains(filter) || x.PhoneNumber.Contains(filter) || x.Email.Contains(filter) || x.Surname.Contains(filter) || x.Name.Contains(filter))
                    .OrderBy(sorting.IsNullOrEmpty() ? nameof(IdentityUser.CreationTime) : sorting)
                    .PageBy(skipCount, maxResultCount)
                    .ToListAsync(GetCancellationToken(cancellationToken));

        }


        /// <summary>
        /// 通过手机号查询用户信息
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <param name="isConfiremed"></param>
        /// <param name="includeDetails"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<IdentityUser> FindByPhoneNumberAsync(string phoneNumber, bool isConfiremed = true, bool includeDetails = false, CancellationToken cancellationToken = default)
        {
            return await DbContext.Set<IdentityUser>().IncludeDetails(includeDetails)
                .Where(user => user.PhoneNumber == phoneNumber && user.PhoneNumberConfirmed == isConfiremed)
                .FirstOrDefaultAsync(cancellationToken);
        }

        /// <summary>
        /// 通过用户ids 查询用户信息
        /// </summary>
        /// <param name="userIds"></param>
        /// <param name="includeDetails"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<List<IdentityUser>> GetListByIdsAsync(List<Guid> userIds, bool includeDetails = false, CancellationToken cancellationToken = default)
        {
            return await DbContext.Set<IdentityUser>().IncludeDetails(includeDetails)
                .Where(user => userIds.Contains(user.Id))
                .ToListAsync(cancellationToken);
        }

        /// <summary>
        /// 分页查询用户关联的组织列表
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="filter"></param>
        /// <param name="includeDetails"></param>
        /// <param name="skipCount"></param>
        /// <param name="maxResultCount"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<OrganizationUnit>> GetUserOrganizationUnitListAsync(
            Guid userId,
            string filter = null,
            bool includeDetails = false,
            int skipCount = 1,
            int maxResultCount = 10,
            CancellationToken cancellationToken = default)
        {
            var query = from userOU in DbContext.Set<IdentityUserOrganizationUnit>()
                        join user in DbContext.Set<IdentityUser>() on userOU.UserId equals user.Id
                        join ou in DbContext.Set<OrganizationUnit>().IncludeDetails(includeDetails)
                            on userOU.OrganizationUnitId equals ou.Id
                        where userOU.UserId == userId
                        select ou;
            return await query
                .WhereIf(!filter.IsNullOrEmpty(), ou => ou.Code.Contains(filter) || ou.DisplayName.Contains(filter))
                .PageBy(skipCount, maxResultCount)
                .ToListAsync(cancellationToken);
            throw new NotImplementedException();
        }


        #region 通过组织id查询关联的用户
        /// <summary>
        /// 通过组织id查询关联的用户数量
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="filter"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<long> GetUsersInOrganizationUnitCountAsync(
            Guid organizationId,
            string filter = null,
            CancellationToken cancellationToken = default)
        {

            var query = from userOU in DbContext.Set<IdentityUserOrganizationUnit>()
                        join user in DbContext.Set<IdentityUser>() on userOU.UserId equals user.Id
                        join ou in DbContext.Set<OrganizationUnit>()
                            on userOU.OrganizationUnitId equals ou.Id
                        where userOU.OrganizationUnitId == organizationId
                        select user;
            return await query.WhereIf(!string.IsNullOrEmpty(filter),
                            user => user.Name.Contains(filter) || user.PhoneNumber.Contains(filter))
                            .LongCountAsync(cancellationToken);
        }

        /// <summary>
        /// 通过组织id查询关联的用户列表
        /// </summary>
        /// <param name="organizationUnitId"></param>
        /// <param name="filter"></param>
        /// <param name="skipCount"></param>
        /// <param name="maxResultCount"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<IdentityUser>> GetUsersInOrganizationUnitListAsync(
            Guid organizationUnitId,
            string filter = null,
            int skipCount = 1,
            int maxResultCount = 50,
            CancellationToken cancellation = default)
        {
            var query = from userOU in DbContext.Set<IdentityUserOrganizationUnit>()
                        join user in DbContext.Set<IdentityUser>() on userOU.UserId equals user.Id
                        join ou in DbContext.Set<OrganizationUnit>()
                            on userOU.OrganizationUnitId equals ou.Id
                        where userOU.OrganizationUnitId == organizationUnitId
                        select user;
            return await query.WhereIf(!string.IsNullOrEmpty(filter),
                                    user => user.Name.Contains(filter) || user.PhoneNumber.Contains(filter))
                              .OrderBy("")
                              .PageBy(skipCount,maxResultCount)
                              .ToListAsync(GetCancellationToken(cancellation));
        } 
        #endregion


        #region 通过组织编码查询关联的用户

        protected IQueryable<IdentityUser> QueryUsersInOrganizationUnitWithChildren(
            string code,
            string filter = null)
        {
            var query = from userOu in DbContext.Set<IdentityUserOrganizationUnit>()
                        join user in DbContext.Set<IdentityUser>() on userOu.UserId equals user.Id
                        join ou in DbContext.Set<OrganizationUnit>()
                            on userOu.OrganizationUnitId equals ou.Id
                        where ou.Code.StartsWith(code)
                        select user;
            return  query
                .WhereIf(!filter.IsNullOrEmpty(),
                    user => user.Name.Contains(filter) || user.UserName.Contains(filter) ||
                        user.Surname.Contains(filter) || user.Email.Contains(filter) ||
                        user.PhoneNumber.Contains(filter));
        }

        /// <summary>
        /// 通过组织编码查询关联的用户列表
        /// </summary>
        /// <param name="code"></param>
        /// <param name="filter"></param>
        /// <param name="skipCount"></param>
        /// <param name="maxResultCount"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<List<IdentityUser>> GetUsersInOrganizationUnitWithChildrenAsync(
            string code,
            string filter = null,
            int skipCount = 1,
            int maxResultCount = 10,
            CancellationToken cancellationToken = default)
        {   
            var query = QueryUsersInOrganizationUnitWithChildren(code, filter);
            return await query.PageBy(skipCount, maxResultCount)
                .ToListAsync(cancellationToken);
        }

        /// <summary>
        /// 通过组织code查询关联的用户数量
        /// </summary>
        /// <param name="code"></param>
        /// <param name="filter"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<long> GetUsersInOrganizationUnitWithChildrenCountAsync(
            string code,
            string filter = null,
            CancellationToken cancellationToken = default)
        {
            var query = QueryUsersInOrganizationUnitWithChildren(code, filter);
            return await query.LongCountAsync();
        } 
        #endregion

        

        /// <summary>
        /// 用户邮箱是否绑定
        /// </summary>
        /// <param name="normalizedEmail"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<bool> IsEmailConfirmedAsync(string normalizedEmail, CancellationToken cancellationToken = default)
        {
            //ToDo: NormalizedEmail 和 Email区别?
            return await DbContext.Set<IdentityUser>().IncludeDetails(false)
                .AnyAsync( user => user.NormalizedEmail == normalizedEmail && user.EmailConfirmed,
                GetCancellationToken(cancellationToken));
            throw new NotImplementedException();
        }

        /// <summary>
        /// 用户手机号是否绑定
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<bool> IsPhoneNumberConfirmedAsync(string phoneNumber, CancellationToken cancellationToken = default)
        {
            return await DbContext.Set<IdentityUser>().IncludeDetails(false)
                .AnyAsync(user => user.PhoneNumber == phoneNumber && user.PhoneNumberConfirmed,
                    GetCancellationToken(cancellationToken));
        }

        /// <summary>
        /// 用户手机号是否已经使用
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<bool> IsPhoneNumberUsedAsync(string phoneNumber, CancellationToken cancellationToken)
        {
            var query = await DbContext.Set<IdentityUser>().IncludeDetails(false)
                .AnyAsync(user => user.PhoneNumber == phoneNumber, GetCancellationToken(cancellationToken));
            return  query;
        }
    }


}
