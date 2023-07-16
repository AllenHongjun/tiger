using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Tiger.Module.OssManagement.Dtos;

namespace Tiger.Module.OssManagement
{
    public interface IFileUploader
    {
        Task UploadAsync(UploadFileChunkInput input, CancellationToken cancellationToken = default);
    }
}
