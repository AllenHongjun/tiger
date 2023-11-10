using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;

namespace Tiger.Module.Forum
{
    /// <summary>
    /// 板块
    /// </summary>
    public class Section:FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 版块标题
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 版块主图
        /// </summary>
        public string ImgUrl { get; set; }
        /// <summary>
        /// 版块内容(详情)
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 是否公开
        /// </summary>
        public bool IsPublic { get; set; }
        /// <summary>
        /// 顺序
        /// </summary>
        public int Sort { get; set; }
        
        /// <summary>
        /// 是否显示
        /// </summary>
        public bool Enable { get; set; }
    }
}
