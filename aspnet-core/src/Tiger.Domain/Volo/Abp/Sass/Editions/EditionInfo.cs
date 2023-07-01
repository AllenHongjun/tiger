using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp;

namespace Tiger.Volo.Abp.Sass.Editions
{   
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// 此类基于多租户 需要重新定义
    /// </remarks>
    [Serializable]
    public class EditionInfo
    {
        public Guid Id { get; set; }

        public string DisplayName { get; set; }

        public EditionInfo()
        {
        }

        public EditionInfo(
            Guid id,
            [NotNull] string displayName)
        {
            Check.NotNull(displayName, nameof(displayName));

            Id = id;
            DisplayName = displayName;
        }
    }
}
