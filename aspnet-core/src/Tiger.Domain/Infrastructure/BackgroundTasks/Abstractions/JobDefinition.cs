using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Localization;

namespace Tiger.Infrastructure.BackgroundTasks.Abstractions
{
    public class JobDefinition
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// 作业类型
        /// </summary>
        public Type JobType { get; }
        /// <summary>
        /// 显示名称
        /// </summary>
        public ILocalizableString DisplayName { get; }
        /// <summary>
        /// 描述
        /// </summary>
        public ILocalizableString Description { get; }
        /// <summary>
        /// 参数列表
        /// </summary>
        public IReadOnlyList<JobDefinitionParamter> Paramters { get; }
        public JobDefinition(
            [NotNull] string name,
            [NotNull] Type jobType,
            [NotNull] ILocalizableString displayName,
            [CanBeNull] IReadOnlyList<JobDefinitionParamter> paramters = null,
            [CanBeNull] ILocalizableString description = null)
        {
            Name = name;
            JobType = jobType;
            DisplayName = displayName;
            Description = description;
            Paramters = paramters ?? new JobDefinitionParamter[0];
        }
    }
}
