using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Tiger.Module.OssManagement.Dtos
{
    public class GetFileShareDto
    {
        public string Name { get; set; }
        public Stream Content { get; set; }
        public GetFileShareDto(
            string name,
            Stream content = null)
        {
            Name = name;
            Content = content ?? Stream.Null;
        }
    }
}
