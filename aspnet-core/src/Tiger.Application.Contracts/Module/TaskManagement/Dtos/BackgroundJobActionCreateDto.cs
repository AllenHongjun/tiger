using System.ComponentModel.DataAnnotations;
using Volo.Abp.Validation;

namespace Tiger.Module.TaskManagement.Dtos;

public class BackgroundJobActionCreateDto : BackgroundJobActionCreateOrUpdateDto
{
    /// <summary>
    /// 名称
    /// </summary>
    [Required]
    [DynamicStringLength(typeof(BackgroundJobActionConsts), nameof(BackgroundJobActionConsts.MaxNameLength))]
    public string Name { get; set; }
}
