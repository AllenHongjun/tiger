using System;
using Volo.Abp.Application.Dtos;

namespace Tiger.Business.Stocks.Dtos
{
    [Serializable]
    public class ShipmentHeaderDto : FullAuditedEntityDto<Guid>
    {
        public string WarehouseCode { get; set; }

        public string Code { get; set; }

        public int ShipmentType { get; set; }

        public DateTime ShipDateTime { get; set; }

        public DateTime DeliveryDate { get; set; }

        public string Note { get; set; }

        public int TotalQty { get; set; }

        public int TotalWeight { get; set; }

        public int TotalVolume { get; set; }

    }
}