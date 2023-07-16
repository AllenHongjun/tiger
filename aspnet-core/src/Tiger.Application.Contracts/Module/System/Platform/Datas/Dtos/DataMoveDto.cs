using System;
using System.Collections.Generic;
using System.Text;

namespace Tiger.Module.System.Platform.Datas.Dtos
{   
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// 有些参数很少的几个 就不要创建实体类
    /// </remarks>
    public class DataMoveDto
    {
        public Guid? ParentId { get; set; }
    }
}
