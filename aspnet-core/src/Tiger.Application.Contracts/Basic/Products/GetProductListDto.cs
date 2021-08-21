using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Tiger.Basic.Products
{
    public class GetProductListDto : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// 查询关键字
        /// </summary>
        public string Filter { get; set; }

        /// <summary>
        /// 品牌id
        /// </summary>
        public int BrandId { get; set; }

        /// <summary>
        /// 产品分类id
        /// </summary>
        public int ProductCategoryId { get; set; }

        /// <summary>
        /// 发布状态
        /// </summary>
        public ProductPublishStatus PublishStatus { get; set; }
    }
}
