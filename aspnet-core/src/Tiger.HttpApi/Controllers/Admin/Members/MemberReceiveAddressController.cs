using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Tiger.Business.Members;
using Tiger.Business.Members.Dtos;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Tiger.Controllers.Admin.MemberReceiveAddresss
{
    /// <summary>
    /// 会员收货地址
    /// </summary>

    [RemoteService(Name = "MemberReceiveAddress")]
    [Area("Basics")]
    [ControllerName("MemberReceiveAddress")]
    [Route("api/basic/memberReceiveAddress")]
    [ApiExplorerSettings(GroupName = "admin-basic")]
    public class MemberReceiveAddressController : TigerController, IMemberReceiveAddressAppService
    {
        protected readonly IMemberReceiveAddressAppService _memberReceiveAddressAppService;

        public MemberReceiveAddressController(IMemberReceiveAddressAppService memberReceiveAddressAppService)
        {
            _memberReceiveAddressAppService = memberReceiveAddressAppService;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<MemberReceiveAddressDto> CreateAsync(CreateUpdateMemberReceiveAddressDto input)
        {
            return _memberReceiveAddressAppService.CreateAsync(input);
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
            return _memberReceiveAddressAppService.DeleteAsync(id);
        }

        /// <summary>
        /// 获取单条
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public Task<MemberReceiveAddressDto> GetAsync(Guid id)
        {
            return _memberReceiveAddressAppService.GetAsync(id);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("all")]
        public Task<PagedResultDto<MemberReceiveAddressDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            return _memberReceiveAddressAppService.GetListAsync(input);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        public Task<MemberReceiveAddressDto> UpdateAsync(Guid id, CreateUpdateMemberReceiveAddressDto input)
        {
            return _memberReceiveAddressAppService.UpdateAsync(id, input);
        }


        
    }
}
