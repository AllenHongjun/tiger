using System;

namespace Tiger.Module.System.TextTemplate.Dtos;

[Serializable]
public class CreateTextTemplateDto
{
    public string Name { get; set; }

    public string DisplayName { get; set; }

    public string Content { get; set; }

    public string Culture { get; set; }
}