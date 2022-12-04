using Volo.Abp.Application.Dtos;

namespace Tiger.Volo.Abp.Identity.ClaimTypes.Dto
{
    public class GetIdentityClaimTypesInput : PagedAndSortedResultRequestDto
    {   
        /// <summary>
        /// 查询字符
        /// </summary>
        public string Filter { get; set; }

    }
}
