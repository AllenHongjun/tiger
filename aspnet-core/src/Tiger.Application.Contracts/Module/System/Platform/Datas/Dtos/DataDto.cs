using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Validation;

namespace Tiger.Module.System.Platform.Datas.Dtos;

[Serializable]
public class DataDto : FullAuditedEntityDto<Guid>
{
    public string Name { get; set; }

    public string Code { get; set; }

    public string DisplayName { get; set; }

    public string Description { get; set; }

    public Guid? ParentId { get; set; }

    public List<DataItemDto> Items { get; set; } = new List<DataItemDto>();
}