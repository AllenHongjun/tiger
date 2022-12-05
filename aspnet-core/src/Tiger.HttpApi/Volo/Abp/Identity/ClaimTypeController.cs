using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using Volo.Abp.Identity;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Tiger.Volo.Abp.Identity.ClaimTypes;
using System.Threading.Tasks;
using Tiger.Volo.Abp.Identity.ClaimTypes.Dto;
using Volo.Abp.Application.Dtos;

namespace Tiger.Volo.Abp.Identity
{   
    /// <summary>
    /// 声明类型
    /// </summary>
    [RemoteService(Name = IdentityRemoteServiceConsts.RemoteServiceName)]
    [Area("identity")]
    [ControllerName("ClaimType")]
    [ApiExplorerSettings(GroupName = "admin")]
    [Route("api/identity/claim-type")]
    public class ClaimTypeController : AbpController, IIdentityClaimTypeAppService
    {   
        protected IIdentityClaimTypeAppService _identityClaimTypeAppService;

        public ClaimTypeController(
            IIdentityClaimTypeAppService identityClaimTypeAppService)
        {
            _identityClaimTypeAppService=identityClaimTypeAppService;
        }

        [HttpPost]
        public async Task<ClaimTypeDto> CreateAsync(CreateClaimTypeDto input)
        {
            return await _identityClaimTypeAppService.CreateAsync(input);
        }

        [HttpDelete]
        public async Task DeleteAsync(Guid id)
        {
             await _identityClaimTypeAppService.DeleteAsync(id);
             return;
        }

        /// <summary>
        /// 获取所有声明类型
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("all")]
        public async Task<List<ClaimTypeDto>> GetAllListAsync()
        {
            return await _identityClaimTypeAppService.GetAllListAsync();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ClaimTypeDto> GetAsync(Guid id)
        {
            return await (_identityClaimTypeAppService.GetAsync(id));
        }

        [HttpGet]
        public async Task<PagedResultDto<ClaimTypeDto>> GetListAsync(GetIdentityClaimTypesInput input)
        {
            return await _identityClaimTypeAppService.GetListAsync(input);
        }

        [HttpPut]
        [Route("id")]
        public async Task<ClaimTypeDto> UpdateAsync(Guid id, UpdateClaimTypeDto input)
        {
            return await _identityClaimTypeAppService.UpdateAsync(id, input);
        }
    }
}
