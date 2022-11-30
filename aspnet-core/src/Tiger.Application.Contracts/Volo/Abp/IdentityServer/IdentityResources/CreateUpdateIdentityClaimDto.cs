using System;
using System.Collections.Generic;
using System.Text;

namespace Tiger.Volo.Abp.IdentityServer.IdentityResources
{
    public class CreateUpdateIdentityClaimDto
    {
        public virtual string Type { get; protected set; }

        public virtual Guid IdentityResourceId { get; set; }
    }
}
