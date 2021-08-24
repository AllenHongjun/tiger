/**
 * 类    名：CommentReplay   
 * 作    者：花生了什么树       
 * 创建时间：2021/8/11 14:38:58       
 * 说    明: 
 * 
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;

namespace Tiger.Basic
{
    /// <summary>
    /// 商品评价回复表
    /// </summary>
    public class CommentReply: FullAuditedAggregateRoot<Guid>
    {
        public Guid CommentId { get; set; }

        [ForeignKey("CommentId")]
        public virtual Comment Comment { get; set; }

        public string MemberNickName { get; set; }

        public string MemberIcon { get; set; }

        public string Content { get; set; }

        /// <summary>
        /// 评论人员类型 0->会员 1->管理员
        /// </summary>
        public int Type { get; set; }
    }
}
