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
        /// 货号
        /// </summary>
        public string ProductSn { get; set; }

        /// <summary>
        /// 品牌id
        /// </summary>
        //public int BrandId { get; set; }

        /// <summary>
        /// 产品分类id
        /// </summary>
        public int ProductCategoryId { get; set; }

        /// <summary>
        /// 发布状态
        /// </summary>
        public ProductPublishStatus PublishStatus { get; set; }

        /// <summary>
        /// 新品状态:0->不是新品；1->新品
        /// </summary>
        public int NewStatus { get; set; }

        /// <summary>
        /// 推荐状态；0->不推荐；1->推荐
        /// </summary>
        public int RecommandStatus { get; set; }

        /// <summary>
        /// 审核状态：0->未审核；1->审核通过
        /// </summary>
        public int VerifyStatus { get; set; }
    }
}
