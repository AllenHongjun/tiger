using System.Collections.Generic;

namespace Tiger.Module.TaskManagement.Dtos;

public class BackgroundJobDefinitionDto
{
    public string Name { get; set; }

    public string DisplayName { get; set; }

    public string Description { get; set; }

    public List<BackgroundJobParamterDto> Paramters { get; set; } = new List<BackgroundJobParamterDto>();
}
