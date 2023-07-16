using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Validation;

namespace Tiger.Module.System.Platform.Datas.Dtos
{
    public class DataItemCreateDto:DataItemCreateOrUpdateDto
    {
        [Required]
        [DynamicStringLength(typeof(DataItemConsts), nameof(DataItemConsts.MaxNameLength))]
        public string Name { get; set; }
    }
}
