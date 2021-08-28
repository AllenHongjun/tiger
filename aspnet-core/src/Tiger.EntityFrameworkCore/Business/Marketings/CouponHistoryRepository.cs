using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tiger.Business.Marketings;
using Tiger.EntityFrameworkCore;
using Tiger.Marketing;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Tiger.Marketings
{
    public class CouponHistoryRepository :
        EfCoreRepository<TigerDbContext, CouponHistory, Guid>,
        ICouponHistoryRepository
    {
        public CouponHistoryRepository(IDbContextProvider<TigerDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<List<CouponHistory>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string filter = null)
        {
            return await DbSet
                .WhereIf(
                    !filter.IsNullOrWhiteSpace(),
                    couponHistory => couponHistory.OrderSn.Contains(filter)
                 )
                .OrderByDescending(sorting => sorting.CreationTime)
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync();
        }

    }
}
