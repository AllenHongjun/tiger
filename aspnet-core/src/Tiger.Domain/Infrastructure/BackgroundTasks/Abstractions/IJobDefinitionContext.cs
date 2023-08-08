using System;
using System.Collections.Generic;
using System.Text;

namespace Tiger.Infrastructure.BackgroundTasks.Abstractions
{
    public interface IJobDefinitionContext
    {
        JobDefinition GetOrNull(string name);

        IReadOnlyList<JobDefinition> GetAll();

        void Add(params JobDefinition[] definitions);
    }
}
