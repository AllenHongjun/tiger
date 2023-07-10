using System;
using System.Collections.Generic;
using System.Text;

namespace Tiger.Module.System.Platform.Menus
{
    /// <summary>
    /// 标准菜单
    /// </summary>
    public class StandardMenu
    {
        /// <summary>
        /// 菜单
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 路径
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 展示名称
        /// </summary>
        public string DisplayName { get; set; } 

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 重定向路径
        /// </summary>
        public string Redirect {get; set; }
    }
}
