using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tiger.Module.System.Platform.Permissions;
using Tiger.Module.System.Platform.Versions.Dto;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tiger.Module.System.Platform.Versions
{
    /// <summary>
    /// 版本文件管理
    /// </summary>
    [Authorize(PlatformPermissions.AppVersion.Default)]
    [RemoteService(IsEnabled = false)]
    public class VersionAppService:ApplicationService,IVersionAppService
    {
        public VersionAppService(
            VersionManager versionManager, 
            IVersionRepository versionRepository)
        {
            VersionManager=versionManager;
            VersionRepository=versionRepository;
        }

        protected  VersionManager VersionManager { get; }
        protected  IVersionRepository VersionRepository { get; }


        public async Task AppendFileAsync(VersionFileCreateDto versionFileCreate)
        {
            await VersionManager.AppendFileAsync(
                versionFileCreate.VersionId,
                versionFileCreate.SHA256,
                versionFileCreate.FileName,
                versionFileCreate.FileVersion,
                versionFileCreate.TotalByte,
                versionFileCreate.FilePath,
                versionFileCreate.FileType);
        }

        [Authorize(PlatformPermissions.AppVersion.Create)]
        public async Task<VersionDto> CreateAsync(VersionCreateDto versionCreate)
        {
            if (await VersionRepository.ExistsAsync(versionCreate.PlatformType, versionCreate.Version))
            {
                throw new UserFriendlyException("VersionAlreadyExists");
            }
            var version = new AppVersion(
                GuidGenerator.Create(),
                versionCreate.Title,
                versionCreate.Version,
                versionCreate.PlatformType,
                CurrentTenant.Id)
            {
                Description = versionCreate.Description,
                Level = versionCreate.Level
            };
            await VersionRepository.InsertAsync(version);
            return ObjectMapper.Map<AppVersion, VersionDto>(version);
        }

        [Authorize(PlatformPermissions.AppVersion.Delete)]
        public async Task DeleteAsync(VersionDeleteDto versionDelete)
        {
            var version = await VersionRepository.GetByVersionAsync(
                versionDelete.PlatformType,
                versionDelete.Version);

            if (version != null)
            {
                await VersionRepository.DeleteAsync(version.Id,true);
            }
        }

        public Task DownloadFileAsync(VersionFileGetDto versionFileGet)
        {
            throw new NotImplementedException();
            return Task.CompletedTask;
        }

        public async Task<PagedResultDto<VersionDto>> GetListAsync(VersionGetByPagedDto input)
        {
            var versionCount = await VersionRepository.GetCountAsync(input.PlatformType,
                input.Filter);
            var versions = await VersionRepository.GetPagedListAsync(input.PlatformType,
                input.Filter, input.Sorting, true, input.SkipCount, input.MaxResultCount);
            return new PagedResultDto<VersionDto>(
                versionCount,
                ObjectMapper.Map<List<AppVersion>, List<VersionDto>>(versions));

        }

        public async Task<VersionDto> GetAsync(VersionGetByIdDto versionGetById)
        {
            var version = await VersionRepository.GetAsync(versionGetById.Id);
            return ObjectMapper.Map<AppVersion,VersionDto>(version);
        }

        public  Task<VersionDto> GetLastestAsync(PlatformType platformType)
        {
            //var version = await VersionRepository.GetByVersionAsync
            throw new NotImplementedException();
        }

        [Authorize(PlatformPermissions.AppVersion.FileManager.Delete)]
        public async Task RemoveAllFileAsync(VersionGetByIdDto versionGetById)
        {
            await VersionManager.RemoveAllFileAsync(versionGetById.Id);
        }


        public async Task RemoveFileAsync(VersionFileDeleteDto versionFileDelete)
        {
            await VersionManager.RemoveFileAsync(versionFileDelete.VersionId, versionFileDelete.FileName);
        }
    }
}
