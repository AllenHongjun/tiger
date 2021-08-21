/**
 * 类    名：GetAuthorListDto   
 * 作    者：花生了什么树       
 * 创建时间：2021/8/17 1:22:44       
 * 说    明: 
 * 
 */

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
