using System.ComponentModel.DataAnnotations;
using Tiger.Module.System.Platform.Routes;
using Volo.Abp.Validation;

namespace Tiger.Module.System.Platform.Layouts.Dto
{
    public class LayoutCreateOrUpdateDto
    {
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
        /// 重定向路径
        /// </summary>
        [DynamicStringLength(typeof(RouteConsts), nameof(RouteConsts.MaxRedirectLength))]
        public string Redirect { get; set; }
    }
}
