using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;

namespace Tiger.Volo.Abp.Identity
{
    public interface ITigerOrganizationUnitRepository: IRepository<OrganizationUnit, Guid>
    {
    }
}
