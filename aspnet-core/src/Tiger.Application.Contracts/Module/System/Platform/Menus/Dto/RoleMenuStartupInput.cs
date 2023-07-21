using System.ComponentModel.DataAnnotations;

namespace Tiger.Module.System.Platform.Menus.Dto;

public class RoleMenuStartupInput
{
    [Required]
    [StringLength(80)]
    public string RoleName { get; set; }
}
