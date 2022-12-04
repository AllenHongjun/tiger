using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Tiger.Volo.Abp.IdentityServer.IdentityResources.Dto
{
    public class GetIdentityResourceDto: PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
