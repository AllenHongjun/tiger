/**
 * 类    名：Comment   
 * 作    者：花生了什么树       
 * 创建时间：2021/8/11 14:38:22       
 * 说    明: 
 * 
 */

using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;

namespace Tiger.Basic
{
    /// <summary>
    /// 商品评价表
    /// </summary>
    public class Comment: FullAuditedAggregateRoot<Guid>
    {
        public Guid ProductId { get; set; }

        public virtual Product Product { get; set; }

        /// <summary>
        /// 上传图片以逗号隔开
        /// </summary>
        public string Pics { get; set; }

        public int Star { get; set; }

        public int ShowStatus { get; set; }


        public string MemberNickName { get; set; }

        public string MemberIP { get; set; }

        public string MemberIcon { get; set; }

        public string Content { get; set; }

        /// <summary>
        /// 购买时产品属性
        /// </summary>

        public string ProductAttribute { get; set; }

        public int CollectCount { get; set; }

        public int ReadCount { get; set; }

        public int ReplayCount { get; set; }

        
    }
}
