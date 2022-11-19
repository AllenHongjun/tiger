using System;
using System.Collections.Generic;
using System.Text;
using TigerAdmin.Volo.Abp.Account;
using Volo.Abp;
using Volo.Abp.Account;
using Volo.Abp.Account.Emailing;
using Volo.Abp.AuditLogging;
using Volo.Abp.Identity;

namespace Tiger.Volo.Abp.Account
{
    [RemoteService(false)]
    public class TigerAccountAppService : AccountAppService, ITigerAccountAppService
    {
        protected IAuditLogRepository AuditLogRepository { get; }

        public TigerAccountAppService(
            IdentityUserManager userManager, 
            IIdentityRoleRepository roleRepository, 
            IAccountEmailer accountEmailer, 
            IdentitySecurityLogManager identitySecurityLogManager
            ) : base(userManager, roleRepository, accountEmailer, identitySecurityLogManager)
        {

        }

        public void Test()
        {
            //UserManager.AddLoginAsync();
        }
    }
}
