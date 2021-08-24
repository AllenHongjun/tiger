using System;
using System.Collections.Generic;
using System.Text;
using Tiger.Basic;
using Volo.Abp.Domain.Entities;

namespace Tiger.Business.Basic
{
    public class ProductTag:Entity<Guid>
    {
        public string Name { get; set; }

        public string Color { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
