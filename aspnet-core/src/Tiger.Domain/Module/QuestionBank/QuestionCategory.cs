using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Tiger.Module.QuestionBank
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
        public string Cover { get; set; }

        /// <summary>
        /// Hierarchical Code of this QuestionCategory.
        /// Example: "00001.00042.00005".
        /// This is a unique code for an QuestionCategory.
        /// It's changeable if QC hierarchy is changed.
        /// </summary>
        public virtual string Code { get; internal set; }

        /// <summary>
        /// 启用
        /// </summary>
        public bool Enable { get; set; }

        /// <summary>
        /// 顺序
        /// </summary>
        public int Sorting { get; set; }

        /// <summary>
        /// 是否公开
        /// </summary>
        public bool IsPublic { get; set; }

        //public virtual QuestionCategory Parent { get; set; }

        //public virtual ICollection<Question> Questions { get; set; }

        

    protected QuestionCategory()
    {
    }

    public QuestionCategory(
        Guid id,
        Guid? tenantId,
        Guid? parentId,
        string name,
        string cover,
        string code,
        bool enable,
        int sorting,
        bool isPublic
    ) : base(id)
    {
        TenantId = tenantId;
        ParentId = parentId;
        Name = name;
        Cover = cover;
        Code = code;
        Enable = enable;
        Sorting = sorting;
        IsPublic = isPublic;
    }
    }
}
