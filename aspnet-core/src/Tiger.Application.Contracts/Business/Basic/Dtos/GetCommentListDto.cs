using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Tiger.Basic.Comments
{
    public class GetCommentListDto : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// 查询关键字
        /// </summary>
        public string Filter { get; set; }

        /// <summary>
        /// 显示状态 0 不显示; 1 显示;
        /// </summary>
        public int ShowStatus { get; set; }
    }
}
