using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Tiger.Books;
using Tiger.Volo.Abp.Identity;
using Tiger.Volo.Abp.Identity.OrganizationUnits;
using Tiger.Volo.Abp.Identity.OrganizationUnits.Dto;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Caching;
using Volo.Abp.ObjectExtending;

namespace Volo.Abp.Identity
{
    /// <summary>
    /// 组织单元服务
    /// </summary>
    [RemoteService(false)]
    [Authorize(TigerIdentityPermissions.OrganizationUnits.Default)]
    public class OrganizationUnitAppService : IdentityAppServiceBase, IOrganizationUnitAppService
    {
        private readonly IDistributedCache<OrganizationUnitDto> _cache;
        private readonly IDistributedCache<ListResultDto<OrganizationUnitDto>> _cacheList;
        private readonly IDistributedCache<PagedResultDto<OrganizationUnitDto>> _cachePage;

        protected OrganizationUnitManager OrganizationUnitManager { get; }
        protected IdentityUserManager UserManager { get; }
        protected IOrganizationUnitRepository OrganizationUnitRepository { get; }
        protected ITigerIdentityRoleRepository RoleRepository { get; }
        protected ITigerIdentityUserRepository UserRepository { get; }


        protected IIdentityUserAppService UserAppService { get; }
        protected IIdentityRoleAppService RoleAppService { get; }

        public OrganizationUnitAppService(
            OrganizationUnitManager unitManager,
            IOrganizationUnitRepository unitRepository,
            IIdentityUserAppService userAppService,
            IIdentityRoleAppService roleAppService,
            IDistributedCache<OrganizationUnitDto> cache,
            IDistributedCache<ListResultDto<OrganizationUnitDto>> cacheList,
            IDistributedCache<PagedResultDto<OrganizationUnitDto>> cachePage,
            ITigerIdentityRoleRepository roleRepository,
            ITigerIdentityUserRepository userRepository,
            IdentityUserManager userManager)
        {
            OrganizationUnitManager = unitManager;
            OrganizationUnitRepository = unitRepository;
            UserAppService = userAppService;
            RoleAppService = roleAppService;


            _cache = cache;
            _cacheList = cacheList;
            _cachePage = cachePage;
            RoleRepository=roleRepository;
            UserRepository=userRepository;
            UserManager=userManager;
        }

        #region Utility
        /// <summary>
        /// 查询组织的子节点
        /// </summary>
        /// <param name="parentId"></param>
        /// <param name="recursive"></param>
        /// <returns></returns>
        protected virtual async Task<List<OrganizationUnitDto>> GetChildrenAsync(Guid parentId, bool recursive = false)
        {
            var list = ObjectMapper.Map<List<OrganizationUnit>, List<OrganizationUnitDto>>(await OrganizationUnitManager.FindChildrenAsync(parentId, recursive));
            await SetLeaf(list);
            return list;
        }


        /// <summary>
        /// 将列表转换为组织树
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="children"></param>
        /// <returns></returns>
        protected virtual async Task TraverseTreeAsync(OrganizationUnitDto dto, List<OrganizationUnitDto> children)
        {
            if (dto.Children.Count == 0)
            {
                children = await GetChildrenAsync(dto.Id);
                dto.Children.AddRange(children);
            }
            if (children == null || !children.Any())
            {
                await Task.CompletedTask;
                return;
            }
            foreach (var child in children)
            {
                var next = await GetChildrenAsync(child.Id);
                child.Children.AddRange(next);
                await TraverseTreeAsync(dto, child.Children);
            }
        }

        /// <summary>
        /// 设置叶子节点
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        protected virtual async Task SetLeaf(List<OrganizationUnitDto> list)
        {
            foreach (var item in list)
            {
                if ((await OrganizationUnitRepository.GetChildrenAsync(item.Id)).Count == 0)
                {
                    item.SetLeaf();
                }
            }
        }

