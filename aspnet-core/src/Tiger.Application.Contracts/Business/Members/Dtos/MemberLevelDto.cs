using System;
using Volo.Abp.Application.Dtos;

namespace Tiger.Business.Members.Dtos
{
    [Serializable]
    public class MemberLevelDto : EntityDto<Guid>
    {
        public string Name { get; set; }

        public int GrowthPoint { get; set; }

        public decimal FreeFreightPoint { get; set; }

        public int CommentGrowthPoint { get; set; }

        public int PriviledgeFreeFreight { get; set; }

        public int PriviledgeSignIn { get; set; }

        public int PrivilegeComment { get; set; }

        public int PrivilegePromotin { get; set; }

        public int PrivilegeMemberPrice { get; set; }

        public int PrivilegeBirthDay { get; set; }

        public string Note { get; set; }

    }
}