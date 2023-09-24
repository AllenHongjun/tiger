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
    /// �ֵ�����
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// �ֵ����
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// �ֵ���ʾ����
    /// </summary>
    public string DisplayName { get; set; }

    /// <summary>
    /// �ֵ�����
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// �ֵ丸��id
    /// </summary>
    public Guid? ParentId { get; set; }

    /// <summary>
    /// �ֵ���Ŀ
    /// </summary>
    public List<DataItemDto> Items { get; set; } = new List<DataItemDto>();
}