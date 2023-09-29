using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Tiger.Volo.Abp.Sass.Editions;

public interface IEditionAppService :
    ICrudAppService<
        EditionDto,
        Guid,
        EditionGetListInput,
        EditionCreateDto,
        EditionUpdateDto>
{

    /// <summary>
    /// 移动所有租户到指定版本
    /// </summary>
    /// <param name="editionId">当前版本</param>
    /// <param name="dstEditionId">目标版本</param>
    /// <returns></returns>
    Task MoveAllTenantAsync(Guid editionId, Guid? dstEditionId);


    /// <summary>
    /// 统计每个版本的租户数量
    /// </summary>
    /// <returns></returns>
    Task<List<EditionDto>> GetUsageStatisticAsync();
}
