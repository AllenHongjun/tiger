using System;
using System.ComponentModel.DataAnnotations;

namespace Tiger.Module.System.Platform.Versions.Dto
{
    public class VersionFileDeleteDto
    {
        [Required]
        public Guid VersionId { get; set; }

        [Required]
        [StringLength(VersionFileConsts.MaxNameLength)]
        public string FileName { get; set; }
    }
}
