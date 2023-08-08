using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tiger.Infrastructure.BackgroundTasks.Abstractions
{
    public interface IJobDefinitionManager
    {
        [NotNull]
        JobDefinition Get([NotNull] string name);

        IReadOnlyList<JobDefinition> GetAll();

        JobDefinition GetOrNull(string name);
    }
}
