using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tiger.Basic.ProductAttributeTypes;
using Tiger.Business.Basic;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;

namespace Tiger.Basic
{
    /// <summary>
    /// 商品属性分类
    /// </summary>
    [RemoteService(false)]
    public class ProductAttributeTypeAppService
        : CrudAppService<
            ProductAttributeType, //The  entity
            ProductAttributeTypeDto,
            Guid, //Primary key 
            GetProductAttributeTypeListDto,
            CreateUpdateProductAttributeTypeDto, //Used for paging/sorting
            CreateUpdateProductAttributeTypeDto>, //Used to create/update
        IProductAttributeTypeAppService
    {
        private readonly IProductAttributeTypeRepository _productAttributeTpyeRepository;
        public ProductAttributeTypeAppService(IProductAttributeTypeRepository productAttributeTpyeRepository) : base(productAttributeTpyeRepository)
        {
            _productAttributeTpyeRepository = productAttributeTpyeRepository;
        }

        public override Task<ProductAttributeTypeDto> CreateAsync(CreateUpdateProductAttributeTypeDto input)
        {
            return base.CreateAsync(input);
        }

        public override Task DeleteAsync(Guid id)
        {
            return base.DeleteAsync(id);
        }

        public override Task<PagedResultDto<ProductAttributeTypeDto>> GetListAsync(GetProductAttributeTypeListDto input)
        {
            return base.GetListAsync(input);
        }

        public override Task<ProductAttributeTypeDto> UpdateAsync(Guid id, CreateUpdateProductAttributeTypeDto input)
        {
            return base.UpdateAsync(id, input);
        }

        protected override Task DeleteByIdAsync(Guid id)
        {
            return base.DeleteByIdAsync(id);
        }

        protected override Task<ProductAttributeTypeDto> MapToGetOutputDtoAsync(ProductAttributeType entity)
        {
            return base.MapToGetOutputDtoAsync(entity);
        }

        // TODO: 获取所有的属性分类（规格）


        /// <summary>
        /// 获取所有规格（属性分类）
        /// </summary>
        /// <returns></returns>
        public async Task<List<ProductAttributeTypeDto>> GetAllAsync()
        {
            var productAttributeTpyes = await _productAttributeTpyeRepository.GetListAsync();
            return ObjectMapper.Map<List<ProductAttributeType>, List<ProductAttributeTypeDto>>(productAttributeTpyes);
        }


    }
}
