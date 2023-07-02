using System;
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
}
