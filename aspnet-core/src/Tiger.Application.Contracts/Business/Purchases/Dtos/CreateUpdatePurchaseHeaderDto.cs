using System;
using System.Collections.Generic;

namespace Tiger.Business.Purchases.Dtos
{
    [Serializable]
    public class CreateUpdatePurchaseHeaderDto
    {
        public string WarehouseCode { get; set; }

        public string Code { get; set; }

        public decimal PurchasePrice { get; set; }

        public int Status { get; set; }

        public decimal TotalAmount { get; set; }

        public int TotalQty { get; set; }

        public string AuditedBy { get; set; }

        public string PurchaseBy { get; set; }

        public string Note { get; set; }

        public Guid WarehouseId { get; set; }

        public Guid SupplyId { get; set; }

        public ICollection<CreateUpdatePurchaseDetailDto> PurchaseDetails { get; set; }
    }
}