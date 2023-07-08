using System;

namespace Tiger.Module.System.Localization.Dtos;

[Serializable]
public class CreateResource
{
    public bool Enable { get; set; }

    public bool Name { get; set; }

    public string DisplayName { get; set; }

    public string Description { get; set; }
}