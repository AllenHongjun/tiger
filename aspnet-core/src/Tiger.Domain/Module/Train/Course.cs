using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Tiger.Module.Exams;
using Tiger.Module.Teachings;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Tiger.Module.Train
{
    /// <summary>
    /// 课程
    /// </summary>
    public class Course : FullAuditedEntity<Guid>, IMultiTenant
    {
        public Guid? TenantId { get; set; }

        /// <summary>
        /// 课程类型：1、公开课程  2、私有课程
        /// </summary>
        public CourseType Type { get; set; }

        /// <summary>
        /// 课程名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 课程描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 封面图片
        /// </summary>
        public string Cover { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Enable { get; set; }

        /// <summary>
        /// 顺序
        /// </summary>
        public int Sorting { get; set; }

        //[NotMapped]
        public virtual ICollection<TestPaper> TestPaper { get; set; }


        protected Course()
        {
        }

        public Course(
            Guid id,
            Guid? tenantId,
            CourseType type,
            string name,
            string description,
            string cover,
            bool enable,
            int sorting
        ) : base(id)
        {
            TenantId = tenantId;
            Type = type;
            Name = name;
            Description = description;
            Cover = cover;
            Enable = enable;
            Sorting = sorting;
        }
    }
}
