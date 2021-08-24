/**
 * 类    名：OrderReturnReason   
 * 作    者：花生了什么树       
 * 创建时间：2021/8/11 13:32:25       
 * 说    明: 
 * 
 */

using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities;

namespace Tiger.Orders
{
    /// <summary>
    /// 退货原因 
    /// </summary>
    public class OrderReturnReason:Entity<Guid>
    {
        /// <summary>
        /// 退货类型描述
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 状态：0->不启用；1->启用
        /// </summary>
        public int Status { get; set; }

        public DateTime DateTime { get; set; }
    }
}
