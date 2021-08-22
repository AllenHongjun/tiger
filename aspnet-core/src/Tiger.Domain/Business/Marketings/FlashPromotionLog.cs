/**
 * 类    名：FlashPromotionLog   
 * 作    者：花生了什么树       
 * 创建时间：2021/8/11 13:44:49       
 * 说    明: 
 * 
 */

using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities;

namespace Tiger.Marketing
{
    /// <summary>
    /// 限时购通知记录表
    /// 
    /// 存储用户限时购预约记录, 当有限时购场次未开始时 用户可以预约操作。当场次开始时候，系统会提醒
    /// </summary>
    public class FlashPromotionLog:Entity<Guid>
    {
        public Guid MemberId { get; set; }

        public Guid ProductId { get; set; }

        /// <summary>
        /// 订阅人手机号
        /// </summary>
        public string MemberPhone { get; set; }

        /// <summary>
        /// 订阅产品名称
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// 订阅时间
        /// </summary>
        public DateTime SubscribeTime { get; set; }

        /// <summary>
        /// 发送时间
        /// </summary>
        public DateTime Sendtime { get; set; }
    }
}
