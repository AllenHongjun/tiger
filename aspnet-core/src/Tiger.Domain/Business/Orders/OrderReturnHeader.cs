using System;
using System.Collections.Generic;
using System.Text;
using Tiger.Orders;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Tiger.Business.Orders
{
    /// <summary>
    /// 销售退款单
    /// </summary>
    public class OrderReturnHeader : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {

        /// <summary>
        /// 单号
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 类型 0->退货退款 1->退货不退款 2->退款不退货 3->退差
        /// </summary>
        public int OrderReturnType { get; set; }

        /// <summary>
        /// 状态 0->已申请; 1->已审核; 2-> 已退款; 3->已关闭
        /// </summary>
        public int Status { get; set; }



        /// <summary>
        /// 完成时间
        /// </summary>
        public DateTime CompleteTime { get; set; }

        /// <summary>
        /// 关闭时间
        /// </summary>
        public DateTime CloseAt { get; set; }

        /// <summary>
        /// 总数量
        /// </summary>
        public int TotalQty { get; set; }

        /// <summary>
        /// 总重量
        /// </summary>
        public decimal TotalWeight { get; set; }

        /// <summary>
        /// 总体积
        /// </summary>
        public decimal TotalVolume { get; set; }

        /// <summary>
        /// 总箱数
        /// </summary>
        public int TotalCases { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Note { get; set; }

        public Guid? StoreId { get; set; }

        public Guid WarehouseId { get; set; }

        public Guid? TenantId { get; set; }

        public virtual ICollection<OrderReturnDetail> OrderReturnDetails { get; set; }


        protected OrderReturnHeader()
        {
        }

        public OrderReturnHeader(
            Guid id,
            string code,
            int orderReturnType,
            int status,
            DateTime completeTime,
            DateTime closeAt,
            int totalQty,
            decimal totalWeight,
            decimal totalVolume,
            int totalCases,
            string note,
            Guid? storeId,
            Guid warehouseId,
            Guid? tenantId,
            ICollection<OrderReturnDetail> orderReturnDetails
        ) : base(id)
        {
            Code = code;
            OrderReturnType = orderReturnType;
            Status = status;
            CompleteTime = completeTime;
            CloseAt = closeAt;
            TotalQty = totalQty;
            TotalWeight = totalWeight;
            TotalVolume = totalVolume;
            TotalCases = totalCases;
            Note = note;
            StoreId = storeId;
            WarehouseId = warehouseId;
            TenantId = tenantId;
            OrderReturnDetails = orderReturnDetails;
        }
    }
}
