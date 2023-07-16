﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Tiger.Module.OssManagement.Dtos
{
    public class OssContainerDto
    {
        public string Name { get; set; }
        public long Size { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public IDictionary<string, string> Metadata { get; set; }
    }
}
