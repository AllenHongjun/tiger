using System;
using Volo.Abp.Application.Dtos;

namespace Tiger.Basic.Dtos
{
    [Serializable]
    public class SupplyDto : FullAuditedEntityDto<Guid>
    {
        public string Name { get; set; }

        public string AttentionTo { get; set; }

        public string Phone { get; set; }

        public string PostCode { get; set; }

        public string Province { get; set; }

        public string City { get; set; }

        public string Reginon { get; set; }

        public string Address { get; set; }

        public int Status { get; set; }

        public Guid? WarehouseId { get; set; }

    }
}