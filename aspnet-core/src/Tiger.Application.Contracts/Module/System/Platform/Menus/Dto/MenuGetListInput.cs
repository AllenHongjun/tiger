using System;
using Tiger.Module.System.Platform.Routes;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Validation;

namespace Tiger.Module.System.Platform.Menus.Dto
{
    public class MenuGetListInput : PagedAndSortedResultRequestDto
    {
        [DynamicStringLength(typeof(LayoutConsts), nameof(LayoutConsts.MaxFrameworkLength))]
        public string Framework { get; set; }

        public string Filter { get; set; }

        public bool Reverse { get; set; }

        public Guid? ParentId { get; set; }

        public Guid? LayoutId { get; set; }
    }
}
