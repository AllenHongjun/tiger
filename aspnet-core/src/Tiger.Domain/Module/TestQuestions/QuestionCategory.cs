using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Tiger.Module.TestQuestions
{
    /// <summary>
    /// 题目分类
    /// </summary>
    public class QuestionCategory:FullAuditedAggregateRoot<Guid>,IMultiTenant
    {
        public Guid? TenantId { get; set; }

        public Guid? ParentId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 封面
        /// </summary>
        public string ImgUrl { get; set; }

        /// <summary>
        /// 是否显示
        /// </summary>
        public bool IsEnable { get; set; }

        /// <summary>
        /// 顺序
        /// </summary>
        public int Sorting { get; set; }

        /// <summary>
        /// 是否公开
        /// </summary>
        public bool IsPublic { get; set; }

        
    }
}
