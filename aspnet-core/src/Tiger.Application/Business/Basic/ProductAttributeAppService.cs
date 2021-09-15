using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tiger.Basic.ProductAttributes;
using Tiger.Business.Basic.Dtos;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Tiger.Basic
{
    [RemoteService(false)]
    public class ProductAttributeAppService
        :CrudAppService<
            ProductAttribute, //The  entity
            ProductAttributeDto,
            Guid, //Primary key 
            GetProductAttributeListDto,
            CreateUpdateProductAttributeDto, //Used for paging/sorting
            CreateUpdateProductAttributeDto>, //Used to create/update
        IProductAttributeAppService
    {
        private readonly IRepository<ProductAttribute, Guid> _repository; 
        public ProductAttributeAppService(IRepository<ProductAttribute, Guid> repository) : base(repository)
        {
            _repository = repository;
        }




        /*
         [
          {
            "name": "颜色",
            "item": [
              "黑"
            ]
          },
          {
            "name": "内存",
            "item": [
              "16G",
              "32G"
            ]
          },
          {
            "name": "尺寸",
            "item": [
              "4.7寸",
              "6.3寸"
            ]
          },
          {
            "name": "像素",
            "item": [
              "4000万"
            ]
          }
        ]
         */
        /// <summary>
        /// 根据属性类型查询所有的属性和值 返回指定类型的规格属性
        /// </summary>
        /// <param name="productAttributeTypeId"></param>
        /// <returns></returns>
        public ProductAttributeResultDto[] GetProductAttributeDtoByTypeId(Guid productAttributeTypeId)
        {
            var productAttributes =  _repository.WhereIf(true,x => x.ProductAttributeTypeId == productAttributeTypeId);
            List<ProductAttributeResultDto> productAttributeResults = new List<ProductAttributeResultDto>();
            foreach (var productAttribute in productAttributes)
            {
                ProductAttributeResultDto productAttributeResultDto = new ProductAttributeResultDto();
                productAttributeResultDto.Id = productAttribute.Id;
                productAttributeResultDto.Name = productAttribute.Name;
                productAttributeResultDto.Item = productAttribute.InputList.Split(',').ToArray();
                productAttributeResults.Add(productAttributeResultDto);
            }
            
            return productAttributeResults.ToArray();
        }
    }
}
