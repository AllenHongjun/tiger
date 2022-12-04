using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Tiger.Volo.Abp.IdentityServer.Grants.Dto
{
    public class GetPersistedGrantInput: PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string SubjectId { get; set; }
    }
}
