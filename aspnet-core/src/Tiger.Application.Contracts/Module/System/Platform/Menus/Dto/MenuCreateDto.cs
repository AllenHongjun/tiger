using System;
using System.ComponentModel.DataAnnotations;

namespace Tiger.Module.System.Platform.Menus.Dto
{
    public class MenuCreateDto : MenuCreateOrUpdateDto
    {
        [Required]
        public Guid LayoutId { get; set; }
    }
}
