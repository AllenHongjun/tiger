using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Validation;

namespace Tiger.Module.System.Platform.Datas.Dtos
{
    public class GetDataByNameInput
    {
        [Required]
        [DynamicStringLength(typeof(DataConsts), nameof(DataConsts.MaxNameLength))]
        public string Name { get; set; }
    }
}
