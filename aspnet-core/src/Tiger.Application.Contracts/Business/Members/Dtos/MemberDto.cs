using System;
using Volo.Abp.Application.Dtos;

namespace Tiger.Business.Members.Dtos
{
    [Serializable]
    public class MemberDto : FullAuditedEntityDto<Guid>
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string NickName { get; set; }

        public string Phone { get; set; }

        public int Status { get; set; }

        public string Icon { get; set; }

        public int Gender { get; set; }

        public DateTime BirthDay { get; set; }

        public string City { get; set; }

        public string Job { get; set; }

        public int SourceType { get; set; }

        public int Integration { get; set; }

        public int Growth { get; set; }

        public int LuckeyCount { get; set; }

        public int HistoryIntegration { get; set; }

        public Guid MemberLevelId { get; set; }


    }
}