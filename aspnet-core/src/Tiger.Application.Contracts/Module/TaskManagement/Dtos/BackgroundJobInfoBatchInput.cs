using System;
using System.Collections.Generic;

namespace Tiger.Module.TaskManagement.Dtos;

public class BackgroundJobInfoBatchInput
{
    public List<string> JobIds { get; set; } = new List<string>();
}
