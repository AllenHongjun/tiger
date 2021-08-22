using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Tiger.Basic.ProductAttributes
{
    public class GetProductAttributeListDto : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// 查询关键字
        /// </summary>
        public string Filter { get; set; }
    }
}
