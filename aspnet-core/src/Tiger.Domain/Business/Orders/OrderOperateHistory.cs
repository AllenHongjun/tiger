/**
 * 类    名：OrderOperateHistory   
 * 作    者：花生了什么树       
 * 创建时间：2021/8/11 13:29:15       
 * 说    明: 
 * 
 */

using System;
using System.Collections.Generic;
using System.Text;
using Tiger.Business;
using Tiger.Business.Orders;
using Volo.Abp.Domain.Entities.Auditing;

namespace Tiger.Orders
{   
    /// <summary>
    /// 订单操作历史记录
    /// </summary>
    public class OrderOperateHistory: FullAuditedAggregateRoot<Guid>
    {

        /// <summary>
        /// 操作人 用户  系统  后台管理员
        /// </summary>
        public string OperateMan { get; set; }

        /// <summary>
        /// '订单状态：0->待付款；1->待发货；2->已发货；3->已完成；4->已关闭；5->无效订单',
        /// </summary>
        public int OrderStatus { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Note { get; set; }

        public int OrderId { get; set; }

        public virtual Order Order { get; set; }
    }
}
