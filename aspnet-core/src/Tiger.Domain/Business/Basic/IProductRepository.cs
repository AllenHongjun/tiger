using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tiger.Basic;
using Volo.Abp.Domain.Repositories;

namespace Tiger.Business.Basic
{
    public interface IProductRepository : IRepository<Product, Guid>
    {
        Task<List<Product>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string filter = null
        );
    }
}
