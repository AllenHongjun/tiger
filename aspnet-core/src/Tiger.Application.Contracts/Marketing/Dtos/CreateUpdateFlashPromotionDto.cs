using System;

namespace Tiger.Marketing.Dtos
{
    [Serializable]
    public class CreateUpdateFlashPromotionDto
    {
        public string Title { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int Status { get; set; }

    }
}