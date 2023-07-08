using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Tiger.Module.System.Cache
{
    public class CacheKeyInput
    {
        [Required]
        public string Key { get; set; }
    }
}
