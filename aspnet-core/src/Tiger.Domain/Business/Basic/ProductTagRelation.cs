using System;
using System.Collections.Generic;
using System.Text;
using Tiger.Basic;
using Volo.Abp.Domain.Entities;

namespace Tiger.Business.Basic
{   
    /// <summary>
    /// 产品标签关系表
    /// </summary>
    public class ProductTagRelation:Entity<Guid>
    {
        public int ProductId { get; set; }

        public virtual Product Product { get; set; }
        

        public int ProductTagId { get; set; }

        public virtual ProductTag ProductTag { get; set; }


    }
}
