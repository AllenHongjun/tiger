using System;
using System.Collections.Generic;
using System.Text;

namespace Tiger.Module.System.Server
{
    /// <summary>
    /// 磁盘信息
    /// </summary>
    public class DiskInfo
    {
        /// <summary>
        /// 磁盘名
        /// </summary>
        public string DiskName { get; set; }

        /// <summary>
        /// 类型名
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// 总剩余
        /// </summary>
        public long TotalFree { get; set; }

        /// <summary>
        /// 总量
        /// </summary>
        public long TotalSize { get; set; }

        /// <summary>
        /// 已使用
        /// </summary>
        public long Used { get; set; }

        /// <summary>
        /// 可使用
        /// </summary>
        public long AvailableFreeSpace { get; set; }

        /// <summary>
        /// 使用百分比
        /// </summary>
        public decimal AvailablePercent { get; set; }
    }
}
