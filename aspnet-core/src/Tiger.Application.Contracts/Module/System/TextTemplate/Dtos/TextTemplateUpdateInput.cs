using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Validation;

namespace Tiger.Module.System.TextTemplate.Dtos;

[Serializable]
public class TextTemplateUpdateInput
{
    [Required]
    [DynamicStringLength(typeof(TextTemplateConsts),nameof(TextTemplateConsts.MaxNameLength))]
    public string Name { get; set; }

    [DynamicStringLength(typeof(TextTemplateConsts), nameof(TextTemplateConsts.MaxCultureLength))]
    public string DisplayName { get; set; }

    [Required]
    [DynamicStringLength(typeof(TextTemplateConsts), nameof(TextTemplateConsts.MaxContentLength))]
    public string Content { get; set; }

    public string Culture { get; set; }
}