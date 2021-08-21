using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Domain.Entities;
using Volo.Abp.MultiTenancy;

namespace Tiger.Basic.Categorys
{
    public class CreateUpdateCategoryDto
    {
        //public Guid? TenantId { get; set; }

        public Guid? ParentId { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        /// <summary>
        /// 级别
        /// </summary>
        public int Level { get; set; }

        public int ProductCount { get; set; }

        /// <summary>
        /// 显示状态 0->不显示 1->显示
        /// </summary>
        [Required]
        public int ShowStatus { get; set; }

        [Required]
        public int Sort { get; set; }

        public string Icon { get; set; }

        /// <summary>
        /// 关键字
        /// </summary>
        public string Keyword { get; set; }

        public string Description { get; set; }
    }
}
