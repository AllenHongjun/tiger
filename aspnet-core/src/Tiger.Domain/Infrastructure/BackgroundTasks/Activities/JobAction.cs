using System.Collections.Generic;

namespace Tiger.Infrastructure.BackgroundTasks.Activities;

public class JobAction
{
    public string Name { get; set; }
    public Dictionary<string, object> Paramters { get; set; }
}
