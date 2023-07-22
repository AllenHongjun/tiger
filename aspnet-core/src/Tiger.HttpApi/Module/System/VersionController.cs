using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tiger.Module.System.Platform;
using Tiger.Module.System.Platform.Versions;
using Tiger.Module.System.Platform.Versions.Dto;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.IO;
using Volo.Abp.Settings;

namespace Tiger.Module.System
{   
    /// <summary>
    /// 版本文件管理
    /// </summary>
    [RemoteService(Name = PlatformRemoteServiceConsts.RemoteServiceName)]
    [Area("platform")]
    [Route("api/platform/version")]
    public class VersionController:AbpController,IVersionAppService
    {
        private readonly IVersionFileManager _versionFileManager;
        private readonly IVersionAppService _versionAppService;
        private readonly ISettingProvider _settingProvider;

        
        [HttpPost]
        public async Task<VersionDto> CreateAsync(VersionCreateDto versionCreate)
        {
            return await _versionAppService.CreateAsync(versionCreate);
        }

        [HttpDelete]
        public async Task DeleteAsync(VersionDeleteDto versionDelete)
        {
            await _versionAppService.DeleteAsync(versionDelete);
        }

        
        [HttpGet]
        [Route("{Id}")]
        public async Task<VersionDto> GetAsync(VersionGetByIdDto versionGetById)
        {
            return await _versionAppService.GetAsync(versionGetById);
        }

        [HttpGet]
        [Route("lastest")]
        public async Task<VersionDto> GetLastestAsync(PlatformType platformType)
        {
            return await _versionAppService.GetLastestAsync(platformType);
        }

        [HttpGet]
        public async Task<PagedResultDto<VersionDto>> GetListAsync(VersionGetByPagedDto input)
        {
            return await _versionAppService.GetListAsync(input);
        }

