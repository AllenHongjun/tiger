using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;

namespace Tiger.Module.Teaching
{
    /// <summary>
    /// 课程
    /// </summary>
    public class Course:FullAuditedEntity<Guid>
    {
        /// <summary>
        /// 课程类型：1、公开课程  2、私有课程
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 课程名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 课程描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 课程图片
        /// </summary>
        public string CourseImage { get; set; }

        /// <summary>
        /// 封面图片
        /// </summary>
        public string CoverImage { get; set; }
        
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnable { get; set; }

        /// <summary>
        /// 顺序
        /// </summary>
        public int Sorting { get; set; }
        
    }
}
