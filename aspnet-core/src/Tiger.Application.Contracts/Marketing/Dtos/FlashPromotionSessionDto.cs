using System;
using Volo.Abp.Application.Dtos;

namespace Tiger.Marketing.Dtos
{
    [Serializable]
    public class FlashPromotionSessionDto : AuditedEntityDto<Guid>
    {
        public string Name { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public int Status { get; set; }

        public Guid FlashPromotionId { get; set; }

        
    }
}