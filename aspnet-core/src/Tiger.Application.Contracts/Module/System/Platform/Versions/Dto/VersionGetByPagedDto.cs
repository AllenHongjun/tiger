using Volo.Abp.Application.Dtos;

namespace Tiger.Module.System.Platform.Versions.Dto
{
    public class VersionGetByPagedDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
        public PlatformType PlatformType { get; set; } = PlatformType.None;
    }
}
