using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Validation;

namespace Tiger.Module.System.Platform.Datas.Dtos;

[Serializable]
public class CreateUpdateDataDto
{   

    /// <summary>
    /// ×ÖµäÃû³Æ
    /// </summary>
    [Required]
    [DynamicStringLength(typeof(DataConsts), nameof(DataConsts.MaxNameLength))]
    public string Name { get; set; }

    /// <summary>
    /// ×ÖµäÏÔÊ¾Ãû³Æ
    /// </summary>
    [Required]
    [DynamicStringLength(typeof(DataConsts), nameof(DataConsts.MaxDisplayNameLength))]
    public string DisplayName { get; set; }

    /// <summary>
    /// ×ÖµäÃèÊö
    /// </summary>
    [DynamicStringLength(typeof(DataConsts), nameof(DataConsts.MaxDescriptionLength))]
    public string Description { get; set; }
}