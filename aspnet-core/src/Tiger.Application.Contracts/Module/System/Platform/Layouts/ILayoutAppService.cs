using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tiger.Module.System.Platform.Layouts.Dto;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tiger.Module.System.Platform.Layouts
{
    public interface ILayoutAppService:
        ICrudAppService<
            LayoutDto,
            Guid,
            GetLayoutListInput,
            LayoutCreateDto,
            LayoutUpdateDto>
    {
        Task<ListResultDto<LayoutDto>> GetAllListAsync();
    }
}
