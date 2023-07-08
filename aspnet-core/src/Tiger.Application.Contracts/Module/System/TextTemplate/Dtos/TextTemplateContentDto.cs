using System;
using Volo.Abp.Application.Dtos;

namespace Tiger.Module.System.TextTemplate.Dtos;

[Serializable]
public class TextTemplateContentDto : AuditedEntityDto<Guid>
{
    public string Name { get; set; }


    public string Content { get; set; }

    public string Culture { get; set; }
}