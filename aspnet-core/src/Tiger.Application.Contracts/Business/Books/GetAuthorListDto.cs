using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Tiger.Books
{
    /// <summary>
    /// Filter is used to search authors. It can be null (or empty string) to get all the authors.
    ///PagedAndSortedResultRequestDto has the standard paging and sorting properties: int MaxResultCount, int SkipCount and string Sorting.
    /// </summary>
    public class GetAuthorListDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        /// <summary>
        /// 货号
        /// </summary>
        public string ProductSn { get; set; }

        
    }
}
