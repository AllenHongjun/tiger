using System;

namespace Tiger.Business.Stocks.Dtos
{
    [Serializable]
    public class CreateUpdateShipmentDetailDto
    {
        public string ProductName { get; set; }

        public string ProductSn { get; set; }

        public int ShipQty { get; set; }

        public int RequestQty { get; set; }

        public string Batch { get; set; }

        public DateTime AgingDate { get; set; }

        public int InventorySts { get; set; }

        public decimal TotalWeight { get; set; }

        public decimal TotalVolume { get; set; }

        public string ProcessStamp { get; set; }

        public string QuantityUm { get; set; }

        public int CanceledQty { get; set; }


        public Guid ProductId { get; set; }


        public Guid ShipmentId { get; set; }


    }
}