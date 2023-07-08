using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Validation;

namespace Tiger.Module.System.TextTemplate.Dtos
{
    public class TextTemplateRestoreInput
    {
        [Required]
        [DynamicStringLength(typeof(TextTemplateConsts), nameof(TextTemplateConsts.MaxNameLength))]
        public string Name { get; set; }

        [DynamicStringLength(typeof(TextTemplateConsts), nameof(TextTemplateConsts.MaxCultureLength))]
        public string Culture { get; set; }
    }
}
