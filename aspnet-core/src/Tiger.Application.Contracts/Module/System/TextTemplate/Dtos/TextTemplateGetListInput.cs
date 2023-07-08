using System;
using System.ComponentModel;
using Volo.Abp.Application.Dtos;

namespace Tiger.Module.System.TextTemplate.Dtos;

[Serializable]
public class TextTemplateGetListInput : PagedAndSortedResultRequestDto
{
    public string Filter { get; set; }
}