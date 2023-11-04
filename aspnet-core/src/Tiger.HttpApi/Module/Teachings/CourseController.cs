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
    [RemoteService(Name = "course")]
    [Area("course")]
    [Route($"api/training/courses")]
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
        [Route("list")]
        public Task<PagedResultDto<CourseDto>> GetListAsync(CourseGetListInput input)
        {
            return CourseAppService.GetListAsync(input);
        }

        [HttpPut]
        public Task<CourseDto> UpdateAsync(Guid id, CreateUpdateCourseDto input)
        {
            return CourseAppService.UpdateAsync(id, input);
        }
    }
}
