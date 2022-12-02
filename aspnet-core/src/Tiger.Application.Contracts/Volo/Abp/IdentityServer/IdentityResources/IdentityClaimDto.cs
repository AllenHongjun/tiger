using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Tiger.Volo.Abp.IdentityServer.IdentityResources
{
    public class IdentityClaimDto:EntityDto
    {
        public virtual string Type { get; protected set; }

        public virtual Guid IdentityResourceId { get; set; }
    }
}
