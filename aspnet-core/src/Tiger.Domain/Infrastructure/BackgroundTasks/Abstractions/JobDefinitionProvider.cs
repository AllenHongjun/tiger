using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.DependencyInjection;

namespace Tiger.Infrastructure.BackgroundTasks.Abstractions
{
    public abstract class JobDefinitionProvider : IJobDefinitionProvider, ITransientDependency
    {
        public abstract void Define(IJobDefinitionContext context);
    }
}
