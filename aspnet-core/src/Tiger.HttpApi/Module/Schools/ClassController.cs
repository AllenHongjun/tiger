using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp;
using Tiger.Module.Schools;
using Tiger.Module.Schools.Dtos;

namespace Tiger.Module.Classes
{
    /// <summary>
    /// 班级
    /// </summary>
    [ApiExplorerSettings(GroupName = ApiExplorerConsts.ExamGroupName)]
    [RemoteService(Name = SchoolRemoteServiceConsts.RemoteServiceName)]
    [Area(SchoolRemoteServiceConsts.ModuleName)]
    [Route($"api/{SchoolRemoteServiceConsts.ModuleName}/classes")]
    public class ClassController : AbpController, IClassInfoAppService
    {
        public ClassController(IClassInfoAppService schoolAppService)
        {
            ClassAppService=schoolAppService;
        }

        protected IClassInfoAppService ClassAppService { get; }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<ClassInfoDto> CreateAsync(CreateUpdateClassInfoDto input)
        {
            return ClassAppService.CreateAsync(input);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        public Task DeleteAsync(Guid id)
        {
            return ClassAppService.DeleteAsync(id);
        }

        /// <summary>
        /// 详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("id")]
        public Task<ClassInfoDto> GetAsync(Guid id)
        {
            return ClassAppService.GetAsync(id);
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        public Task<PagedResultDto<ClassInfoDto>> GetListAsync(ClassInfoGetListInput input)
        {
            return ClassAppService.GetListAsync(input);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        public Task<ClassInfoDto> UpdateAsync(Guid id, CreateUpdateClassInfoDto input)
        {
            return ClassAppService.UpdateAsync(id, input);
        }
    }
}
