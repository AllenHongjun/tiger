using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Tiger.Module.OssManagement
{
    /// <summary>
    /// 描述一个对象的状态信息
    /// </summary>
    public class OssObject
    {
        private Stream _content;

        public bool IsFolder { get; }

        public string Name { get; }

        public string FullName { get; }

        public string Prefix { get; }

        public string MD5 { get; }

        public long Size { get; }

        public Stream Content => _content;
        public DateTime? CreationDate { get; }
        public DateTime? LastModifiedDate { get; }
        public IDictionary<string, string> Metadata { get; }
    }
}
