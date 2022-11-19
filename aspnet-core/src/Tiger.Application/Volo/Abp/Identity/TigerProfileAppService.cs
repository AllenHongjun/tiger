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
