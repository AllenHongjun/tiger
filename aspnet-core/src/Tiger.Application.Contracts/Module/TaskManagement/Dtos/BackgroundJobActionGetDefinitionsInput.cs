using Tiger.Infrastructure.BackgroundTasks.Abstractions.Enum;

namespace Tiger.Module.TaskManagement.Dtos;

public class BackgroundJobActionGetDefinitionsInput
{
    public JobActionType? Type { get; set; }
}
