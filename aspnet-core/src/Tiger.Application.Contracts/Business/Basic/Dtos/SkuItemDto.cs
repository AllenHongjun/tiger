using System;
using System.Collections.Generic;
using System.Text;

namespace Tiger.Business.Basic.Dtos
{
    public class SkuItemDto
    {
        public string Sku { get; set; }

        public string Code { get; set; }

        public decimal Price { get; set; }

        public decimal Stock { get; set; }
    }
}
