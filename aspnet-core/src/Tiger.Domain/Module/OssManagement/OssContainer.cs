using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Tiger.Module.OssManagement
{
    /// <summary>
    /// 描述一个容器的状态信息
    /// </summary>
    public class OssContainer
    {
        public string Name { get; }
        public long Size { get; }
        public DateTime CreationDate { get; }
        public DateTime? LastModifiedDate { get; }
        public IDictionary<string, string> Metadata { get; }

    }
}
