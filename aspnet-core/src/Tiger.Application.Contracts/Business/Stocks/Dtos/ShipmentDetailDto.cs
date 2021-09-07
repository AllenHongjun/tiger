using System;
using Volo.Abp.Application.Dtos;

namespace Tiger.Business.Stocks.Dtos
{
    [Serializable]
    public class ShipmentDetailDto : FullAuditedEntityDto<Guid>
    {
        public string ProductName { get; set; }

        public string ProductSn { get; set; }

        /// <summary>
        /// 发货数量
        /// </summary>
        public int ShipQty { get; set; }

        /// <summary>
        /// 请求数量
        /// </summary>
        public int RequestQty { get; set; }

        /// <summary>
        /// 批次号
        /// </summary>
        public string Batch { get; set; }

        /// <summary>
        /// 入库日期
        /// </summary>
        public DateTime AgingDate { get; set; }

        /// <summary>
        /// 库存状态
        /// </summary>
        public int InventorySts { get; set; }

        /// <summary>
        /// 总重量
        /// </summary>
        public decimal TotalWeight { get; set; }

        /// <summary>
        /// 总体积
        /// </summary>
        public decimal TotalVolume { get; set; }

        /// <summary>
        /// 处理标记
        /// </summary>
        public string ProcessStamp { get; set; }

        /// <summary>
        /// 数量单位
        /// </summary>
        public string QuantityUm { get; set; }

        /// <summary>
        /// 以取消数量
        /// </summary>
        public int CanceledQty { get; set; }




        public Guid ProductId { get; set; }



        public Guid ShipmentId { get; set; }




        public Guid? TenantId { get; set; }


    }
}