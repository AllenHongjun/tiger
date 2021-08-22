using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tiger.Basic;
using Volo.Abp.Domain.Repositories;

namespace Tiger.Business.Basic
{
    public interface IProductAttributeTpyeRepository
        : IRepository<ProductAttributeType, Guid>
    {
        //Task<List<ProductAttributeTpye>> GetListAsync(
        //    int skipCount,
        //    int maxResultCount,
        //    string sorting,
        //    string filter = null
        //);
    }
}
