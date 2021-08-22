/**
 * 类    名：Order   
 * 作    者：花生了什么树       
 * 创建时间：2021/8/11 13:28:38       
 * 说    明: 
 * 
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Tiger.Basic;
using Tiger;
using Tiger.Orders;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp;
using Volo.Abp.MultiTenancy;

namespace Tiger.Business
{   
    /// <summary>
    /// 订单表
    /// </summary>
    [Table("Order")]
    public class Order : FullAuditedAggregateRoot<Guid>,ISoftDelete,IMultiTenant
    {
        public Guid? TenantId { get; set; }
        public int MemberId { get; set; }

        public int CouponId { get; set; }

        public string OrderSn { get; set; }

        /// <summary>
        /// 订单总金额
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// 实际支付金额
        /// </summary>
        public decimal PayAmount { get; set; }

        /// <summary>
        /// 运费金额
        /// </summary>
        public decimal FreightAmount { get; set; }

        /// <summary>
        /// 促销优化金额(促销价，满减价， 阶梯价)
        /// </summary>
        public decimal PromotionAmount { get; set; }

        /// <summary>
        /// 积分抵扣金额
        /// </summary>
        public decimal IntegrationAmount { get; set; }

        /// <summary>
        /// 优惠券抵扣金额
        /// </summary>
        public decimal CouponAmount { get; set; }

        /// <summary>
        /// 折扣金额
        /// </summary>
        public decimal DiscountAmount { get; set; }





        /// <summary>
        /// 支付方式：0->未支付；1->支付宝；2->微信
        /// </summary>
        public int PayType { get; set; }

        /// <summary>
        /// 订单来源：0->PC订单；1->app订单
        /// </summary>
        public int SourceType { get; set; }

        /// <summary>
        /// 订单状态：0->待付款；1->待发货；2->已发货；3->已完成；4->已关闭；5->无效订单
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 订单类型：0->正常订单；1->秒杀订单
        /// </summary>
        public int OrderType { get; set; }

        /// <summary>
        /// 物流公司
        /// </summary>
        public string DeliveryCompany { get; set; }

        /// <summary>
        /// 物流单号
        /// </summary>
        public string DeliverySn { get; set; }

        /// <summary>
        /// 自动确认时间
        /// </summary>
        public int AutoConfirmDay { get; set; }

        public int Integration { get; set; }

        public int Growth { get; set; }

        /// <summary>
        /// 活动信息
        /// </summary>
        public int PromotionInfo { get; set; }





        /// <summary>
        /// 发票类型
        /// </summary>
        public int BillType { get; set; }

        /// <summary>
        /// 发票抬头
        /// </summary>
        public string BillHeader { get; set; }

        /// <summary>
        /// 发票内容
        /// </summary>
        public string BillContent { get; set; }
        /// <summary>
        /// 收票人电话
        /// </summary>
        public string BillReceiverPhone { get; set; }
        /// <summary>
        /// 收票人邮箱
        /// </summary>
        public string BillReceiverEmail { get; set; }




        /// <summary>
        /// 收货人姓名
        /// </summary>
        public string ReceiverName { get; set; }
        /// <summary>
        /// 收货人电话
        /// </summary>
        public string ReceiverPhone { get; set; }
        /// <summary>
        /// 收货人邮编
        /// </summary>
        public string ReceiverPostCode { get; set; }
        /// <summary>
        /// 省份/直辖市
        /// </summary>
        public string ReceiverProvince { get; set; }

        public string ReceiverCity { get; set; }

        public string ReceiverRegion { get; set; }

        public string ReceiverDetailAddress { get; set; }




        /// <summary>
        /// 备注
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// '确认收货状态：0->未确认；1->已确认',
        /// </summary>
        public int ConfirmStatus { get; set; }
        /// <summary>
        /// 下单时使用的积分
        /// </summary>
        public int UseIntegration { get; set; }

        /// <summary>
        /// 支付时间
        /// </summary>
        public DateTime PaymentTime { get; set; }
        /// <summary>
        /// 发货时间
        /// </summary>
        public DateTime DeliveryTime { get; set; }
        /// <summary>
        /// 确认收货时间
        /// </summary>
        public DateTime ReceiveTime { get; set; }
        /// <summary>
        /// 评价时间
        /// </summary>
        public DateTime CommentTime { get; set; }


        public virtual ICollection<OrderItem> OrderItems { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        
    }
}
