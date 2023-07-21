using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tiger.Module.System.Platform.Versions.Dto;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tiger.Module.System.Platform.Versions
{
    /// <summary>
    /// 版本
    /// </summary>
    public interface IVersionAppService:IApplicationService
    {
        Task<VersionDto> GetLastestAsync(PlatformType platformType);

        Task<PagedResultDto<VersionDto>> GetAsync(VersionGetByPagedDto input);

        Task<VersionDto> GetAsync(VersionGetByIdDto versionGetById);

        Task<VersionDto> CreateAsync(VersionCreateDto versionCreate);

        /// <summary>
        /// 删除版本
        /// </summary>
        /// <param name="versionDelete"></param>
        /// <returns></returns>
        Task DeleteAsync(VersionDeleteDto versionDelete);

        /// <summary>
        /// 添加版本文件
        /// </summary>
        /// <param name="versionFileCreate"></param>
        /// <returns></returns>
        Task AppendFileAsync(VersionFileCreateDto versionFileCreate);
        Task RemoveFileAsync(VersionFileDeleteDto versionFileDelete);
        Task RemoveAllFileAsync(VersionGetByIdDto versionGetById);
        Task DownloadFileAsync(VersionFileGetDto versionFileGet);
    }
}
