using System;
using System.Collections.Generic;
using System.Text;

namespace Tiger.Module.System.Platform.Layouts
{
    /// <summary>
    /// 布局视图
    /// </summary>
    public class Layout
    {
        /// <summary>
        /// 框架
        /// </summary>
        public virtual string Framework { get;protected set; }

        /// <summary>
        /// 约定的Meta采用哪种数据字典,主要是约束路由必须字段的一致性
        /// </summary>
        public virtual Guid DataId { get; protected set; }
    }
}
