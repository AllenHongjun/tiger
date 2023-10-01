using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Tiger.Infrastructure.ExportImport;
using Tiger.Infrastructure.ExportImport.Help;
using Tiger.Volo.Abp.Identity.ClaimTypes.Dto;
using Tiger.Volo.Abp.Identity.OrganizationUnits.Dto;
using Tiger.Volo.Abp.Identity.Roles;
using Tiger.Volo.Abp.Identity.Roles.Dto;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Identity;
using IdentityRole = Volo.Abp.Identity.IdentityRole;

namespace Tiger.Volo.Abp.Identity
{

    /// <summary>
    /// 角色服务
    /// </summary>
    [RemoteService(IsEnabled = false)]
    [Dependency(ReplaceServices = true)]
    [ExposeServices(typeof(IIdentityRoleAppService),
        typeof(IdentityRoleAppService),
        typeof(ITigerIdentityRoleAppService),
        typeof(TigerIdentityRoleAppService))]
    [Authorize(IdentityPermissions.Roles.Default)]
    public class TigerIdentityRoleAppService : IdentityRoleAppService, ITigerIdentityRoleAppService
    {
        protected OrganizationUnitManager OrganizationUnitManager { get; }
        protected ITigerIdentityRoleRepository TigerIdentityRoleRepository { get; }
        
        protected IIdentityUserRepository IdentityUserRepository { get; }
        public TigerIdentityRoleAppService(IdentityRoleManager roleManager,
            IIdentityRoleRepository roleRepository,
            OrganizationUnitManager orgManager,
            ITigerIdentityRoleRepository tigerRoleRepository) : base(roleManager, roleRepository)
        {
            OrganizationUnitManager = orgManager;
            TigerIdentityRoleRepository=tigerRoleRepository;
        }
        #region Roles
        [Authorize(IdentityPermissions.Roles.Default)]
        public  async Task<PagedResultDto<IdentityRoleDto>> GetListAsync(GetIdentityRolesInput input)
        {
            var roleCount = await TigerIdentityRoleRepository.GetCountAsync(input.Filter);

            var roles = await TigerIdentityRoleRepository.GetListAsync(
                input.Sorting ?? "Id desc", input.MaxResultCount, input.SkipCount, input.Filter, includeDetails:true);

            var roleList = ObjectMapper.Map<List<IdentityRole>, List<IdentityRoleDto>>(roles);

            #region 查询每个角色的用户数量
            var roleUserCount = await TigerIdentityRoleRepository.GeUserCountOfRoleAsync(roles.Select(x => x.Id).ToList());
            foreach (var role in roleList)
            {
                role.SetUserCount(roleUserCount.FirstOrDefault(userCount => userCount.Key == role.Id).Value);
            } 
            #endregion

            return new PagedResultDto<IdentityRoleDto>(roleCount, roleList);

        }

        /// <summary>
        /// 添加角色同时关联组织
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize(IdentityPermissions.Roles.Create)]
        public virtual async Task<IdentityRoleDto> CreateAsync(IdentityRoleOrgCreateDto input)
        {
            var role = await base.CreateAsync(
                ObjectMapper.Map<IdentityRoleOrgCreateDto, IdentityRoleCreateDto>(input)
            );
            if (input.OrgId.HasValue)
            {
                await OrganizationUnitManager.AddRoleToOrganizationUnitAsync(role.Id, input.OrgId.Value);
            }
            return role;
        }


        /// <summary>
        /// 移动当前角色用户到目标角色
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="targetRoleId"></param>
        /// <param name="cancelAssign"></param>
        /// <returns></returns>
        [Authorize(IdentityPermissions.Roles.Default)]
        public virtual async  Task MoveAllUsers(Guid roleId, Guid? targetRoleId)
        {
            await TigerIdentityRoleRepository.MoveAllUsersAsync(roleId, targetRoleId);
            await CurrentUnitOfWork.SaveChangesAsync();
        }


        


