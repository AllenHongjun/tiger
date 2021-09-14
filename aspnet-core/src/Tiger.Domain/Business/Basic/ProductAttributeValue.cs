/**
 * 类    名：AttributeValue   
 * 作    者：花生了什么树       
 * 创建时间：2021/8/11 13:55:13       
 * 说    明: 
 * 
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Tiger.Basic;
using Volo.Abp.Domain.Entities;

namespace Tiger.Business.Basic
{   
    /// <summary>
    /// 属性值表(熟悉和商品关系表)
    /// 
    /// 如果对应参数是规格切支持手动添加，该表用于存储手动新增的值;如果对应的商品属性是参数，那么该表用于存储参数的值
    /// </summary>
    public class ProductAttributeValue:Entity<Guid>
    {
        public Guid ProductId { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

        public Guid ProductAttributeId { get; set; }

        [ForeignKey("ProductAttributeId")]
        public virtual ProductAttribute ProductAttribute { get; set; }

        /// <summary>
        /// 规格有多个时以逗号隔开
        /// </summary>
        public string Value { get; set; }

        protected ProductAttributeValue()
        {
        }

        public ProductAttributeValue(
            Guid id,
            Guid productId,
            //Product product,
            Guid productAttributeId,
            //ProductAttribute productAttribute,
            string value
        ) : base(id)
        {
            ProductId = productId;
            //Product = product;
            ProductAttributeId = productAttributeId;
            //ProductAttribute = productAttribute;
            Value = value;
        }
    }
}
