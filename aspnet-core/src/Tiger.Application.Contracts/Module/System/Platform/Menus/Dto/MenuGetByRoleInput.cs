using System.ComponentModel.DataAnnotations;
using Tiger.Module.System.Platform.Routes;
using Volo.Abp.Validation;

namespace Tiger.Module.System.Platform.Menus.Dto
{
    public class MenuGetByRoleInput
    {
        [Required]
        [StringLength(80)]
        public string Role { get; set; }

        [DynamicStringLength(typeof(LayoutConsts), nameof(LayoutConsts.MaxFrameworkLength))]
        public string Framework { get; set; }
    }
}
