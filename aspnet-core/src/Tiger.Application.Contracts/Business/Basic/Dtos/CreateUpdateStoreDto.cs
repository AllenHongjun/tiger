using System;

namespace Tiger.Basic.Dtos
{
    [Serializable]
    public class CreateUpdateStoreDto
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public int Status { get; set; }

        public string Apolygons { get; set; }

        public string Lat { get; set; }

        public string Lng { get; set; }

        public Guid? WarehouseId { get; set; }

        //public Warehouse Warehouse { get; set; }

    }
}