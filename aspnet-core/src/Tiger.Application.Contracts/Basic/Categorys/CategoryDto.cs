using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.MultiTenancy;

namespace Tiger.Basic.Categorys
{
    public class CategoryDto : EntityDto<Guid>, IMultiTenant
    {
        public Guid? TenantId { get; set; }

        public Guid? ParentId { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// 级别
        /// </summary>
        public int Level { get; set; }

        public int ProductCount { get; set; }

        /// <summary>
        /// 显示状态 0->不显示 1->显示
        /// </summary>
        public int ShowStatus { get; set; }

        public int Sort { get; set; }

        public string Icon { get; set; }

        /// <summary>
        /// 关键字
        /// </summary>
        public string Keyword { get; set; }

        public string Description { get; set; }

    }
}
