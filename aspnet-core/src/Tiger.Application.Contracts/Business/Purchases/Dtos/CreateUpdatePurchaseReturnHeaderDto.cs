using System;
using System.Collections.Generic;

namespace Tiger.Business.Purchases.Dtos
{
    [Serializable]
    public class CreateUpdatePurchaseReturnHeaderDto
    {
        public string Code { get; set; }

        public int OrderReturnType { get; set; }

        public int Status { get; set; }

        public DateTime CompleteTime { get; set; }

        public DateTime CloseAt { get; set; }

        public int TotalQty { get; set; }

        public decimal TotalWeight { get; set; }

        public decimal TotalVolume { get; set; }

        public int TotalCases { get; set; }

        public string Note { get; set; }

        public Guid? StoreId { get; set; }

        public Guid WarehouseId { get; set; }

        public ICollection<CreateUpdatePurchaseReturnDetailDto> PurchaseReturnDetails { get; set; }
    }
}