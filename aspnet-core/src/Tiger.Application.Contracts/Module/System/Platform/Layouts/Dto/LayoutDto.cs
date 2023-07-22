using System;
using Tiger.Module.System.Platform.Routes.Dto;

namespace Tiger.Module.System.Platform.Layouts.Dto
{
    public class LayoutDto : RouteDto
    {
        /// <summary>
        /// 框架
        /// </summary>
        public string Framework { get; set; }
        /// <summary>
        /// 约定的Meta采用哪种数据字典,主要是约束路由必须字段的一致性
        /// </summary>
        public Guid DataId { get; set; }
    }
}
