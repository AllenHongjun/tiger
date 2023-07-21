using System.ComponentModel.DataAnnotations;

namespace Tiger.Module.System.Platform.Versions.Dto
{
    public class VersionDeleteDto
    {
        [Required]
        [StringLength(AppVersionConsts.MaxVersionLength)]
        public string Version { get; set; }

        public PlatformType PlatformType { get; set; }
    }
}
