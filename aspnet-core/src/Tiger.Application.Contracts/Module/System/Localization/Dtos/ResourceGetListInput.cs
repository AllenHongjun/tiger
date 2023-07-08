using System;
using System.ComponentModel;
using Volo.Abp.Application.Dtos;

namespace Tiger.Module.System.Localization.Dtos;

[Serializable]
public class ResourceGetListInput : PagedAndSortedResultRequestDto
{
    public bool? Enable { get; set; }

    public bool? Name { get; set; }

    public string DisplayName { get; set; }

    public string Description { get; set; }
}