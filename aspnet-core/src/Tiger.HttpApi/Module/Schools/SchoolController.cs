using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tiger.Module.Teachings.Dtos;
using Tiger.Module.Teachings;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp;
using Tiger.Module.Schools.Dtos;

namespace Tiger.Module.Schools
{
    /// <summary>
    /// 学校
    /// </summary>
    [ApiExplorerSettings(GroupName = ApiExplorerConsts.ExamGroupName)]
    [RemoteService(Name = SchoolRemoteServiceConsts.RemoteServiceName)]
    [Area(SchoolRemoteServiceConsts.ModuleName)]
    [Route($"api/{SchoolRemoteServiceConsts.ModuleName}/schools")]
    public class SchoolController : AbpController, ISchoolAppService
    {
        public SchoolController(ISchoolAppService schoolAppService)
        {
            SchoolAppService=schoolAppService;
        }

        protected ISchoolAppService SchoolAppService { get; }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<SchoolDto> CreateAsync(CreateUpdateSchoolDto input)
        {
            return SchoolAppService.CreateAsync(input);
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
            return SchoolAppService.DeleteAsync(id);
        }

        /// <summary>
        /// 详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("id")]
        public Task<SchoolDto> GetAsync(Guid id)
        {
            return SchoolAppService.GetAsync(id);
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        public Task<PagedResultDto<SchoolDto>> GetListAsync(SchoolGetListInput input)
        {
            return SchoolAppService.GetListAsync(input);
        }
         
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        public Task<SchoolDto> UpdateAsync(Guid id, CreateUpdateSchoolDto input)
        {
            return SchoolAppService.UpdateAsync(id, input);
        }
    }
}
