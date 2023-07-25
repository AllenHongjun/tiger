using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp;

namespace Tiger.Module.OssManagement.Dto
{
    public class GetOssObjectRequest
    {
        public string Bucket { get; }
        public string Path { get; }

        /// <summary>
        /// 在各语言SDK中，ObjectKey、Key以及ObjectName是同一概念，均表示对Object执行相关操作时需要填写的Object名称。例如向某一存储空间上传Object时，ObjectKey表示上传的Object所在存储空间的完整名称，即包含文件后缀在内的完整路径，如填写为abc/efg/123.jpg。
        /// </summary>
        public string Object { get; }
        public bool MD5 { get; set; }
        /// <summary>
        /// 需要处理文件的参数
        /// </summary>
        public string Process { get; }

        public bool CreatePathIsNotExists { get; set; } = false;

        public GetOssObjectRequest(
            [NotNull] string bucket,
            [NotNull] string @object,
            [CanBeNull] string path = "",
            [CanBeNull] string process = "")
        {
            Check.NotNullOrWhiteSpace(bucket, nameof(bucket));
            Check.NotNullOrWhiteSpace(@object, nameof(@object));

            Bucket = bucket;
            Object = @object;
            Path = path;
            Process = process;
        }
    }
}
