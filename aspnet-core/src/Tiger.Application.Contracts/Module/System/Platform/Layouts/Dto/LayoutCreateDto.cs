using System;
using System.ComponentModel.DataAnnotations;
using Tiger.Module.System.Platform.Routes;
using Volo.Abp.Validation;

namespace Tiger.Module.System.Platform.Layouts.Dto
{
    public class LayoutCreateDto : LayoutCreateOrUpdateDto
    {
        public Guid DataId { get; set; }

        [Required]
        [DynamicStringLength(typeof(LayoutConsts), nameof(LayoutConsts.MaxFrameworkLength))]
        public string Freamwork { get; set; }
    }
}
