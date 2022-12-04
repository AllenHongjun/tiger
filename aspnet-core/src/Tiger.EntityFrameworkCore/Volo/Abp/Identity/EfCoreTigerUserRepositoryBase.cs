using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.Users.EntityFrameworkCore;

namespace Tiger.Volo.Abp.Identity
{
    /// <summary>
    /// 用户仓储实现（扩展abp用户仓储）
    /// </summary>
    public class EfCoreTigerIdentityUserRepository : EfCoreIdentityUserRepository, ITigerIdentityUserRepository
    {
        public EfCoreTigerIdentityUserRepository(
            IDbContextProvider<IIdentityDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
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
            return await DbContext.Users.IncludeDetails(includeDetails)
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
        public async Task<List<IdentityUser>> GetListByIdListAsync(List<Guid> userIds, bool includeDetails = false, CancellationToken cancellationToken = default)
        {
            return await DbContext.Users.IncludeDetails(includeDetails)
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
                        join user in DbContext.Users on userOU.UserId equals user.Id
                        join ou in DbContext.OrganizationUnits.IncludeDetails(includeDetails)
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
        public Task<long> GetUsersInOrganizationUnitCountAsync(
            Guid organizationId,
            string filter = null,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
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
        public Task<List<IdentityUser>> GetUsersInOrganizationUnitAsync(
            Guid organizationUnitId,
            string filter = null,
            int skipCount = 1,
            int maxResultCount = 50,
            CancellationToken cancellation = default)
        {
            throw new NotImplementedException();
        } 
        #endregion


        #region 通过组织编码查询关联的用户

        protected IQueryable<IdentityUser> QueryUsersInOrganizationUnitWithChildren(
            string code,
            string filter = null)
        {
            var query = from userOu in DbContext.Set<IdentityUserOrganizationUnit>()
                        join user in DbContext.Users on userOu.UserId equals user.Id
                        join ou in DbContext.OrganizationUnits
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
            return await DbContext.Users.IncludeDetails(false)
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
            return await DbContext.Users.IncludeDetails(false)
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
            var query = await DbContext.Users.IncludeDetails(false)
                .AnyAsync(user => user.PhoneNumber == phoneNumber, GetCancellationToken(cancellationToken));
            return  query;
        }
    }


}
