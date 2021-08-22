using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Tiger.Basic.Products
{
    public class CreateUpdateProductDto
    {
        public Guid ProductCategoryId { get; set; }

        //public virtual Category Category { get; set; }
        /// <summary>
        /// 商品属性类型id
        /// </summary>
        public Guid ProductAttributeTypeId { get; set; }

        public String Name { get; set; }

        /// <summary>
        /// 货号
        /// </summary>
        public string ProductSn { get; set; }

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


        public int Sort { get; set; }

        /// <summary>
        /// 销量
        /// </summary>
        public int Sale { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// 图片地址
        /// </summary>
        public string Picture { get; set; }

        /// <summary>
        /// 相册图片
        /// </summary>
        public string AlbumPics { get; set; }

        public string DetailTitle { get; set; }

        public String SubTitle { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public string Unit { get; set; }

        public string Keywords { get; set; }

        /// <summary>
        /// 详情描述
        /// </summary>
        public string DetailDesc { get; set; }




        /// <summary>
        /// 销售价
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 原价
        /// </summary>
        public decimal OriPrice { get; set; }

        /// <summary>
        /// 采购价
        /// </summary>
        public decimal PurchasePrice { get; set; }

        /// <summary>
        /// 促销价
        /// </summary>
        public decimal PromotionPrice { get; set; }





        /// <summary>
        /// 赠送成长值
        /// </summary>
        public int GiftGrowth { get; set; }

        /// <summary>
        /// 赠送积分
        /// </summary>
        public int GiftIntegration { get; set; }





        /// <summary>
        /// 库存
        /// </summary>
        public int Stock { get; set; }

        /// <summary>
        /// 预警库存
        /// </summary>
        public int LowStock { get; set; }

        /// <summary>
        /// 重量
        /// </summary>
        public decimal Weight { get; set; }

        /// <summary>
        /// 是否为预告商品
        /// </summary>
        public int PreviewStatus { get; set; }



        /// <summary>
        /// 促销类型：0->没有促销使用原价;1->使用促销价；2->使用会员价；3->使用阶梯价格；4->使用满减价格；5->限时购
        /// </summary>
        public int PromotionType { get; set; }

        /// <summary>
        /// 促销活动开始时间
        /// </summary>

        public DateTime PromotionStartTime { get; set; }

        public DateTime PromotionEndTime { get; set; }

        /// <summary>
        /// 活动限购数量
        /// </summary>
        public int PromotionPerLimit { get; set; }

        /// <summary>
        /// 分类名字
        /// </summary>
        public string CategoryName { get; set; }
    }
}
