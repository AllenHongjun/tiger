using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Volo.Abp.Identity
{
    public class GetAllOrgnizationUnitInput : ISortedResultRequest
    {
        public GetAllOrgnizationUnitInput()
        {
            Sorting = "Code";
        }

        /// <summary>
        /// 过滤条件
        /// </summary>
        public string Filter { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public string Sorting { get; set; }
    }
}