        /// <summary>
        ///  获取根节点的组织列表
        /// </summary>
        /// <returns></returns>
        protected virtual async Task<ListResultDto<OrganizationUnitDto>> GetRootListAsync()
        {
            return await _cacheList.GetOrAddAsync(
                Guid.NewGuid().ToString(), //Cache key
                async () => {

                    var root = ObjectMapper.Map<List<OrganizationUnit>, List<OrganizationUnitDto>>(await OrganizationUnitRepository.GetChildrenAsync(null,includeDetails:false));
                    await SetLeaf(root);
                    return new PagedResultDto<OrganizationUnitDto>(
                        root.Count,
                        root
                    );
                },
                () => new DistributedCacheEntryOptions
                {
                    AbsoluteExpiration = DateTimeOffset.Now.AddHours(1)
                }
            );
        }

        
        #endregion

        #region CRUD

        /// <summary>
        /// 获取组织列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public virtual async Task<PagedResultDto<OrganizationUnitDto>> GetListAsync(GetOrganizationUnitInput input)
        {
            var count = await OrganizationUnitRepository.GetCountAsync();
            var list = await OrganizationUnitRepository.GetListAsync(input.Sorting, input.MaxResultCount, input.SkipCount, includeDetails:false);
            return new PagedResultDto<OrganizationUnitDto>(
                count,
                ObjectMapper.Map<List<OrganizationUnit>, List<OrganizationUnitDto>>(list)
            );
        }

        /// <summary>
        /// 获取根节点
        /// </summary>
        /// <returns></returns>
        public async virtual Task<ListResultDto<OrganizationUnitDto>> GetRootAsync()
        {
            var rootOrganizationUnits = await OrganizationUnitManager.FindChildrenAsync(null, recursive: false);

            return new ListResultDto<OrganizationUnitDto>(
                ObjectMapper.Map<List<OrganizationUnit>, List<OrganizationUnitDto>>(rootOrganizationUnits));
        }


        /// <summary>
        /// 查询所有组织单元(树结构)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public virtual async Task<ListResultDto<OrganizationUnitDto>> GetAllListAsync(GetAllOrgnizationUnitInput input)
        {
            var root = await GetRootListAsync();
            foreach (var ouDto in root.Items)
            {
                await TraverseTreeAsync(ouDto, ouDto.Children);
            }
            return root;
        }


        /// <summary>
        /// 获取单条组织机构
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<OrganizationUnitDto> GetAsync(Guid id)
        {
            return await _cache.GetOrAddAsync(
               id.ToString(), //Cache key
               async () =>
               {
                   return ObjectMapper.Map<OrganizationUnit, OrganizationUnitDto>(
                       await OrganizationUnitRepository.GetAsync(id)
                   );
               },
               () => new DistributedCacheEntryOptions
               {
                   AbsoluteExpiration = DateTimeOffset.Now.AddHours(1)
               }
           );
        }

        /// <summary>
        /// 获取单条组织明细(树形结构)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<OrganizationUnitDto> GetDetailsAsync(Guid id)
        {
            var ou = await OrganizationUnitRepository.GetAsync(id, includeDetails: true);
            var ouDto = ObjectMapper.Map<OrganizationUnit, OrganizationUnitDto>(ou);
            await TraverseTreeAsync(ouDto, ouDto.Children);
            return ouDto;
        }

        /// <summary>
        /// 查询组织的子节点
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public virtual async Task<ListResultDto<OrganizationUnitDto>> FindChildrenAsync(GetOrganizationUnitChildrenDto input)
        {
            var organizationUnitChildren = await OrganizationUnitManager.FindChildrenAsync(input.Id, input.Recursive);

            return new ListResultDto<OrganizationUnitDto>(
                ObjectMapper.Map<List<OrganizationUnit>, List<OrganizationUnitDto>>(organizationUnitChildren));
        }

        /// <summary>
        /// 获取最后一个子节点
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public virtual async Task<OrganizationUnitDto> GetLastChildOrNullAsync(Guid? parentId)
        {
            var organizationUnitLastChild = await OrganizationUnitManager.GetLastChildOrNullAsync(parentId);

            return ObjectMapper.Map<OrganizationUnit, OrganizationUnitDto>
                (organizationUnitLastChild);
        }



