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

        /// <summary>
        /// 是否文件夹
        /// </summary>
        public bool IsFolder { get; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// 全称
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// 前缀
        /// </summary>
        public string Prefix { get; }

        /// <summary>
        /// MD5值
        /// </summary>
        public string MD5 { get; }

        /// <summary>
        /// 文件大小
        /// </summary>
        public long Size { get; }

        public Stream Content => _content;

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreationDate { get; }


        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? LastModifiedDate { get; }
        public IDictionary<string, string> Metadata { get; }

        public OssObject(
           string name,
           string prefix,
           string md5,
           DateTime? creationDate = null,
           long size = 0,
           DateTime? lastModifiedDate = null,
           IDictionary<string, string> metadata = null,
           bool isFolder = false)
        {
            Name = name;
            Prefix = prefix;
            MD5 = md5;
            CreationDate = creationDate;
            LastModifiedDate = lastModifiedDate;
            Size = size;
            IsFolder = isFolder;
            Metadata = metadata ?? new Dictionary<string, string>();
        }

        public void SetContent(Stream stream)
        {
            _content = stream;
            if (!_content.IsNullOrEmpty())
            {
                _content.Seek(0, SeekOrigin.Begin);
            }
        }
    }
}
