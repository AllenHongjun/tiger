using System;
using System.Collections.Generic;
using System.Text;

namespace Tiger.Business.Basic.Dtos
{
    public class ProductAttributeResultDto
    {   
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string[] Item { get; set; }
    }
}
