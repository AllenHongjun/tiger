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

namespace Tiger.Module.System.Platform.Versions
{   
    /// <summary>
    /// 版本管理
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
        /// 保存文件
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



        //public async virtual Task AppendFileAsync(Guid versionId, string fileSha256,
        //    string fileName, string fileVersion,
        //    long fileSize, string filePath = "",
        //    FileType )



    }
}
