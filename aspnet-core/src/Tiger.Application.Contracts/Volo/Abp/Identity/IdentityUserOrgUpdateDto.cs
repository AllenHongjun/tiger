using System;
using System.Collections.Generic;
using System.Text;

namespace Volo.Abp.Identity
{
    public class IdentityUserOrgUpdateDto : IdentityUserUpdateDto
    {
        /// <summary>
        /// 组织id列表
        /// </summary>
        public List<Guid> OrgIds { get; set; }
    }
}
