using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Tiger.Business.Stocks
{
    /// <summary>
    /// 出库单头
    /// </summary>
    public class ShipmentHeader : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public Guid? TenantId { get; set; }

        public string WarehouseCode { get; set; }

        /// <summary>
        /// 出库单号
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 出库单类型
        /// </summary>
        public int ShipmentType { get; set; }

        /// <summary>
        /// 发货时间
        /// </summary>
        public DateTime ShipDateTime { get; set; }

        /// <summary>
        /// 到货时间
        /// </summary>
        public DateTime DeliveryDate { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Note { get; set; }


        /// <summary>
        /// 总数量
        /// </summary>
        public int TotalQty { get; set; }

        /// <summary>
        /// 总重量
        /// </summary>
        public int TotalWeight { get; set; }

        /// <summary>
        /// 总体积
        /// </summary>
        public int TotalVolume { get; set; }

        /// <summary>
        /// 出库单明细
        /// </summary>
        public virtual ICollection<ShipmentDetail> ShipmentDetails { get; set; }


    }
}