        /// <summary>
        /// 添加组织
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <remarks>
        /// 自动计算要添加的code (有层次结构代码)
        /// </remarks>
        [Authorize(TigerIdentityPermissions.OrganizationUnits.Create)]
        public virtual async Task<OrganizationUnitDto> CreateAsync(OrganizationUnitCreateDto input)
        {
            var ou = new OrganizationUnit(
                GuidGenerator.Create(),
                input.DisplayName,
                input.ParentId,
                CurrentTenant.Id
            );
            input.MapExtraPropertiesTo(ou);

            await OrganizationUnitManager.CreateAsync(ou);
            await CurrentUnitOfWork.SaveChangesAsync();

            return ObjectMapper.Map<OrganizationUnit, OrganizationUnitDto>(ou);
        }

        /// <summary>
        /// 修改组织
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize(TigerIdentityPermissions.OrganizationUnits.Update)]
        public virtual async Task<OrganizationUnitDto> UpdateAsync(Guid id, OrganizationUnitUpdateDto input)
        {
            var ou = await OrganizationUnitRepository.GetAsync(id);
            ou.ConcurrencyStamp = input.ConcurrencyStamp;
            ou.DisplayName = input.DisplayName;

            input.MapExtraPropertiesTo(ou);

            await OrganizationUnitManager.UpdateAsync(ou);
            await CurrentUnitOfWork.SaveChangesAsync();

            return ObjectMapper.Map<OrganizationUnit, OrganizationUnitDto>(ou);
        }


        /// <summary>
        /// 移动组织树节点
        /// </summary>
        /// <param name="id"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        [Authorize(TigerIdentityPermissions.OrganizationUnits.Update)]
        public async Task MoveAsync(Guid id, Guid? parentId)
        {
            var ou = await OrganizationUnitRepository.GetAsync(id);
            if (ou == null)
            {
                return;
            }
            await OrganizationUnitManager.MoveAsync(id, parentId);
        }

