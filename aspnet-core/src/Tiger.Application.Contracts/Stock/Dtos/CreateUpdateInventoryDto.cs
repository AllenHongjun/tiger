using System;

namespace Tiger.Stock.Dtos
{
    [Serializable]
    public class CreateUpdateInventoryDto
    {
        public string ProductName { get; set; }

        public string ProductSn { get; set; }

        public int OnHandQty { get; set; }

        public int LockedQty { get; set; }

        public int FrozenQty { get; set; }

        public int InventoryStatus { get; set; }

        public string AttributeData { get; set; }

        public int CsQty { get; set; }

        public Guid SkuId { get; set; }

        public Guid WarehouseId { get; set; }


        public Guid ProductId { get; set; }

    }
}