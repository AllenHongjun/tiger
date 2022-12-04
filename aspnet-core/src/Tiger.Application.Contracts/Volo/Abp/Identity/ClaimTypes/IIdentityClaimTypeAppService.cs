using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tiger.Volo.Abp.Identity.ClaimTypes.Dto;
using Volo.Abp.Application.Services;

namespace Tiger.Volo.Abp.Identity.ClaimTypes
{   
    /// <summary>
    /// 用户声明类型管理
    /// </summary>
    public interface IIdentityClaimTypeAppService :
        ICrudAppService<
            ClaimTypeDto, 
            Guid, 
            GetIdentityClaimTypesInput, 
            CreateClaimTypeDto, 
            UpdateClaimTypeDto>
    {
        Task<List<ClaimTypeDto>> GetAllListAsync();
    }
}
