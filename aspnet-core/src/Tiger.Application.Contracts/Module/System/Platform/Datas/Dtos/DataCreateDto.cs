using System;
using System.Collections.Generic;
using System.Text;

namespace Tiger.Module.System.Platform.Datas.Dtos
{
    public class DataCreateDto:CreateUpdateDataDto
    {
        public Guid? ParentId { get;set; }
    }
}
