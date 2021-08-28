using System;

namespace Tiger.Stock.Dtos
{
    [Serializable]
    public class CreateUpdateTransferDetailDto
    {
        public string TransferCode { get; set; }

        public string ProductSn { get; set; }

        public string ProductName { get; set; }

        public Guid FromWarehouseId { get; set; }

        public int TotalQty { get; set; }

        public string QuantityUnit { get; set; }

        public int InventorySts { get; set; }

        public int Status { get; set; }

        public string ProcessStamp { get; set; }


        public Guid ProductId { get; set; }


        public Guid TransferId { get; set; }


    }
}