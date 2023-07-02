using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tiger.Volo.Abp.Sass.Tenants;

namespace Tiger.Volo.Abp.Sass
{
    public static class SaasEfCoreQueryableExtensions
    {   
        /// <summary>
        /// 包含明细扩展方法
        /// </summary>
        /// <param name="queryable"></param>
        /// <param name="include"></param>
        /// <returns></returns>
        public static IQueryable<Tenant> IncludeDetails(this IQueryable<Tenant> queryable, bool include = true)
        {
            if (!include)
            {
                return queryable;
            }

            return queryable
                .Include(x => x.ConnectionStrings);
        }
    }
}
