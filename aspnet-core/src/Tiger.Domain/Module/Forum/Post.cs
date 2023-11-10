using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Tiger.Module.Forum
{
    /// <summary>
    /// 帖子
    /// </summary>
    public class Post : FullAuditedEntity<Guid>, IMultiTenant
    {
        public Guid? TenantId { get; set; }

        /// <summary>
        /// 班级Id
        /// </summary>
        public Guid ClassId { get; set; }

        /// <summary>
        /// 学校Id
        /// </summary>
        public Guid SchoolId { get; set; }

        /// <summary>
        /// 版块Id
        /// </summary>
        public Guid SectionId { get; set; }

        /// <summary>
        /// 回复帖子Id
        /// </summary>
        public int ReplyPostId { get; set; }
        

        /// <summary>
        /// 帖子标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 帖子内容
        /// </summary>
        public string Contents { get; set; }

        /// <summary>
        /// 总点赞数量
        /// </summary>
        public int Likes { get; set; }

        /// <summary>
        /// 浏览次数
        /// </summary>
        public int PageView { get; set; }

        /// <summary>
        /// 是否置顶
        /// </summary>
        public bool IsTop { get; set; }

        /// <summary>
        /// 是否公开 true公开 false仅自己可见
        /// </summary>
        public bool IsOpen { get; set; }

        /// <summary>
        /// 回复时间
        /// </summary>
        public DateTime? ReplyDate { get; set; }

        /// <summary>
        /// 是否显示
        /// </summary>
        public bool Enable { get; set; }

        /// <summary>
        /// 审核状态 1.待审核 2.已通过 3.不通过 4.已删除
        /// </summary>
        public int State { get; set; }

        

    }
}
