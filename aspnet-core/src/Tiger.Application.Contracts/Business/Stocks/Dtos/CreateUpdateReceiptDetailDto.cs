using System;
using System.ComponentModel;
using Volo.Abp.Application.Dtos;
using Volo.Abp.MultiTenancy;

namespace Tiger.Business.Stocks.Dtos
{
    [Serializable]
    public class CreateUpdateReceiptDetailDto 
    {   
        public Guid? Id { get; set; }

        public string ReceiptCode { get; set; }

        public string WarehouseCode { get; set; }

        public string ProductSn { get; set; }

        public string ProductName { get; set; }

        public string Batch { get; set; }

        public DateTime ManufactureDate { get; set; }

        public DateTime AgingDate { get; set; }

        public int TotalQty { get; set; }

        public int OpenQty { get; set; }

        public string ProcessStamp { get; set; }

        public string QuantityUm { get; set; }

        //public Warehouse Warehouse { get; set; }

        public Guid ProductId { get; set; }

        //public Guid? TenantId { get; set; }

        //public Product Product { get; set; }

        //public Guid? ReceiptHeaderId { get; set; }

        //public ReceiptHeader ReceiptHeader { get; set; }

    }
}