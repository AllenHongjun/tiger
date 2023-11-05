using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tiger.Module.TaskManagement;
using Tiger.Module.Teachings.Dtos;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace Tiger.Module.Teachings
{
    /// <summary>
    /// 课程
    /// </summary>
    [ApiExplorerSettings(GroupName = ApiExplorerConsts.ExamGroupName)]
    [RemoteService(Name = TeachingRemoteServiceConsts.RemoteServiceName)]
    [Area(TeachingRemoteServiceConsts.ModuleName)]
    [Route($"api/{TeachingRemoteServiceConsts.ModuleName}/courses")]
    public class CourseController : AbpController, ICourseAppService
    {
        public CourseController(ICourseAppService courseAppService)
        {
            CourseAppService=courseAppService;
        }

        protected ICourseAppService CourseAppService { get; }

        [HttpPost]
        public Task<CourseDto> CreateAsync(CreateUpdateCourseDto input)
        {
            return CourseAppService.CreateAsync(input);
        }

        [HttpDelete]
        [Route("{id}")]
        public Task DeleteAsync(Guid id)
        {
            return CourseAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("id")]
        public Task<CourseDto> GetAsync(Guid id)
        {
            return CourseAppService.GetAsync(id);
        }

        [HttpGet]
        public Task<PagedResultDto<CourseDto>> GetListAsync(CourseGetListInput input)
        {
            return CourseAppService.GetListAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public Task<CourseDto> UpdateAsync(Guid id, CreateUpdateCourseDto input)
        {
            return CourseAppService.UpdateAsync(id, input);
        }
    }
}
