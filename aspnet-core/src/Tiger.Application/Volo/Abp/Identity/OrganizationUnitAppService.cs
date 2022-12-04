using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Tiger.Books;
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
    [Authorize(TigerIdentityPermissions.OrganitaionUnits.Default)]
    public class OrganizationUnitAppService : IdentityAppServiceBase, IOrganizationUnitAppService
    {
        private readonly IDistributedCache<OrganizationUnitDto> _cache;
        private readonly IDistributedCache<ListResultDto<OrganizationUnitDto>> _cacheList;
        private readonly IDistributedCache<PagedResultDto<OrganizationUnitDto>> _cachePage;

        protected OrganizationUnitManager UnitManager { get; }
        protected IOrganizationUnitRepository UnitRepository { get; }
        protected IIdentityUserAppService UserAppService { get; }
        protected IIdentityRoleAppService RoleAppService { get; }

        public OrganizationUnitAppService(
            OrganizationUnitManager unitManager,
            IOrganizationUnitRepository unitRepository,
            IIdentityUserAppService userAppService,
            IIdentityRoleAppService roleAppService,
            IDistributedCache<OrganizationUnitDto> cache,
            IDistributedCache<ListResultDto<OrganizationUnitDto>> cacheList,
            IDistributedCache<PagedResultDto<OrganizationUnitDto>> cachePage
            )
        {
            UnitManager = unitManager;
            UnitRepository = unitRepository;
            UserAppService = userAppService;
            RoleAppService = roleAppService;


            _cache = cache;
            _cacheList = cacheList;
            _cachePage = cachePage;
        }

        #region Utility
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
                if ((await UnitRepository.GetChildrenAsync(item.Id)).Count == 0)
                {
                    item.SetLeaf();
                }
            }
        }

        /// <summary>
        ///  获取根节点的组织列表
        /// </summary>
        /// <returns></returns>
        public virtual async Task<ListResultDto<OrganizationUnitDto>> GetRootListAsync()
        {
            return await _cacheList.GetOrAddAsync(
                Guid.NewGuid().ToString(), //Cache key
                async () => {

                    var root = ObjectMapper.Map<List<OrganizationUnit>, List<OrganizationUnitDto>>(await UnitRepository.GetChildrenAsync(null));
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

        #region Basic

        /// <summary>
        /// 获取组织列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public virtual async Task<PagedResultDto<OrganizationUnitDto>> GetListAsync(GetOrganizationUnitInput input)
        {
            var count = await UnitRepository.GetCountAsync();
            var list = await UnitRepository.GetListAsync(input.Sorting, input.MaxResultCount, input.SkipCount, includeDetails:false);
            return new PagedResultDto<OrganizationUnitDto>(
                count,
                ObjectMapper.Map<List<OrganizationUnit>, List<OrganizationUnitDto>>(list)
            );
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
                       await UnitRepository.GetAsync(id)
                   );
               },
               () => new DistributedCacheEntryOptions
               {
                   AbsoluteExpiration = DateTimeOffset.Now.AddHours(1)
               }
           );
        }
        


        /// <summary>
        /// 添加组织
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <remarks>
        /// 自动计算要添加的code (有层次结构代码)
        /// </remarks>
        [Authorize(TigerIdentityPermissions.OrganitaionUnits.Create)]
        public virtual async Task<OrganizationUnitDto> CreateAsync(OrganizationUnitCreateDto input)
        {
            var ou = new OrganizationUnit(
                GuidGenerator.Create(),
                input.DisplayName,
                input.ParentId,
                CurrentTenant.Id
            );
            input.MapExtraPropertiesTo(ou);

            await UnitManager.CreateAsync(ou);
            await CurrentUnitOfWork.SaveChangesAsync();

            return ObjectMapper.Map<OrganizationUnit, OrganizationUnitDto>(ou);
        }

        /// <summary>
        /// 修改组织
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize(TigerIdentityPermissions.OrganitaionUnits.Update)]
        public virtual async Task<OrganizationUnitDto> UpdateAsync(Guid id, OrganizationUnitUpdateDto input)
        {
            var ou = await UnitRepository.GetAsync(id);
            ou.ConcurrencyStamp = input.ConcurrencyStamp;
            ou.DisplayName = input.DisplayName;

            input.MapExtraPropertiesTo(ou);

            await UnitManager.UpdateAsync(ou);
            await CurrentUnitOfWork.SaveChangesAsync();

            return ObjectMapper.Map<OrganizationUnit, OrganizationUnitDto>(ou);
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
        [Authorize(TigerIdentityPermissions.OrganitaionUnits.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            var ou = await UnitRepository.GetAsync(id, false);
            if (ou == null)
            {
                return;
            }
            await UnitManager.DeleteAsync(id);
        }
        #endregion



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
        /// 获取单条组织明细(树形结构)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<OrganizationUnitDto> GetDetailsAsync(Guid id)
        {
            var ou = await UnitRepository.GetAsync(id, includeDetails: true);
            var ouDto = ObjectMapper.Map<OrganizationUnit, OrganizationUnitDto>(ou);
            await TraverseTreeAsync(ouDto, ouDto.Children);
            return ouDto;
        }





        #region  待重构


        public virtual async Task<ListResultDto<OrganizationUnitDto>> GetAllListDetailsAsync(GetAllOrgnizationUnitInput input)
        {
            var root = await GetRootListAsync();
            foreach (var ouDto in root.Items)
            {
                await TraverseTreeAsync(ouDto, ouDto.Children);
            }
            return root;
        }

        public virtual async Task<PagedResultDto<OrganizationUnitDto>> GetListDetailsAsync(GetOrganizationUnitInput input)
        {
            var count = await UnitRepository.GetCountAsync();
            var list = await UnitRepository.GetListAsync(input.Sorting, input.MaxResultCount, input.SkipCount);
            var listDto = ObjectMapper.Map<List<OrganizationUnit>, List<OrganizationUnitDto>>(list);
            foreach (var ouDto in listDto)
            {
                await TraverseTreeAsync(ouDto, ouDto.Children);
            }
            return new PagedResultDto<OrganizationUnitDto>(
                count,
                listDto
            );
        }



        /// <summary>
        /// 查询组织的子节点
        /// </summary>
        /// <param name="parentId"></param>
        /// <param name="recursive"></param>
        /// <returns></returns>
        public virtual async Task<List<OrganizationUnitDto>> GetChildrenAsync(Guid parentId, bool recursive = false)
        {
            var list = ObjectMapper.Map<List<OrganizationUnit>, List<OrganizationUnitDto>>(await UnitManager.FindChildrenAsync(parentId, recursive));
            await SetLeaf(list);
            return list;
        }




        /// <summary>
        /// 获取下一个子节点的code
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public Task<string> GetNextChildCodeAsync(Guid? parentId)
        {
            return UnitManager.GetNextChildCodeAsync(parentId);
        }


        /// <summary>
        /// 移动组织树节点
        /// </summary>
        /// <param name="id"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public async Task MoveAsync(Guid id, Guid? parentId)
        {
            var ou = await UnitRepository.GetAsync(id);
            if (ou == null)
            {
                return;
            }
            await UnitManager.MoveAsync(id, parentId);
        }

        #endregion





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
            var ou = await UnitRepository.GetAsync(ouId.Value);
            var selfAndChildren = await UnitRepository.GetAllChildrenWithParentCodeAsync(ou.Code, ou.Id);
            selfAndChildren.Add(ou);
            //Consider submitting PR to get its own overloading method containing all the members of the child node
            foreach (var child in selfAndChildren)
            {
                // Find child nodes where users have duplicates (users can have multiple organizations)
                //count += await UnitRepository.GetMembersCountAsync(child, usersInput.Filter);
                list = Enumerable.Union(list, await UnitRepository.GetMembersAsync(
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
            var ou = await UnitRepository.GetAsync(ouId.Value);
            var selfAndChildren = await UnitRepository.GetAllChildrenWithParentCodeAsync(ou.Code, ou.Id);
            selfAndChildren.Add(ou);

            // Todo:减少查询数据库次数
            foreach (var child in selfAndChildren)
            {
                list = Enumerable.Union(list, await UnitRepository.GetRolesAsync(
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




    }
}
