using System;
using System.Collections.Generic;
using System.Text;
using Tiger.Marketing;
using Volo.Abp.Domain.Repositories;

namespace Tiger.Business.Marketings
{
    public interface ICounponRepository : IRepository<Coupon, Guid>
    {
    }
}
