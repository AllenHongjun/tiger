using System;
using System.Collections.Generic;
using System.Text;

namespace Tiger.Basic.Products
{
    public class CreateUpdateProductDto
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

        /// <summary>
        /// 发布状态
        /// </summary>
        public ProductPublishStatus PublishStatus { get; set; }

        //public int DeleteStatus { get; set; }

        public int Sort { get; set; }

        public decimal Price { get; set; }

        public String SubTitle { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// 详情描述
        /// </summary>
        public string DetailDesc { get; set; }
    }
}
