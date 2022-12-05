using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Tiger.Volo.Abp.Identity.IdentitySecurityLogs.Dto
{
    public class GetIdentitySecurityLogInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public DateTime? startTime { get; set; }
        public DateTime? endTime  { get; set; }


        public string applicationName { get; set; }
        public string identity { get; set; }
        public string action { get; set; }
        public string userName { get; set; }
        public string clientId { get; set; }
        public string correlationId { get; set; }
        public bool includeDetails { get; set; }

    }
}
