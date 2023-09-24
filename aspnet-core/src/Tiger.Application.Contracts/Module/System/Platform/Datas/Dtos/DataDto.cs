using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Validation;

namespace Tiger.Module.System.Platform.Datas.Dtos;

[Serializable]
public class LanguageTextDto : FullAuditedEntityDto<Guid>
{
    /// <summary>
    /// ×ÖµäÃû³Æ
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// ×Öµä±àÂë
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// ×ÖµäÏÔÊ¾Ãû³Æ
    /// </summary>
    public string DisplayName { get; set; }

    /// <summary>
    /// ×ÖµäÃèÊö
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// ×Öµä¸¸¼¶id
    /// </summary>
    public Guid? ParentId { get; set; }

    /// <summary>
    /// ×ÖµäÏîÄ¿
    /// </summary>
    public List<DataItemDto> Items { get; set; } = new List<DataItemDto>();
}