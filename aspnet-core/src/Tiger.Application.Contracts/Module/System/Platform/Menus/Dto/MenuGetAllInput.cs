using System;
using Tiger.Module.System.Platform.Routes;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Validation;

namespace Tiger.Module.System.Platform.Menus.Dto
{
    public class MenuGetAllInput : ISortedResultRequest
    {
        /// <summary>
        /// 框架
        /// </summary>
        [DynamicStringLength(typeof(LayoutConsts), nameof(LayoutConsts.MaxFrameworkLength))]
        public string Framework { get; set; }

        public string Filter { get; set; }

        public bool Reverse { get; set; }

        /// <summary>
        /// 父级id
        /// </summary>
        public Guid? ParentId { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public string Sorting { get; set; }

        /// <summary>
        /// 布局id
        /// </summary>
        public Guid? LayoutId { get; set; }
    }
}
