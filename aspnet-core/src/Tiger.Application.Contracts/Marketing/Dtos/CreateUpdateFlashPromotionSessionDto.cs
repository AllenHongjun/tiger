using System;

namespace Tiger.Marketing.Dtos
{
    [Serializable]
    public class CreateUpdateFlashPromotionSessionDto
    {
        public string Name { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public int Status { get; set; }

        public Guid FlashPromotionId { get; set; }

        
    }
}