using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Tiger.Module.System.Platform.Routes;
using Volo.Abp.Validation;

namespace Tiger.Module.System.Platform.Menus.Dto
{
    /// <summary>
    /// 菜单
    /// </summary>
    public class MenuCreateOrUpdateDto
    {
        /// <summary>
        /// 父级id
        /// </summary>
        public Guid? ParentId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [DynamicStringLength(typeof(RouteConsts), nameof(RouteConsts.MaxNameLength))]
        public string Name { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        [Required]
        [DynamicStringLength(typeof(RouteConsts), nameof(RouteConsts.MaxDisplayNameLength))]
        public string DisplayName { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        [DynamicStringLength(typeof(RouteConsts), nameof(RouteConsts.MaxDescriptionLength))]
        public string Description { get; set; }

        /// <summary>
        /// 路径
        /// </summary>
        [Required]
        [DynamicStringLength(typeof(RouteConsts), nameof(RouteConsts.MaxPathLength))]
        public string Path { get; set; }

        /// <summary>
        /// 跳转路径
        /// </summary>
        [DynamicStringLength(typeof(RouteConsts), nameof(RouteConsts.MaxRedirectLength))]
        public string Redirect { get; set; }

        /// <summary>
        /// 组件
        /// </summary>
        [Required]
        [DynamicStringLength(typeof(MenuConsts), nameof(MenuConsts.MaxComponentLength))]
        public string Component { get; set; }

        /// <summary>
        /// 是否公开
        /// </summary>
        public bool IsPublic { get; set; }

        public Dictionary<string, object> Meta { get; set; } = new Dictionary<string, object>();
    }
}
