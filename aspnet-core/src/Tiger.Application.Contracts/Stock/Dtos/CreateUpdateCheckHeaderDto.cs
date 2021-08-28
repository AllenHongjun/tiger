using System;

namespace Tiger.Stock.Dtos
{
    [Serializable]
    public class CreateUpdateCheckHeaderDto
    {
        public string WarehouseCode { get; set; }

        public int CheckType { get; set; }

        public int Status { get; set; }

        public string Note { get; set; }

        public string CloseBy { get; set; }

        public DateTime CloseAt { get; set; }

        public string ProcessStamp { get; set; }

    }
}