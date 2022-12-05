using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Tiger.Volo.Abp.IdentityServer.ApiScopes.Dto
{
    public class GetApiScopeInput:PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
