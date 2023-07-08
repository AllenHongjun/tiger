using System;
using System.ComponentModel;
using Volo.Abp.Application.Dtos;

namespace Tiger.Module.System.Localization.Dtos;

[Serializable]
public class LanguageGetListInput : PagedAndSortedResultRequestDto
{   
    public string Filter { get; set; }
    
}