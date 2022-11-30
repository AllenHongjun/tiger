using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Tiger.Volo.Abp.IdentityServer.Devices
{
    public class CreateUpdateDeviceFlowCodeDto:CreationAuditedEntityDto<Guid>
    {
        public virtual string DeviceCode { get; set; }

        public virtual string UserCode { get; set; }

        public virtual string SubjectId { get; set; }

        public virtual string ClientId { get; set; }

        public virtual DateTime? Expiration { get; set; }

        public virtual string Data { get; set; }
    }
}