        /// <summary>
        /// 从xlsx导入角色
        /// </summary>
        /// <param name="importexcelfile"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public virtual async Task ImportRoleFromXlsxAsync(IFormFile importexcelfile)
        {

            if (importexcelfile != null && importexcelfile.Length > 0)
            {
                using var workbook = new XLWorkbook(importexcelfile.OpenReadStream());
                // get the first worksheet in the workbook
                var worksheet = workbook.Worksheets.FirstOrDefault();
                if (worksheet == null)
                    throw new Exception("No worksheet found");

                //the columns
                var properties = ImportManager.GetPropertiesByExcelCells<IdentityRoleDto>(worksheet);

                var manager = new PropertyManager<IdentityRoleDto>(properties);
                var iRow = 2;

                while (true)
                {
                    var allColumnsAreEmpty = manager.GetProperties
                        .Select(property => worksheet.Row(iRow).Cell(property.PropertyOrderPosition))
                        .All(cell => cell?.Value == null || string.IsNullOrEmpty(cell.Value.ToString()));

                    if (allColumnsAreEmpty)
                        break;

                    manager.ReadFromXlsx(worksheet, iRow);

                    var role = await TigerIdentityRoleRepository.GetAsync(manager.GetProperty("Id").GuidValue);

                    var isNew = role == null;

                    // TODO: 增加验证 如果错误抛出异常

                    role ??= new IdentityRole(GuidGenerator.Create(), manager.GetProperty("Name").StringValue, CurrentTenant.Id);

                    foreach (var property in manager.GetProperties)
                    {
                        if( property.PropertyName == L["DisplayName:IsDefault"] ) role.IsDefault = property.BooleanValue;
                        if( property.PropertyName == L["DisplayName:IsPublic"] ) role.IsPublic = property.BooleanValue;
                        if( property.PropertyName == L["RoleName"] ) role.ChangeName(property.StringValue);
                    }

                    if (isNew)
                        await RoleManager.CreateAsync(role);
                    else
                        await RoleManager.UpdateAsync(role);

                    iRow++;
                }

                // TODO:发送异步导入成功通知

            }
            else
            {
                throw new Exception("文件不能空");
            }
        }

        /// <summary>
        /// Export Roles to XLSX
        /// </summary>
        /// <param name="GetIdentityRolesInput">input</param>
        /// <returns>查询到的所有角色</returns>
        public virtual async Task<IActionResult> ExportRolesToXlsxAsync(GetIdentityRolesInput input)
        {
            var roles = await TigerIdentityRoleRepository.GetListAsync(
                input.Sorting ?? "Id desc", input.MaxResultCount, input.SkipCount, input.Filter, includeDetails: true);

            //property manager 
            var manager = new PropertyManager<IdentityRole>(new[]
            {
                new PropertyByName<IdentityRole>("Id", p => p.Id),
                new PropertyByName<IdentityRole>(L["RoleName"], p => p.Name),
                new PropertyByName<IdentityRole>(L["DisplayName:IsDefault"], p => p.IsDefault),
                new PropertyByName<IdentityRole>(L["DisplayName:IsPublic"], p => p.IsPublic),
            });

            var bytes = await manager.ExportToXlsxAsync(roles);

            return new FileContentResult(bytes, MimeTypes.TextXlsx);
        }
        #endregion

        #region OrganizationUnit

        /// <summary>
        /// 角色关联组织(一个角色之关联一个组织)
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="ouId"></param>
        /// <returns></returns>
        [Authorize(TigerIdentityPermissions.Roles.ManageOrganizationUnits)]
        public Task AddToOrganizationUnitAsync(Guid roleId, Guid ouId)
        {
            return OrganizationUnitManager.AddRoleToOrganizationUnitAsync(roleId, ouId);
        }


        [Authorize(TigerIdentityPermissions.Roles.ManageOrganizationUnits)]
        public async virtual Task<ListResultDto<OrganizationUnitDto>> GetOrganizationUnitsAsync(Guid id)
        {
            var origanizationUnits = await TigerIdentityRoleRepository.GetOrganizationUnitListAsync(id);

            return new ListResultDto<OrganizationUnitDto>(
                ObjectMapper.Map<List<OrganizationUnit>, List<OrganizationUnitDto>>(origanizationUnits));
        }

