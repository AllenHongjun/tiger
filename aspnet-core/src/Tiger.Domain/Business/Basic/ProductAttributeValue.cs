/**
 * 类    名：AttributeValue   
 * 作    者：花生了什么树       
 * 创建时间：2021/8/11 13:55:13       
 * 说    明: 
 * 
 */

using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities;

namespace Tiger.Basic
{   
    /// <summary>
    /// 属性值表
    /// 
    /// 如果对应参数是规格切支持手动添加，该表用于存储手动新增的值;如果对应的商品属性是参数，那么该表用于存储参数的值
    /// </summary>
    public class ProductAttributeValue:Entity<Guid>
    {
        public Guid ProductId { get; set; }

        public Guid ProductAttributeId { get; set; }

        /// <summary>
        /// 规格有多个时以逗号隔开
        /// </summary>
        public string Value { get; set; }
    }
}
