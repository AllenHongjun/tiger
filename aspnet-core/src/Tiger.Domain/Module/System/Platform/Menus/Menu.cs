using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Text;
using Tiger.Module.System.Platform.Routes;
using Volo.Abp;

namespace Tiger.Module.System.Platform.Menus
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
        public Menu()
        {
        }

        public Menu(
            [NotNull] Guid id,
            [NotNull] Guid layoutId,
            [NotNull] string path,
            [NotNull] string name,
            [NotNull] string code, 
            [NotNull] string component, 
            [NotNull] string displayName,
            [NotNull] string framework,
            string redirect = "",
            string description = "",
            Guid? parentId = null,
            [NotNull] bool status = true,
            [CanBeNull] string icon = "",
            Guid? tenantId = null
            ): base(id, path, name, displayName, redirect, description,status,icon, tenantId)
        {
            Check.NotNullOrWhiteSpace(code, nameof(code));

            LayoutId = layoutId;
            Code = code;
            Component = component;// 下属的一级菜单的Component应该是布局页, 例如vue-admin中的 component: Layout, 其他前端框架雷同, 此处应传递布局页的路径让前端import
            Framework = framework;
            ParentId = parentId;

            IsPublic = false;
        }

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
