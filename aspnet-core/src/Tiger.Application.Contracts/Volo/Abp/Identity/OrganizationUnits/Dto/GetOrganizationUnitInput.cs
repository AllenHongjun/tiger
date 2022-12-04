using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Tiger.Volo.Abp.Identity.OrganizationUnits.Dto
{
    public class GetOrganizationUnitInput : PagedAndSortedResultRequestDto
    {
        public Guid? ParentId { get; set; }
        public string Filter { get; set; }
    }
}
