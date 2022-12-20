using System;
using System.ComponentModel.DataAnnotations;

namespace Tiger.Volo.Abp.Identity.Users.Dto
{
    public class IdentityUserOrganizationUnitUpdateDto
    {
        [Required]
        public Guid[] OrganizationUnitIds { get; set; }
    }
}
