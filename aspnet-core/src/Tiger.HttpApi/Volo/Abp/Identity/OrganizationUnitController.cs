using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Tiger.Volo.Abp.Identity.OrganizationUnits;
using Tiger.Volo.Abp.Identity.OrganizationUnits.Dto;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace Volo.Abp.Identity
{
    /// <summary>
    /// 组织架构服务
    /// </summary>
    /// <remarks>
    /// </remarks>
    [RemoteService(Name = IdentityRemoteServiceConsts.RemoteServiceName)]
    [Area("identity")]
    [ControllerName("Organization")]
    [Route("api/identity/organizations")]
    [ApiExplorerSettings(GroupName = "admin")]
    public class OrganizationUnitController : AbpController, IOrganizationUnitAppService
    {
        protected IOrganizationUnitAppService UnitAppService { get; }
        private readonly IWebHostEnvironment _webHostEnvironment;
        public OrganizationUnitController(IOrganizationUnitAppService unitAppService, IWebHostEnvironment webHostEnvironment)
        {
            UnitAppService = unitAppService;
            _webHostEnvironment = webHostEnvironment;
        }

        /// <summary>
        /// 添加组织
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <remarks>
        /// </remarks>
        [HttpPost]
        public virtual Task<OrganizationUnitDto> CreateAsync(OrganizationUnitCreateDto input)
        {
            return UnitAppService.CreateAsync(input);
        }

        /// <summary>
        /// 删除组织
        /// </summary>
        /// <param name="id">Guid</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return UnitAppService.DeleteAsync(id);
        }

        

        /// <summary>
        /// 获取明细
        /// </summary>
        /// <param name="id">Guid</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public virtual Task<OrganizationUnitDto> GetAsync(Guid id)
        {
            return UnitAppService.GetAsync(id);
        }

        /// <summary>
        /// 获取组织列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        public virtual Task<PagedResultDto<OrganizationUnitDto>> GetListAsync(GetOrganizationUnitInput input)
        {
            return UnitAppService.GetListAsync(input);
        }

        /// <summary>
        /// 修改组织
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        public virtual Task<OrganizationUnitDto> UpdateAsync(Guid id, OrganizationUnitUpdateDto input)
        {
            return UnitAppService.UpdateAsync(id, input);
        }


        /// <summary>
        /// 获取组织机构关联的用户
        /// </summary>
        /// <param name="ouId"></param>
        /// <param name="usersInput"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("users")]
        public Task<PagedResultDto<IdentityUserDto>> GetUsersAsync(Guid? ouId, GetIdentityUsersInput usersInput)
        {
            return UnitAppService.GetUsersAsync(ouId, usersInput);
        }

        /// <summary>
        /// 获取组织机构的角色
        /// </summary>
        /// <param name="ouId">组织id</param>
        /// <param name="roleInput">角色</param>
        /// <returns></returns>
        [HttpGet]
        [Route("roles")]
        public Task<PagedResultDto<IdentityRoleDto>> GetRolesAsync(Guid? ouId, PagedAndSortedResultRequestDto roleInput)
        {
            return UnitAppService.GetRolesAsync(ouId, roleInput);
        }

        /// <summary>
        /// 获取所有组织（树结构）
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("all-tree")]
        public virtual Task<ListResultDto<OrganizationUnitDto>> GetAllListAsync(GetAllOrgnizationUnitInput input)
        {
            return UnitAppService.GetAllListAsync(input);
        }


        /// <summary>
        /// 获取组织id查询单个组织(包含子节点的树结构)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}/detail-tree")]
        public Task<OrganizationUnitDto> GetDetailsAsync(Guid id)
        {
            return UnitAppService.GetDetailsAsync(id);
        }





        #region 待重构
        /// <summary>
        /// 获取根组织
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("root")]
        public Task<ListResultDto<OrganizationUnitDto>> GetRootListAsync()
        {
            return UnitAppService.GetRootListAsync();
        }





        /// <summary>
        /// 将组织移动到指定父节点
        /// </summary>
        /// <param name="id"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("move")]
        [RemoteService(false)]
        public Task MoveAsync(Guid id, Guid? parentId)
        {
            return UnitAppService.MoveAsync(id, parentId);
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("details")]
        [RemoteService(false)]
        public Task<PagedResultDto<OrganizationUnitDto>> GetListDetailsAsync(GetOrganizationUnitInput input)
        {
            return UnitAppService.GetListDetailsAsync(input);
        }

        [HttpGet]
        [Route("all/details")]
        [RemoteService(false)]
        public Task<ListResultDto<OrganizationUnitDto>> GetAllListDetailsAsync(GetAllOrgnizationUnitInput input)
        {
            return UnitAppService.GetAllListDetailsAsync(input);
        }

        /// <summary>
        /// 根据父级id获取组织
        /// </summary>
        /// <param name="parentId"></param>
        /// <param name="recursive"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("children/{parentId}")]
        [RemoteService(false)]
        public Task<List<OrganizationUnitDto>> GetChildrenAsync(Guid parentId, bool recursive = false)
        {
            return UnitAppService.GetChildrenAsync(parentId, recursive);
        }



        [HttpGet]
        [Route("next-code")]
        [RemoteService(false)]
        public Task<string> GetNextChildCodeAsync(Guid? parentId)
        {
            return UnitAppService.GetNextChildCodeAsync(parentId);
        } 
        #endregion





        #region 文件上传测试
        /// <summary>
        /// 上传 文件,并返回相对url(不包含 host port wwwroot)
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [Route("upload-file")]
        [HttpPost]
        [RemoteService(false)]
        public async Task<string> UploadFile(IFormFile file)
        {
            if (file == null)
            {
                return null;
            }

            // 网站根目录
            string content_path = _webHostEnvironment.ContentRootPath;
            // wwwroot 静态资源根目录
            string web_path = _webHostEnvironment.WebRootPath;


            string webRootPath = web_path; // wwwroot 文件夹
            string uploadPath = Path.Combine("uploads", DateTime.Now.ToString("yyyyMMdd"));
            string dirPath = Path.Combine(webRootPath, uploadPath);
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }
            string fileExt = Path.GetExtension(file.FileName).Trim('.'); //文件扩展名，不含“.”
            string newFileName = Guid.NewGuid().ToString().Replace("-", "") + "." + fileExt; //随机生成新的文件名
            var filePath = Path.Combine(dirPath, newFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            var url = $@"\{uploadPath}\{newFileName}";
            return url;
        } 
        #endregion


    }
}
