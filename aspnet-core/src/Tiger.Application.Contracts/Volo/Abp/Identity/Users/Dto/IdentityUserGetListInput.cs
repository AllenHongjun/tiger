using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Tiger.Volo.Abp.Identity.Users.Dto
{
    public class IdentityUserGetListInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public Guid? RoleId { get; set; }

        public Guid? OrganizationUnitId { get; set; }

        public string UserName { get; set; }

        public string PhoneNumber { get; set; }

        public string Name { get; set; }

        public bool? IsLockedOut { get; set; }

        public bool? NotActive { get; set; }    

        public bool? EmailConfirmed { get; set; }

        public bool? IsExternal { get; set; }   

        public DateTime? MinCreationTime { get; set; }

        public DateTime? MaxCreationTime { get; set;}

        public DateTime? MinModifitionTime { get; set; }

        public DateTime? MaxModifitionTime { get; set;}
    }
}
