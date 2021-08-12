/**
 * 类    名：FlashPromotion   
 * 作    者：hongjy       
 * 创建时间：2021/8/11 13:33:47       
 * 说    明: 
 * 
 */

using System;
using System.Collections.Generic;
using System.Text;

namespace Tiger.Marketing
{
    /// <summary>
    /// 限制购物
    /// </summary>
    public class FlashPromotion
    {

        public string Title { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int Status { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
