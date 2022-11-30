using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Tiger.Volo.Abp.IdentityServer.Grants
{
    public class CreateUpdatePersistedGrantDto:EntityDto<Guid>
    {
        public virtual string Key { get; set; }

        public virtual string Type { get; set; }

        public virtual string SubjectId { get; set; }

        public virtual string ClientId { get; set; }

        public virtual DateTime CreationTime { get; set; }

        public virtual DateTime? Expiration { get; set; }

        public virtual string Data { get; set; }
    }
}
