using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Tiger.Module.System.Platform.Localization;
using Volo.Abp.Validation;

namespace Tiger.Module.System.Platform.Datas.Dtos
{   
    /// <summary>
    /// 字典项目
    /// </summary>
    public class DataItemCreateOrUpdateDto : IValidatableObject
    {   
        /// <summary>
        /// 显示名称
        /// </summary>
        [Required]
        [DynamicStringLength(typeof(DataItemConsts), nameof(DataItemConsts.MaxDisplayNameLength))]
        public string DisplayName { get; set; }

        /// <summary>
        /// 默认值
        /// </summary>
        [DynamicStringLength(typeof(DataItemConsts), nameof(DataItemConsts.MaxValueLength))]
        public string DefaultValue { get; set; }

        /// <summary>
        /// 字典描述
        /// </summary>
        [DynamicStringLength(typeof(DataItemConsts), nameof(DataItemConsts.MaxDescriptionLength))]
        public string Description { get; set; }

        /// <summary>
        /// 允许为空
        /// </summary>
        public bool AllowBeNull { get; set; }

        /// <summary>
        /// 数据类型 
        /// </summary>
        public ValueType ValueType { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!AllowBeNull && DefaultValue.IsNullOrWhiteSpace())
            {
                var localizer = validationContext.GetRequiredService<IStringLocalizer<PlatformResource>>();
                yield return new ValidationResult(
                    localizer["The {0} field is required.", localizer["DisplayName:Value"]],
                    new string[] { nameof(DefaultValue) });
            }
        }
    }
}
