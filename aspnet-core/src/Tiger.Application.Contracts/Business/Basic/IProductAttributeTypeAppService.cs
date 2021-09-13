using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Tiger.Basic.ProductAttributeTypes
{
    /// <summary>
    /// 产品属性
    /// </summary>
    public interface IProductAttributeTypeAppService:
        ICrudAppService<
            ProductAttributeTypeDto, // Used to show ProductAttributeTpye
            Guid,
            GetProductAttributeTypeListDto,
            CreateUpdateProductAttributeTypeDto,
            CreateUpdateProductAttributeTypeDto>
    {

        /// <summary>
        /// 获取所有规格（属性分类）
        /// </summary>
        /// <returns></returns>
        public Task<List<ProductAttributeTypeDto>> GetAllAsync();
    }
}
