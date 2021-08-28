using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Tiger.Basic.Categorys
{
    public class GetCategoryListDto:PagedAndSortedResultRequestDto
    {   
        /// <summary>
        /// 查询关键字
        /// </summary>
        public string Filter { get; set; }
        /// <summary>
        /// 父级分类id
        /// </summary>
        public Guid? ParentId { get; set; }
    }
}