        /// <summary>
        /// 添加版本文件
        /// </summary>
        /// <param name="versionFileCreate"></param>
        /// <returns></returns>
        /// <exception cref="UserFriendlyException"></exception>
        [HttpPost]
        [Route("file/append")]
        [RequestSizeLimit(200_000_000)]
        public async Task AppendFileAsync([FromQuery] VersionFileCreateDto versionFileCreate)
        {
            // 检查文件大小
            var fileSizeLimited = await _settingProvider
                .GetOrNullAsync(PlatformSettingNames.AppVersion.VersionFileLimitLength);
            if (Convert.ToInt32(fileSizeLimited) * 1024 * 1024 < versionFileCreate.TotalByte)
            {
                throw new UserFriendlyException(L["UploadFileSizeBeyondLimit", fileSizeLimited]);
            }
            // 采用分块模式上传文件

            // 保存分块到临时目录
            var fileName = versionFileCreate.FileName;
            // 文件扩展名
            var fileExtensionName = FileHelper.GetExtension(fileName);
            var fileAllowExtension = await _settingProvider
                .GetOrNullAsync(PlatformSettingNames.AppVersion.AllowVersionFileExtensions);
            if (fileAllowExtension.IsNullOrWhiteSpace())
            {
                fileAllowExtension = AppVersionConsts.DefaultAllowVersionFileExtensions;
            }
            // 检查文件扩展名
            if (!fileAllowExtension.Split(',').Any(fe => fe.Equals(fileExtensionName, StringComparison.CurrentCultureIgnoreCase)))
            {
                throw new UserFriendlyException(L["NotAllowedFileExtensionName", fileExtensionName]);
            }
            // 当前计算机临时目录
            var tempFilePath = Environment.GetFolderPath(Environment.SpecialFolder.Templates);
            // 以上传的文件名创建一个临时目录
            tempFilePath = Path.Combine(tempFilePath, "lingyun-platform", Path.GetFileNameWithoutExtension(fileName));
            // 以上传的分片索引创建临时文件
            var tempSavedFile = Path.Combine(tempFilePath, $"{versionFileCreate.CurrentByte}.{fileExtensionName}");
            if (!Directory.Exists(tempFilePath))
            {
                // 临时目录不存在则创建
                Directory.CreateDirectory(tempFilePath);
            }
            try
            {
                if (HttpContext.RequestAborted.IsCancellationRequested)
                {
                    // 如果取消请求,删除临时目录
                    Directory.Delete(tempFilePath, true);
                    return;
                }
                // 保存临时文件
                using (var fs = new FileStream(tempSavedFile, FileMode.Create, FileAccess.Write))
                {
                    // 写入当前分片文件
                    await Request.Body.CopyToAsync(fs);
                }

                if (versionFileCreate.CurrentByte == versionFileCreate.TotalByte)
                {
                    // 合并文件
                    var mergeSavedFile = Path.Combine(tempFilePath, $"{fileName}");
                    // 获取并排序所有分片文件
                    var mergeFiles = Directory.GetFiles(tempFilePath).OrderBy(f => f.Length).ThenBy(f => f);
                    // 创建临时合并文件
                    using (var mergeSavedFileStream = new FileStream(mergeSavedFile, FileMode.Create))
                    {
                        foreach (var mergeFile in mergeFiles)
                        {
                            // 读取当前文件字节
                            var mergeFileBytes = await FileHelper.ReadAllBytesAsync(mergeFile);
                            // 写入到合并文件流
                            await mergeSavedFileStream.WriteAsync(mergeFileBytes, 0, mergeFileBytes.Length);
                            // 删除已参与合并的临时文件分片
                            FileHelper.DeleteIfExists(mergeFile);
                        }
                        // 上传最终合并的文件并取得SHA256指纹
                        var fileData = await mergeSavedFileStream.GetAllBytesAsync();
                        versionFileCreate.SHA256 = await _versionFileManager.SaveFileAsync(versionFileCreate.Version,
                            versionFileCreate.FilePath, versionFileCreate.FileName, versionFileCreate.FileVersion, fileData);
                    }
                    // 添加到版本信息
                    await _versionAppService.AppendFileAsync(versionFileCreate);
                    // 文件保存之后删除临时文件目录
                    Directory.Delete(tempFilePath, true);
                }
            }
            catch
            {
                // 发生异常删除临时文件目录
                Directory.Delete(tempFilePath, true);
                throw;
            }
        }


        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="versionFileGet"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("file/download")]
        public async Task DownloadFileAsync(VersionFileGetDto versionFileGet)
        {
            // 分块模式下载文件

            // 得到文件流
            var fileStream = await _versionFileManager.DownloadFileAsync(
                versionFileGet.PlatformType, versionFileGet.Version, versionFileGet.FilePath,
                versionFileGet.FileName, versionFileGet.FileVersion);
            // 得到文件扩展名
            var fileExt = Path.GetExtension(versionFileGet.FileName);
            var provider = new FileExtensionContentTypeProvider();
            // Http响应标头的文件类型
            string memi = provider.Mappings[fileExt];
            using (Response.Body)
            {
                // Http响应标头的文件类型
                Response.ContentType = memi;
                // 文件大小
                byte[] contentBytes = await fileStream.GetAllBytesAsync();
                long contentLength = contentBytes.Length;
                // 指定响应内容大小
                Response.ContentLength = contentLength;
                // 单个分块大小 2MB
                int bufferSize = 2 * 1024 * 1024;
                // 分块总数
                int contentByteCount = Math.DivRem(contentBytes.Length, bufferSize, out int modResult);
                for (int index = 0; index < contentByteCount; index++)
                {
                    // 当前分页传输字节
                    byte[] currentTransferBytes = new byte[bufferSize];
                    if (index == contentByteCount - 1)
                    {
                        // 最后一个分块和余数大小一起传输
                        if (modResult > 0)
                        {
                            currentTransferBytes = new byte[bufferSize + modResult];
                        }
                    }
                    // 复制文件流中的当前分块区段
                    Array.Copy(contentBytes, index * bufferSize, currentTransferBytes, 0, currentTransferBytes.Length);
                    // 写入响应流
                    await Response.Body.WriteAsync(currentTransferBytes, 0, currentTransferBytes.Length);
                    // 清空缓冲区
                    await Response.Body.FlushAsync();
                }
            }
        }


        [HttpGet]
        [Route("file/remove-all")]
        public async Task RemoveAllFileAsync(VersionGetByIdDto versionGetById)
        {
            await _versionAppService.RemoveAllFileAsync(versionGetById);
        }

        /// <summary>
        /// 移除版本文件
        /// </summary>
        /// <param name="versionFileDelete"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("file/remove-file")]
        public async Task RemoveFileAsync(VersionFileDeleteDto versionFileDelete)
        {
            await _versionAppService.RemoveFileAsync(versionFileDelete);
        }
    }
}
