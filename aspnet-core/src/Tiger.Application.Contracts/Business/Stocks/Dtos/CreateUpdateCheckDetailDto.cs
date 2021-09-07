using System;
using Volo.Abp.Application.Dtos;

namespace Tiger.Stock.Dtos
{
    [Serializable]
    public class CreateUpdateCheckDetailDto:EntityDto<Guid>
    {
        public string ProductSn { get; set; }

        public string ProductName { get; set; }

        public int InventorySts { get; set; }

        public int SystemQty { get; set; }

        public int LastCheckQty { get; set; }

        public int CheckedQty { get; set; }

        public DateTime CheckedBy { get; set; }

        public DateTime CheckedAt { get; set; }

        public string ProcessStamp { get; set; }

        public int AdjustQty { get; set; }

        public string Batch { get; set; }

        public Guid WarehouseId { get; set; }


        public Guid ProductId { get; set; }


        public Guid CheckHeaderId { get; set; }


    }
}