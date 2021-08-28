using System;
using Volo.Abp.Application.Dtos;

namespace Tiger.Business.Members.Dtos
{
    [Serializable]
    public class MemberLoginLogDto : FullAuditedEntityDto<Guid>
    {
        public string IP { get; set; }

        public string City { get; set; }

        public int LoginType { get; set; }

        public string Province { get; set; }

        public Guid MemberId { get; set; }


    }
}