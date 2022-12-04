using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities;

namespace Tiger.Volo.Abp.Identity.OrganizationUnits.Dto
{
    public class OrganizationUnitUpdateDto : OrganizationUnitCreateOrUpdateDtoBase, IHasConcurrencyStamp
    {
        public string ConcurrencyStamp { get; set; }
    }
}
