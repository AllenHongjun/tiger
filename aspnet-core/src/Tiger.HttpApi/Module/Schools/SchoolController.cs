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

        [HttpPost]
        public Task<SchoolDto> CreateAsync(CreateUpdateSchoolDto input)
        {
            return SchoolAppService.CreateAsync(input);
        }

        [HttpDelete]
        [Route("{id}")]
        public Task DeleteAsync(Guid id)
        {
            return SchoolAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("id")]
        public Task<SchoolDto> GetAsync(Guid id)
        {
            return SchoolAppService.GetAsync(id);
        }

        [HttpGet]
        public Task<PagedResultDto<SchoolDto>> GetListAsync(SchoolGetListInput input)
        {
            return SchoolAppService.GetListAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public Task<SchoolDto> UpdateAsync(Guid id, CreateUpdateSchoolDto input)
        {
            return SchoolAppService.UpdateAsync(id, input);
        }
    }
}
