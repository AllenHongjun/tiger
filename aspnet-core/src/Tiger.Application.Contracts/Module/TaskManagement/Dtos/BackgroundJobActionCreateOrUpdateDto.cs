using System.Collections.Generic;
using Volo.Abp.Data;

namespace Tiger.Module.TaskManagement.Dtos;

public abstract class BackgroundJobActionCreateOrUpdateDto
{
    /// <summary>
    /// 是否启用
    /// </summary>
    public bool IsEnabled { get; set; }
    /// <summary>
    /// 参数
    /// </summary>
    public IDictionary<string, object> Paramters { get; set; }

    public BackgroundJobActionCreateOrUpdateDto()
    {
        Paramters = new Dictionary<string, object>();
    }
}
