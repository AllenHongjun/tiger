using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Volo.Abp;

namespace Tiger.Module.OssManagement.Dto
{
    public class CreateOssObjectRequest
    {
        public string Bucket { get; }
        public string Path { get; }
        public string Object { get; }
        public bool Overwrite { get; set; }
        public Stream Content { get; private set; }
        public TimeSpan? ExpirationTime { get; }
        public CreateOssObjectRequest(
            [NotNull] string bucket,
            [NotNull] string @object,
            [CanBeNull] Stream content,
            [CanBeNull] string path = null,
            [CanBeNull] TimeSpan? expirationTime = null)
        {
            Bucket = Check.NotNullOrWhiteSpace(bucket, nameof(bucket));
            Object = Check.NotNullOrWhiteSpace(@object, nameof(@object));

            Path = path;
            Content = content;
            ExpirationTime = expirationTime;
        }

        public void SetContent(Stream content)
        {
            Content = content;
        }
    }
}
