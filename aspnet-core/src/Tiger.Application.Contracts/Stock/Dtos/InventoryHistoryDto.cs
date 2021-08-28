using System;
using Volo.Abp.Application.Dtos;

namespace Tiger.Stock.Dtos
{
    [Serializable]
    public class InventoryHistoryDto : FullAuditedEntityDto<Guid>
    {
        public string ProductName { get; set; }

        public int OnHandQty { get; set; }

        public int TransitQty { get; set; }

        public int AllocateQty { get; set; }

        public int LockedQty { get; set; }

        public int FrozenQty { get; set; }

        public int InventoryStatus { get; set; }

        public string AttributeData { get; set; }

        public string Batch { get; set; }

        public DateTime ManufactureDate { get; set; }

        public DateTime ExpirationDate { get; set; }

        public DateTime AgingDate { get; set; }

        public int CsQty { get; set; }

        public Guid WarehouseId { get; set; }


        public Guid ProductId { get; set; }


    }
}