using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tiger.Module.System.Platform.Datas
{
    public static class DataEfCoreQuerableExtensions
    {
        public static IQueryable<Data> IncludeDetails(
           this IQueryable<Data> queryable,
           bool include = true)
        {
            if (!include)
            {
                return queryable;
            }

            return queryable
                .Include(x => x.Items);
        }
        
    }
}
