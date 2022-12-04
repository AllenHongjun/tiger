using System;
using System.Collections.Generic;
using System.Text;

namespace Tiger.Volo.Abp.IdentityServer.ApiResources.Dto
{
    public class CreateUpdateApiResourceDto
    {
        public string Name { get; set; }

        public string DisplayName { get; set; }

        public string Description { get; set; }

        public bool Enabled { get; set; }
    }
}
