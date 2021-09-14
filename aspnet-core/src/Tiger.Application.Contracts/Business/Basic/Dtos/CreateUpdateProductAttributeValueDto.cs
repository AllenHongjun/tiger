using System;
using System.ComponentModel;
namespace Tiger.Business.Basic.Dtos
{
    [Serializable]
    public class CreateUpdateProductAttributeValueDto
    {
        public Guid ProductId { get; set; }


        public Guid ProductAttributeId { get; set; }


        public string Value { get; set; }
    }
}