using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;

namespace Tiger.Module.System.Area
{
    /// <summary>
    /// 地区表
    /// </summary>
    /// <remarks>
    /// 
    /// </remarks>
    public class Region :FullAuditedAggregateRoot<Guid>
    {   
        /// <summary>
        /// 地区编码
        /// </summary>
        public string RegionId { get; set; }

        /// <summary>
        /// 地区名称
        /// </summary>
        public string RegionName { get; set; }

        /// <summary>
        /// 地区缩写(只有省才有)
        /// </summary>
        public string RegionShortName { get; set; }

        /// <summary>
        /// 行政地区编码
        /// </summary>
        public string RegionCode { get; set; }

        /// <summary>
        /// 地区父Id
        /// </summary>
        public string RegionParentId { get; set; }

        /// <summary>
        /// 地区级别 1-省、自治区、直辖市 2-地级市、地区、自治州、盟 3-市辖区、县级市、县
        /// </summary>
        public string RegionLevel { get; set; }
    }
}
