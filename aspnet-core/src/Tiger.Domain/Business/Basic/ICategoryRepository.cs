using System;
using System.Collections.Generic;
using System.Text;
using Tiger.Basic;
using Volo.Abp.Domain.Repositories;

namespace Tiger.Business.Basic
{
    public interface ICategoryRepository : IRepository<Category, Guid>
    { 

    }
}
