using System;
using System.Collections.Generic;
using System.Text;
using Tiger.Infrastructure.BackgroundTasks.Abstractions;
using Volo.Abp.DependencyInjection;

namespace Tiger.Infrastructure.BackgroundTasks
{
    public abstract class JobDefinitionProvider : IJobDefinitionProvider, ITransientDependency
    {
        public abstract void Define(IJobDefinitionContext context);
    }
}