        /// <summary>
        /// 删除组织
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <remarks>
        /// 1. 删除所有组织关联的角色
        /// 2. 删除所有的组织子节点
        /// 3. Todo:删除组织关联的用户
        /// </remarks>
        [Authorize(TigerIdentityPermissions.OrganizationUnits.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            var ou = await OrganizationUnitRepository.GetAsync(id, false);
            if (ou == null)
            {
                return;
            }
            await OrganizationUnitManager.DeleteAsync(id);
        }
        #endregion



        #region 组织关联角色
        /// <summary>
        /// 获取组织机构关联的角色
        /// </summary>
        /// <param name="ouId"></param>
        /// <param name="roleInput"></param>
        /// <returns></returns>
        [Authorize(IdentityPermissions.Roles.Default)]
        public virtual async Task<PagedResultDto<IdentityRoleDto>> GetRolesAsync(Guid? ouId, PagedAndSortedResultRequestDto roleInput)
        {
            if (!ouId.HasValue)
            {
                return await RoleAppService.GetListAsync(roleInput);
            }
            IEnumerable<IdentityRole> list = new List<IdentityRole>();
            var ou = await OrganizationUnitRepository.GetAsync(ouId.Value);
            var selfAndChildren = await OrganizationUnitRepository.GetAllChildrenWithParentCodeAsync(ou.Code, ou.Id);
            selfAndChildren.Add(ou);

            // Todo:减少查询数据库次数
            foreach (var child in selfAndChildren)
            {
                list = Enumerable.Union(list, await OrganizationUnitRepository.GetRolesAsync(
                       child,
                       roleInput.Sorting
                ));
            }
            return new PagedResultDto<IdentityRoleDto>(
                list.Count(),
                ObjectMapper.Map<List<IdentityRole>, List<IdentityRoleDto>>(
                    list.Skip(roleInput.SkipCount).Take(roleInput.MaxResultCount)
                    .ToList()
                )
            );
        }

        /// <summary>
        /// 获取未关联组织的角色列表
        /// </summary>
        /// <param name="id">组织机构id</param>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize(TigerIdentityPermissions.OrganizationUnits.ManageRoles)]
        public async virtual Task<PagedResultDto<IdentityRoleDto>> GetUnaddedRolesAsync(Guid id, GetOrganizationUnitInput input)
        {
            var organizationUnit = await OrganizationUnitRepository.GetAsync(id);

            if (organizationUnit == null)
            {
                throw new UserFriendlyException("组织机构不存在");
            }

            var organizationUnitRolesCount = await OrganizationUnitRepository.GetUnaddedRolesCountAsync(organizationUnit, input.Filter);

            var organizationUnitRoles = await OrganizationUnitRepository.GetUnaddedRolesAsync(organizationUnit, input.Sorting, input.MaxResultCount, input.SkipCount, input.Filter);

            return new PagedResultDto<IdentityRoleDto>(organizationUnitRolesCount,
                ObjectMapper.Map<List<IdentityRole>, List<IdentityRoleDto>>(organizationUnitRoles));
        }


        [Authorize(TigerIdentityPermissions.OrganizationUnits.ManageRoles)]
        public async virtual Task<ListResultDto<string>> GetRoleNamesAsync(Guid ouid)
        {
            var roleNames = await UserRepository.GetRoleNamesInOrganizationUnitAsync(ouid);

            return new ListResultDto<string>(roleNames);
        }


        /// <summary>
        /// 组织机构关联多个角色
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize(TigerIdentityPermissions.OrganizationUnits.ManageRoles)]
        public async virtual Task AddRolesAsync(Guid id, OrganizationUnitAddRolesDto input)
        {
            var organizationUnit = await OrganizationUnitRepository.GetAsync(id);

            var roles = await RoleRepository.GetListByIdListAsync(input.RoleIds, includeDetails: true);

            foreach (var role in roles)
            {
                await OrganizationUnitManager.AddRoleToOrganizationUnitAsync(role, organizationUnit);
            }
            // 不调用savechange 就不最终保存数据
            await CurrentUnitOfWork.SaveChangesAsync();
        }

        /// <summary>
        /// 从组织机构中移除角色
        /// </summary>
        /// <param name="id">组织id</param>
        /// <param name="RoleId">角色id</param>
        /// <returns></returns>
        [Authorize(TigerIdentityPermissions.OrganizationUnits.ManageRoles)]
        public async virtual Task RemoveRoleAsync(Guid id, Guid RoleId)
        {
            await OrganizationUnitManager.RemoveRoleFromOrganizationUnitAsync(id, RoleId);

            await CurrentUnitOfWork.SaveChangesAsync();
        }

        #endregion

        #region 组织关联用户

        /// <summary>
        /// 获取组织机构关联的用户
        /// </summary>
        /// <param name="ouId"></param>
        /// <param name="userInput"></param>
        /// <returns></returns>
        [Authorize(IdentityPermissions.Users.Default)]
        public virtual async Task<PagedResultDto<IdentityUserDto>> GetUsersAsync(Guid? ouId, GetIdentityUsersInput userInput)
        {
            if (!ouId.HasValue)
            {
                return await UserAppService.GetListAsync(userInput);
            }
            IEnumerable<IdentityUser> list = new List<IdentityUser>();
            var ou = await OrganizationUnitRepository.GetAsync(ouId.Value);
            var selfAndChildren = await OrganizationUnitRepository.GetAllChildrenWithParentCodeAsync(ou.Code, ou.Id);
            selfAndChildren.Add(ou);
            //Consider submitting PR to get its own overloading method containing all the members of the child node
            foreach (var child in selfAndChildren)
            {
                // Find child nodes where users have duplicates (users can have multiple organizations)
                //count += await UnitRepository.GetMembersCountAsync(child, usersInput.Filter);
                list = Enumerable.Union(list, await OrganizationUnitRepository.GetMembersAsync(
                       child,
                       userInput.Sorting,
                       //usersInput.MaxResultCount, // So let's think about looking up all the members of the subset
                       //usersInput.SkipCount,  
                       filter: userInput.Filter
                ));
            }
            return new PagedResultDto<IdentityUserDto>(
                list.Count(),
                ObjectMapper.Map<List<IdentityUser>, List<IdentityUserDto>>(
                    list.Skip(userInput.SkipCount).Take(userInput.MaxResultCount)
                    .ToList()
                )
            );
        }


