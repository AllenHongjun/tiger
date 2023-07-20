using System;
using System.ComponentModel.DataAnnotations;

namespace Tiger.Module.System.Platform.Menus.Dto;
public class UserFavoriteMenuRemoveInput
{
    [Required]
    public Guid MenuId { get; set; }
}
