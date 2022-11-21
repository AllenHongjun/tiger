using System;
using System.Collections.Generic;
using System.Text;

namespace Tiger.CoreModule.DataFiltiering
{
    /// <summary>
    /// 过滤活跃/消极数据,任何实体都可以实现它（自定义的数据过滤）
    /// </summary>
    public interface IsActive
    {

        /// <summary>
        /// 过滤活跃/消极数据 (接口里面添加一个属性)
        /// </summary>
        bool? IsActive { get; }
    }
}
