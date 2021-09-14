using System;
using Volo.Abp.Application.Dtos;

namespace Tiger.Business.Basic.Dtos
{
    [Serializable]
    public class ProductAttributeValueDto : EntityDto<Guid>
    {
        public Guid ProductId { get; set; }


        public Guid ProductAttributeId { get; set; }


        public string Value { get; set; }
    }
}