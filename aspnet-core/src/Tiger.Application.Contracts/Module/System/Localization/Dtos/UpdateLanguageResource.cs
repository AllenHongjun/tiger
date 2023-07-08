using System;

namespace Tiger.Module.System.Localization.Dtos;

[Serializable]
public class UpdateLanguageResource
{
    public bool Enable { get; set; }

    public string CultureName { get; set; }

    public string UiCultureName { get; set; }

    public string DisplayName { get; set; }

    public string FlagIcon { get; set; }
}