using System;

namespace Tiger.Stock.Dtos
{
    [Serializable]
    public class CreateUpdateReverseDetailDto
    {
        public string ProductSn { get; set; }

        public string ProductName { get; set; }

        public string WarehouseCode { get; set; }


        public Guid ProductId { get; set; }


        public Guid ReverseOrderId { get; set; }

    }
}