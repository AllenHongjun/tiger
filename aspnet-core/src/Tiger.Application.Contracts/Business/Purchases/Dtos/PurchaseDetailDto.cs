using System;
using Tiger.Basic.Products;
using Volo.Abp.Application.Dtos;

namespace Tiger.Business.Purchases.Dtos
{
    [Serializable]
    public class PurchaseDetailDto : FullAuditedEntityDto<Guid>
    {
        public string ProductSn { get; set; }

        public string Unit { get; set; }

        public int Qty { get; set; }

        public decimal PurchasePrice { get; set; }

        public string Note { get; set; }

        public int TotalQty { get; set; }

        public int OpenQty { get; set; }

        public string ProcessStamp { get; set; }


        public Guid ProductId { get; set; }

        public ProductDto Product { get; set; }


    }
}