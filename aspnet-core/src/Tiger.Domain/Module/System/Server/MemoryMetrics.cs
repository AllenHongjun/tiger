using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tiger.Module.System.Server
{   
    /// <summary>
    /// 内存信息
    /// </summary>
    public class MemoryMetrics
    {
        [JsonIgnore]
        public double Total { get; set; }

        [JsonIgnore]
        public double Used { get; set; }

        [JsonIgnore]
        public double Free { get; set; }

        /// <summary>
        /// 已用内存
        /// </summary>
        public string UsedRam { get; set; }

        /// <summary>
        /// CPU使用率%
        /// </summary>
        public string CpuRate { get; set; }

        /// <summary>
        /// 总内存 GB
        /// </summary>
        public string TotalRam { get; set; }

        /// <summary>
        /// 内存使用率 %
        /// </summary>
        public string RamRate { get; set; }

        /// <summary>
        /// 空闲内存
        /// </summary>
        public string FreeRam { get; set; }
    }
}