        /// <summary>
        /// 获取未关联组织的用户列表
        /// </summary>
        /// <param name="id">组织id</param>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize(TigerIdentityPermissions.OrganizationUnits.ManageUsers)]
        public async virtual Task<PagedResultDto<IdentityUserDto>> GetUnaddedUsersAsync(Guid id, GetOrganizationUnitInput input)
        {   
            var organizationUnit = await OrganizationUnitRepository.GetAsync(id);

            var organizationUnitUsersCount = await OrganizationUnitRepository
                .GetUnaddedUsersCountAsync(organizationUnit, input.Filter);

            var organizationUnitUsers = await OrganizationUnitRepository
                .GetUnaddedUsersAsync(organizationUnit, input.Sorting, input.MaxResultCount,
                input.SkipCount, input.Filter);

            return new PagedResultDto<IdentityUserDto>(organizationUnitUsersCount,
                ObjectMapper.Map<List<IdentityUser>, List<IdentityUserDto>>(organizationUnitUsers));

        }


        /// <summary>
        /// 组织关联多个用户
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize(TigerIdentityPermissions.OrganizationUnits.ManageUsers)]
        public async virtual Task AddUsersAsync(Guid id, OrganizationUnitAddUsersDto input)
        {
            var organizationUnit = await OrganizationUnitRepository.GetAsync(id);

            var users = await UserRepository.GetListByIdListAsync(input.UserIds, includeDetails: true);

            foreach (var user in users)
            {
                await UserManager.AddToOrganizationUnitAsync(user, organizationUnit);
            }

            await CurrentUnitOfWork.SaveChangesAsync();
        }

        /// <summary>
        /// 移除组织关联的用户
        /// </summary>
        /// <param name="ouId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [Authorize(TigerIdentityPermissions.OrganizationUnits.ManageUsers)]
        public async virtual Task RemoveUserAsync(Guid ouId, Guid userId)
        {

            await UserManager.RemoveFromOrganizationUnitAsync(userId, ouId);

            await CurrentUnitOfWork.SaveChangesAsync();
        }

        #endregion











        #region  待重构


        //public virtual async Task<ListResultDto<OrganizationUnitDto>> GetAllListDetailsAsync(GetAllOrgnizationUnitInput input)
        //{
        //    var root = await GetRootListAsync();
        //    foreach (var ouDto in root.Items)
        //    {
        //        await TraverseTreeAsync(ouDto, ouDto.Children);
        //    }
        //    return root;
        //}

        //public virtual async Task<PagedResultDto<OrganizationUnitDto>> GetListDetailsAsync(GetOrganizationUnitInput input)
        //{
        //    var count = await OrganizationUnitRepository.GetCountAsync();
        //    var list = await OrganizationUnitRepository.GetListAsync(input.Sorting, input.MaxResultCount, input.SkipCount);
        //    var listDto = ObjectMapper.Map<List<OrganizationUnit>, List<OrganizationUnitDto>>(list);
        //    foreach (var ouDto in listDto)
        //    {
        //        await TraverseTreeAsync(ouDto, ouDto.Children);
        //    }
        //    return new PagedResultDto<OrganizationUnitDto>(
        //        count,
        //        listDto
        //    );
        //}



        


        /// <summary>
        /// 获取下一个子节点的code
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public Task<string> GetNextChildCodeAsync(Guid? parentId)
        {
            return OrganizationUnitManager.GetNextChildCodeAsync(parentId);
        }


        

        #endregion
    }
}
