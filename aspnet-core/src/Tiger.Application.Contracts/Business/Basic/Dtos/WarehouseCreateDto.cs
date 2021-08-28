using System;

namespace Tiger.Basic.Dtos
{
    [Serializable]
    public class WarehouseCreateDto
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public int Status { get; set; }

        public int Type { get; set; }

        public double Lat { get; set; }

        public double Lng { get; set; }

        public string Address { get; set; }

        public string District { get; set; }

        public string City { get; set; }

        public string Province { get; set; }

        public string PosttalCode { get; set; }

        public string Mobile { get; set; }

        public Guid OrgId { get; set; }
    }
}