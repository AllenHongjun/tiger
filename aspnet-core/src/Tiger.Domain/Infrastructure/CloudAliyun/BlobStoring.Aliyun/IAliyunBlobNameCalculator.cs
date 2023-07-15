using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.BlobStoring;

namespace Tiger.Infrastructure.CloudAliyun.BlobStoring.Aliyun
{
    public interface IAliyunBlobNameCalculator
    {
        string Calculate(BlobProviderArgs args);
    }
}
