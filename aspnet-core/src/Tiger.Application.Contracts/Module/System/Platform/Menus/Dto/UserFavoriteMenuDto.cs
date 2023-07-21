using System;
using Volo.Abp.Application.Dtos;

namespace Tiger.Module.System.Platform.Menus.Dto;

public class UserFavoriteMenuDto : AuditedEntityDto<Guid>
{
    public Guid MenuId { get; set; }

    public Guid UserId { get; set; }

    public string AliasName { get; set; }

    public string Color { get; set; }

    public string Framework { get; set; }

    public string Name { get; set; }

    public string DisplayName { get; set; }

    public string Path { get; set; }

    public string Icon { get; set; }
}
