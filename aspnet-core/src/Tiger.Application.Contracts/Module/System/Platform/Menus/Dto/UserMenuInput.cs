using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tiger.Module.System.Platform.Menus.Dto
{
    public class UserMenuInput
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public List<Guid> MenuIds { get; set; } = new List<Guid>();
    }
}
