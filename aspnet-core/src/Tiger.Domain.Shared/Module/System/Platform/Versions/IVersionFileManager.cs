using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Tiger.Module.System.Platform.Versions
{
    /// <summary>
    /// 版本文件管理
    /// </summary>
    public interface IVersionFileManager
    {   
        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="version"></param>
        /// <param name="filePath"></param>
        /// <param name="fileName"></param>
        /// <param name="fileVersion"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        Task<string> SaveFileAsync(
            string version,
            string filePath,
            string fileName,
            string fileVersion,
            byte[] data);

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="platformType"></param>
        /// <param name="version"></param>
        /// <param name="filePath"></param>
        /// <param name="fileName"></param>
        /// <param name="fileVersion"></param>
        /// <returns></returns>
        Task<Stream> DownloadFileAsync(
            PlatformType platformType,
            string version,
            string filePath,
            string fileName,
            string fileVersion);
    }
}
