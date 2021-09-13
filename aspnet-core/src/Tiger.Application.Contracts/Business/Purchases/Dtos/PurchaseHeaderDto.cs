using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace Tiger.Business.Purchases.Dtos
{
    [Serializable]
    public class PurchaseHeaderDto : FullAuditedEntityDto<Guid>
    {
        public string WarehouseCode { get; set; }

        public string Code { get; set; }

        public decimal PurchasePrice { get; set; }

        public int Status { get; set; }

        public decimal TotalAmount { get; set; }

        public int TotalQty { get; set; }

        public string AuditedBy { get; set; }

        public string CloseBy { get; set; }

        public string PurchaseBy { get; set; }

        public string Note { get; set; }

        public Guid WarehouseId { get; set; }

        public Guid SupplyId { get; set; }

        public string CreateBy { get; set; }

        public ICollection<PurchaseDetailDto> PurchaseDetails { get; set; }
    }
}