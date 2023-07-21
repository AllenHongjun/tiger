using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tiger.Module.System.Platform.Datas;
using Tiger.Module.System.Platform.Layouts;
using Tiger.Module.System.Platform.Menus;

namespace Tiger.Module.System.Platform
{
    public static class PlatformEfCoreQueryableExtensions
    {

        public static IQueryable<Layout> IncludeDetails(this IQueryable<Layout> queryable, bool include = true)
        {
            if (!include)
            {
                return queryable;
            }

            return queryable;
        }

        public static IQueryable<Menu> IncludeDetails(this IQueryable<Menu> queryable, bool include = true)
        {
            if (!include)
            {
                return queryable;
            }

            return queryable;
        }

        //public static IQueryable<Data> IncludeDetails(this IQueryable<Data> queryable, bool include = true)
        //{
        //    if (!include)
        //    {
        //        return queryable;
        //    }

        //    return queryable
        //        //.AsSplitQuery()
        //        .Include(x => x.Items);
        //}
    }
}
