using Tiger.Module.System.Platform.Routes;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Validation;

namespace Tiger.Module.System.Platform.Layouts.Dto
{
    public class GetLayoutListInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public bool Reverse { get; set; }

        [DynamicStringLength(typeof(LayoutConsts), nameof(LayoutConsts.MaxFrameworkLength))]
        public string Framework { get; set; }
    }
}
