/**
 * 类    名：OrderSetting   
 * 作    者：花生了什么树       
 * 创建时间：2021/8/11 13:29:38       
 * 说    明: 
 * 
 */

using System;
using System.Collections.Generic;
using System.Text;

namespace Tiger.Orders
{
    public class OrderSetting
    {   
        /// <summary>
        /// 秒杀订单超时关闭时间
        /// </summary>
        public int FlashOrderOverTime { get; set; }

        /// <summary>
        /// 正常订单超时时间
        /// </summary>
        public int NormalOrderOverTime { get; set; }

        /// <summary>
        /// 自动确认收货时间
        /// </summary>
        public int ComfirmOvertime { get; set; }

        /// <summary>
        /// 自动好评时间
        /// </summary>
        public int CommentOvertime { get; set; }
    }
}
