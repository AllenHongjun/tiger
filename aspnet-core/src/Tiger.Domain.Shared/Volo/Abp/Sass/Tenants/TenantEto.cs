using System;
using System.Collections.Generic;
using System.Text;

namespace Tiger.Volo.Abp.Sass.Tenants
{
    [Serializable]
    public class TenantEto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}
