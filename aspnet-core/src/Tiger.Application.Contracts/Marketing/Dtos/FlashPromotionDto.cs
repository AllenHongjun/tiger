using System;
using Volo.Abp.Application.Dtos;

namespace Tiger.Marketing.Dtos
{
    [Serializable]
    public class FlashPromotionDto : AuditedEntityDto<Guid>
    {
        public string Title { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int Status { get; set; }

    }
}