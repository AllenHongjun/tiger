/**
 * 类    名：TigerProfileAppService   
 * 作    者：花生了什么树       
 * 创建时间：2021/8/13 9:39:11       
 * 说    明: 
 * 
 */

using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp;
using Volo.Abp.Identity;

namespace Tiger.Volo.Abp.Identity
{
    [RemoteService(false)]
    public class TigerProfileAppService : ProfileAppService
    {
        public TigerProfileAppService(IdentityUserManager userManager) : base(userManager)
        {
        }
    }
}
