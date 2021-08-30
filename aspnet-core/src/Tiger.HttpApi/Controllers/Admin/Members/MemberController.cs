using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Tiger.Business.Members;
using Tiger.Business.Members.Dtos;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Tiger.Controllers.Admin.Members
{
    /// <summary>
    /// 产品分类
    /// </summary>

    [RemoteService(Name = "Member")]
    [Area("Basics")]
    [ControllerName("Member")]
    [Route("api/basic/member")]
    [ApiExplorerSettings(GroupName = "admin-basic")]
    public class MemberController : TigerController, IMemberAppService
    {
        protected readonly IMemberAppService _memberAppService;

        public MemberController(IMemberAppService memberAppService)
        {
            _memberAppService = memberAppService;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<MemberDto> CreateAsync(CreateUpdateMemberDto input)
        {
            return _memberAppService.CreateAsync(input);
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
            return _memberAppService.DeleteAsync(id);
        }

        /// <summary>
        /// 获取单条
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public Task<MemberDto> GetAsync(Guid id)
        {
            return _memberAppService.GetAsync(id);
        }

        ///// <summary>
        ///// 获取列表
        ///// </summary>
        ///// <param name="input"></param>
        ///// <returns></returns>
        //[HttpGet]
        //[Route("all")]
        //public Task<PagedResultDto<MemberDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        //{
        //    throw new NotImplementedException();
        //}

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        //[Route("all")]
        public Task<PagedResultDto<MemberDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            return _memberAppService.GetListAsync(input);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        public Task<MemberDto> UpdateAsync(Guid id, CreateUpdateMemberDto input)
        {
            return _memberAppService.UpdateAsync(id, input);
        }


        
    }
}
