using System;
using System.Collections.Generic;
using System.ComponentModel;
using Volo.Abp.Application.Dtos;

namespace Tiger.Module.System.Platform.Datas.Dtos;

[Serializable]
public class DataGetListInput : PagedAndSortedResultRequestDto
{
    public string Filter { get; set; }
}