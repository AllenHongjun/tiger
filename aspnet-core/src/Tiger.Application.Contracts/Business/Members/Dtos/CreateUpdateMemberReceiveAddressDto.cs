using System;

namespace Tiger.Business.Members.Dtos
{
    [Serializable]
    public class CreateUpdateMemberReceiveAddressDto
    {
        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public int DefaultStatus { get; set; }

        public string PostCode { get; set; }

        public string Province { get; set; }

        public string City { get; set; }

        public string Reginon { get; set; }

        public string Address { get; set; }

        public string Lat { get; set; }

        public string Lon { get; set; }

        public Guid MemberId { get; set; }


    }
}