        [Authorize(TigerIdentityPermissions.Roles.ManageOrganizationUnits)]
        public async virtual Task SetOrganizationUnitsAsync(Guid id, IdentityRoleAddOrRemoveOrganizationUnitDto input)
        {
            var origanizationUnits = await TigerIdentityRoleRepository.GetOrganizationUnitListAsync(id, true);

            var notInRoleOuIds = input.OrganizationUnitIds.Where(ouid => !origanizationUnits.Any(ou => ou.Id.Equals(ouid)));

            foreach (var ouId in notInRoleOuIds)
            {
                await OrganizationUnitManager.AddRoleToOrganizationUnitAsync(id, ouId);
            }

            var removeRoleOriganzationUnits = origanizationUnits.Where(ou => !input.OrganizationUnitIds.Contains(ou.Id));
            foreach (var origanzationUnit in removeRoleOriganzationUnits)
            {
                origanzationUnit.RemoveRole(id);
            }

            await CurrentUnitOfWork.SaveChangesAsync();
        }

        [Authorize(TigerIdentityPermissions.Roles.ManageOrganizationUnits)]
        public async virtual Task RemoveOrganizationUnitsAsync(Guid id, Guid ouId)
        {
            await OrganizationUnitManager.RemoveRoleFromOrganizationUnitAsync(id, ouId);

            await CurrentUnitOfWork.SaveChangesAsync();
        }

        #endregion

        #region ClaimType


        /// <summary>
        /// 获取角色关联的声明
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async virtual Task<ListResultDto<IdentityRoleClaimDto>> GetClaimsAsync(Guid id)
        {
            var role = await RoleRepository.GetAsync(id);

            return new ListResultDto<IdentityRoleClaimDto>(ObjectMapper.Map<ICollection<IdentityRoleClaim>, List<IdentityRoleClaimDto>>(role.Claims));
        }

        /// <summary>
        /// 添加角色声明
        /// </summary>
        /// <param name="id">角色id</param>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="UserFriendlyException"></exception>
        [Authorize(TigerIdentityPermissions.Roles.ManageClaims)]
        public async virtual Task AddClaimAsync(Guid id, IdentityRoleClaimCreateDto input)
        {
            var role = await RoleRepository.GetAsync(id);
            var claim = new Claim(input.ClaimType, input.ClaimValue);
            if (role.FindClaim(claim) != null)
            {
                throw new UserFriendlyException(L["RoleClaimAlreadyExists"]);
            }

            role.AddClaim(GuidGenerator, claim);
            await RoleRepository.UpdateAsync(role);

            await CurrentUnitOfWork.SaveChangesAsync();
        }

        /// <summary>
        /// 更新角色声明
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize(TigerIdentityPermissions.Roles.ManageClaims)]
        public async virtual Task UpdateClaimAsync(Guid id, IdentityRoleClaimUpdateDto input)
        {
            var role = await RoleRepository.GetAsync(id);
            var oldClaim = role.FindClaim(new Claim(input.ClaimType, input.ClaimValue));
            if (oldClaim != null)
            {
                role.RemoveClaim(oldClaim.ToClaim());
                role.AddClaim(GuidGenerator, new Claim(input.ClaimType, input.NewClaimValue));

                await RoleRepository.UpdateAsync(role);

                await CurrentUnitOfWork.SaveChangesAsync();
            }
        }

        /// <summary>
        /// 删除角色声明
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize(TigerIdentityPermissions.Roles.ManageClaims)]
        public async virtual Task DeleteClaimAsync(Guid id, IdentityRoleClaimDeleteDto input)
        {
            var role = await RoleRepository.GetAsync(id);
            role.RemoveClaim(new Claim(input.ClaimType, input.ClaimValue));

            await RoleRepository.UpdateAsync(role);

            await CurrentUnitOfWork.SaveChangesAsync();
        }

        #endregion
    }
}
