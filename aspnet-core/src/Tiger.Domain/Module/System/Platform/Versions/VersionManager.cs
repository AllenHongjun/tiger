using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.BlobStoring;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Services;
using Volo.Abp.Uow;

namespace Tiger.Module.System.Platform.Versions
{   
    /// <summary>
    /// 版本及版本文件管理
    /// </summary>
    [Dependency(Microsoft.Extensions.DependencyInjection.ServiceLifetime.Transient)]
    [ExposeServices(typeof(IVersionFileManager), typeof(VersionManager))]
    public class VersionManager : DomainService, IVersionFileManager
    {
        public VersionManager(
            IVersionRepository versionRepository, 
            IBlobContainer<VersionContainer> versionBlobContainer)
        {
            VersionRepository=versionRepository;
            VersionBlobContainer=versionBlobContainer;
        }

        protected IVersionRepository VersionRepository { get;}
        protected IBlobContainer<VersionContainer> VersionBlobContainer { get; }


        /// <summary>
        /// 下载版本文件
        /// </summary>
        /// <param name="platformType"></param>
        /// <param name="version"></param>
        /// <param name="filePath"></param>
        /// <param name="fileName"></param>
        /// <param name="fileVersion"></param>
        /// <returns></returns>
        public async Task<Stream> DownloadFileAsync(PlatformType platformType, string version, string filePath, string fileName, string fileVersion)
        {
            var appVersion =  await VersionRepository.GetByVersionAsync(platformType, version);
            var versionFile = appVersion.FindFile(filePath, fileName, fileVersion);
            if (versionFile == null)
            {
                throw new BusinessException(PlatformErrorCodes.VersionFileNotFound)
                    .WithData("FileName", fileName)
                    .WithData("FileVersion", fileVersion);
            }
            versionFile.Download();
            // 通过ABP的对象存储获取下载的文件
            return await VersionBlobContainer.GetAsync(
                VersionFile.NormalizeBlobName(version, versionFile.Name, versionFile.Version, versionFile.Path));
        }

        /// <summary>
        /// 获取版本文件
        /// </summary>
        /// <param name="versionFile"></param>
        /// <returns></returns>
        public async virtual Task<Stream> GetFileAsync(VersionFile versionFile)
        {
            versionFile.Download();
            return await VersionBlobContainer.GetAsync(
                VersionFile.NormalizeBlobName(versionFile.AppVersion.Version, versionFile.Name, versionFile.Version, versionFile.Path));
        }



        /// <summary>
        /// 保存(添加)文件
        /// </summary>
        /// <param name="version"></param>
        /// <param name="filePath"></param>
        /// <param name="fileName"></param>
        /// <param name="fileVersion"></param>
        /// <param name="data">文件二进制数据</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<string> SaveFileAsync(string version, string filePath, string fileName, string fileVersion, byte[] data)
        {
            var sha256 = SHA256.Create();
            var checkHash = sha256.ComputeHash(data);
            var sha256Hash = BitConverter.ToString(checkHash).Replace("-", string.Empty);

            await VersionBlobContainer.SaveAsync(VersionFile.NormalizeBlobName(version, fileName, fileVersion, filePath), data, true);

            return sha256Hash;
        }




        /// <summary>
        /// 添加版本文件
        /// </summary>
        /// <param name="versionId"></param>
        /// <param name="fileSha256"></param>
        /// <param name="fileName"></param>
        /// <param name="fileVersion"></param>
        /// <param name="fileSize"></param>
        /// <param name="filePath"></param>
        /// <param name="fileType"></param>
        /// <returns></returns>
        [UnitOfWork]
        public async virtual Task AppendFileAsync(Guid versionId, string fileSha256,
            string fileName, string fileVersion,
            long fileSize, string filePath = "",
            FileType fileType = FileType.Stream)
        {
            var appVersion = await VersionRepository.GetAsync(versionId);
            if (appVersion.FileExists(fileName))
            {
                appVersion.RemoveFile(fileName);
            }
            appVersion.AppendFile(fileName, fileVersion, fileSize, fileSha256, filePath,fileType);
        }

        /// <summary>
        /// 删除一个版本文件
        /// </summary>
        /// <param name="versionId"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        [UnitOfWork]
        public async virtual Task RemoveFileAsync(Guid versionId, string fileName)
        {
            var appVersion = await VersionRepository.GetAsync(versionId);
            var versionFile = appVersion.FindFile(fileName);
            if (versionFile != null)
            {
                // 从oss对象存储当中删除文件
                await VersionBlobContainer
                    .DeleteAsync(VersionFile.NormalizeBlobName(appVersion.Version, versionFile.Name,
                    versionFile.Version));
                appVersion.RemoveFile(fileName);
            }
        }

        /// <summary>
        /// 删除版本对应的所有文件
        /// </summary>
        /// <param name="versionId"></param>
        /// <returns></returns>
        public async virtual Task RemoveAllFileAsync(Guid versionId)
        {
            var appVersion = await VersionRepository.GetAsync(versionId);
            foreach(var versionFile in appVersion.Files)
            {
                await VersionBlobContainer
                    .DeleteAsync(VersionFile.NormalizeBlobName(appVersion.Version, versionFile.Name, versionFile.Version));
            }
            appVersion.RemoveAllFile();
        }

    }
}
