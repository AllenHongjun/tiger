using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Tiger.Basic.Products
{
    public enum ProductPublishStatus
    {   
        [Description("上架")]
        On = 1,
        [Description("下架")]
        Off = 0
    }
}
