using System;
using System.Collections.Generic;
using System.Text;
using Tiger.Module.System.Platform.Routes;

namespace Tiger.Module.System.Platform.Menu
{
    /// <summary>
    /// 菜单
    /// </summary>
    /// <remarks>
    /// 菜单管理模块
    /// 实体和命名空间一个有s一个没有作为区分
    /// </remarks>
    public class Menu:Route
    {
        /// <summary>
        /// 框架
        /// </summary>
        public virtual string Framework { get; set; }

        /// <summary>
        /// 菜单编码
        /// </summary>
        public virtual string Code { get; set; }

        /// <summary>
        /// 布局页 Layout路径
        /// </summary>
        public virtual  string Component { get; set; }

        /// <summary>
        /// 父级菜单ID
        /// </summary>
        public virtual Guid? ParentId { get; set; }

        /// <summary>
        /// 布局ID
        /// </summary>
        public virtual Guid LayoutId { get;set; }

        /// <summary>
        /// 公共菜单
        /// </summary>
        public virtual bool IsPublic { get; set; }
    }
}
