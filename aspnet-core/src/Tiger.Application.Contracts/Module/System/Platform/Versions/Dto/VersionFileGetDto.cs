using System.ComponentModel.DataAnnotations;

namespace Tiger.Module.System.Platform.Versions.Dto
{
    public class VersionFileGetDto
    {
        public PlatformType PlatformType { get; set; } = PlatformType.None;

        [Required]
        [StringLength(AppVersionConsts.MaxVersionLength)]
        public string Version { get; set; }

        [StringLength(VersionFileConsts.MaxPathLength)]
        public string FilePath { get; set; }

        [Required]
        [StringLength(VersionFileConsts.MaxNameLength)]
        public string FileName { get; set; }

        [Required]
        [StringLength(VersionFileConsts.MaxVersionLength)]
        public string FileVersion { get; set; }
    }
}
