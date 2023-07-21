using System;
using System.ComponentModel.DataAnnotations;
using Tiger.Module.System.Platform.Routes;
using Volo.Abp.Validation;

namespace Tiger.Module.System.Platform.Menus.Dto
{
    public class MenuGetByUserInput
    {
        [Required]
        public Guid UserId { get; set; }

        public string[] Roles { get; set; } = new string[0];

        [DynamicStringLength(typeof(LayoutConsts), nameof(LayoutConsts.MaxFrameworkLength))]
        public string Framework { get; set; }
    }
}
