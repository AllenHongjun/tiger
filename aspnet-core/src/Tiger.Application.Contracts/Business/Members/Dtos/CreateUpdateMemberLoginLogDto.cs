using System;

namespace Tiger.Business.Members.Dtos
{
    [Serializable]
    public class CreateUpdateMemberLoginLogDto
    {
        public string IP { get; set; }

        public string City { get; set; }

        public int LoginType { get; set; }

        public string Province { get; set; }

        public Guid MemberId { get; set; }


    }
}