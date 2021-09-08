using System;
using Volo.Abp.Application.Dtos;

namespace Tiger.Business.Purchases.Dtos
{
    [Serializable]
    public class PurchaseReturnDetailDto : FullAuditedEntityDto<Guid>
    {
        public string ProductSn { get; set; }

        public string Unit { get; set; }

        public decimal PurchasePrice { get; set; }

        public int TotalQty { get; set; }

        public int OpenQty { get; set; }

        public string ProcessStamp { get; set; }

        public string Note { get; set; }


        public Guid ProductId { get; set; }


    }
}