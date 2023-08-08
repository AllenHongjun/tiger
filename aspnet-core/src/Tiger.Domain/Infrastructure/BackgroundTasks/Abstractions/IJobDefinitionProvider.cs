using System;
using System.Collections.Generic;
using System.Text;

namespace Tiger.Infrastructure.BackgroundTasks.Abstractions
{
    public interface IJobDefinitionProvider
    {
        void Define(IJobDefinitionContext context);
    }
}
