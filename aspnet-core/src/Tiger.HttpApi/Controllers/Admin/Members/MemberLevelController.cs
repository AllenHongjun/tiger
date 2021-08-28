using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Tiger.Business.Members;
using Tiger.Business.Members.Dtos;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Tiger.Controllers.Admin.MemberLevels
{
    /// <summary>
    /// 产品分类
    /// </summary>

    [RemoteService(Name = "MemberLevel")]
    [Area("Basics")]
    [ControllerName("MemberLevel")]
    [Route("api/basic/memberLevel")]
    [ApiExplorerSettings(GroupName = "admin-basic")]
    public class MemberLevelController : TigerController, IMemberLevelAppService
    {
        protected readonly IMemberLevelAppService _memberLevelAppService;

        public MemberLevelController(IMemberLevelAppService memberLevelAppService)
        {
            _memberLevelAppService = memberLevelAppService;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<MemberLevelDto> CreateAsync(CreateUpdateMemberLevelDto input)
        {
            return _memberLevelAppService.CreateAsync(input);
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
            return _memberLevelAppService.DeleteAsync(id);
        }

        /// <summary>
        /// 获取单条
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public Task<MemberLevelDto> GetAsync(Guid id)
        {
            return _memberLevelAppService.GetAsync(id);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("all")]
        public Task<PagedResultDto<MemberLevelDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            return _memberLevelAppService.GetListAsync(input);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        public Task<MemberLevelDto> UpdateAsync(Guid id, CreateUpdateMemberLevelDto input)
        {
            return _memberLevelAppService.UpdateAsync(id, input);
        }


        
    }
}
