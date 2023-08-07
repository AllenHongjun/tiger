using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Localization;

namespace Tiger.Infrastructure.BackgroundTasks.Abstractions
{
    public class JobDefinitionParamter
    {
        public string Name { get; }

        public bool Required { get; }

        public ILocalizableString DisplayName { get; }

        public ILocalizableString Description { get; }

        public JobDefinitionParamter(
            [NotNull] string name,
            [NotNull] ILocalizableString displayName,
            [CanBeNull] ILocalizableString description = null,
            bool required = false)
        {
            Name = name;
            Required = required;
            DisplayName = displayName;
            Description = description;
        }
    }
}
