using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.BlobStoring;

namespace Tiger.BlobDemo
{
    /// <summary>
    /// 类型化BLOB容器系统是一种在应用程序中创建和管理多个容器的方法;
    /// </summary>
    [BlobContainerName("my-custom-blob")]
    public class MyBlobContainer
    {
    }
}
