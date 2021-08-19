/**
 * 类    名：Product   
 * 作    者：花生了什么树       
 * 创建时间：2021/8/11 13:53:14       
 * 说    明: 
 * 
 */

using System;
using System.Collections.Generic;
using System.Text;

using Tiger.Business;

namespace Tiger.Basic
{   
    /// <summary>
    /// 商品表
    /// </summary>
    public class Product
    {
        public int BrandId { get; set; }

        public int ProductCategoryId { get; set; }

        public String Name { get; set; }

        /// <summary>
        /// 图片地址
        /// </summary>
        public string Picture { get; set; }

        /// <summary>
        /// 货号
        /// </summary>
        public string ProductSn { get; set; }

        public int DeleteStatus { get; set; }

        public int Sort { get; set; }

        public decimal Price { get; set; }

        public String SubTitle { get; set; }

        public string Unit { get; set; }

        public string DetailDesc { get; set; }

        public virtual ICollection<Order> Orders { get; set; }




    }
}
