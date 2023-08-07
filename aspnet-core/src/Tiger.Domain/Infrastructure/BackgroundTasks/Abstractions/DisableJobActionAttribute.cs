using System;
using System.Collections.Generic;
using System.Text;

namespace Tiger.Infrastructure.BackgroundTasks.Abstractions
{
    [AttributeUsage(AttributeTargets.Class)]
    public class DisableJobActionAttribute:Attribute
    {
    }
}
