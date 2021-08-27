using System;

namespace Tiger.Basic.Dtos
{
    [Serializable]
    public class CreateUpdateSkuDto
    {
        public string SkuCode { get; set; }

        public decimal Price { get; set; }

        public decimal Stock { get; set; }

        public decimal LowStock { get; set; }

        public string SPData { get; set; }

        public int Sale { get; set; }

        public string Image { get; set; }

        public decimal PromotionPrice { get; set; }

        public int LockStock { get; set; }

        public Guid ProductId { get; set; }

        public Product Product { get; set; }
    }
}