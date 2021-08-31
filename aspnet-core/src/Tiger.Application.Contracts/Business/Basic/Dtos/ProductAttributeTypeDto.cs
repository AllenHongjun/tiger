using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Tiger.Basic.ProductAttributeTypes
{
    public class ProductAttributeTypeDto:EntityDto<Guid>
    {
        //public Guid? TenantId { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// 属性数量
        /// </summary>
        public int AttributeCount { get; set; }

        /// <summary>
        /// 参数数量
        /// </summary>
        public int ParamCount { get; set; }


    }
}
