/**
 * 类    名：OrderReturnApply   
 * 作    者：花生了什么树       
 * 创建时间：2021/8/11 13:31:01       
 * 说    明: 
 * 
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Tiger.Basic;
using Tiger.Business.Orders;
using Tiger.Business.Members;
using Volo.Abp.Domain.Entities.Auditing;

namespace Tiger.Orders
{   
    /// <summary>
    /// 退款单明细
    /// </summary>
    public class OrderReturnDetail : FullAuditedAggregateRoot<Guid>
    {

        /// <summary>
        /// 订单编码
        /// </summary>

        public string OrderSn { get; set; }

        /// <summary>
        /// 退款金额
        /// </summary>
        public decimal ReturnAmount { get; set; }

        /// <summary>
        /// 退货人姓名
        /// </summary>
        public string ReturnName { get; set; }

        /// <summary>
        /// 退货人手机号
        /// </summary>
        public string ReturnPhone { get; set; }

        /// <summary>
        /// 申请状态：0->待处理；1->退货中；2->已完成；3->已拒绝
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 处理时间
        /// </summary>
        public DateTime HandleTime { get; set; }

        public string ProductPic { get; set; }

        public string ProductName { get; set; }

        public string ProductAttr { get; set; }

        /// <summary>
        /// 退货数量
        /// </summary>
        public string ProductQuantity { get; set; }

        /// <summary>
        /// 商品单价
        /// </summary>
        public string ProductPrice { get; set; }

        public string ProductRealPrice { get; set; }


        /// <summary>
        /// 退货理由
        /// </summary>
        public string Reason { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 凭证图片，以逗号隔开
        /// </summary>
        public string ProofPics { get; set; }
        /// <summary>
        /// 处理备注
        /// </summary>
        public string HandleNote { get; set; }

        /// <summary>
        /// 处理人
        /// </summary>
        public string HandleMan { get; set; }

        /// <summary>
        /// 收货人
        /// </summary>
        public string ReceiveMan { get; set; }

        /// <summary>
        /// 收货时间
        /// </summary>
        public string ReceiveTime { get; set; }

        /// <summary>
        /// 收货备注
        /// </summary>
        public string ReceiveNote { get; set; }




        //public Guid? OrderId { get; set; }

        //[ForeignKey("OrderId")]
        //public virtual Order Order { get; set; }

        public Guid ReceiveAddressId { get; set; }

        public virtual MemberReceiveAddress MemberReceiveAddress { get; set; }

        public Guid ProductId { get; set; }

        public virtual Product Product { get; set; }

        public Guid MemberId { get; set; }

        public virtual Member Member { get; set; }
    }
}
