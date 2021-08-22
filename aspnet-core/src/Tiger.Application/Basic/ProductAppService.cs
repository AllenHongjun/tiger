using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tiger.Basic.Products;
using Tiger.Business.Basic;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Tiger.Basic
{
    
    public class ProductAppService
    :
        CrudAppService<
            Product, //The  entity
            ProductDto,
            Guid, //Primary key 
            GetProductListDto,
            CreateUpdateProductDto, //Used for paging/sorting
            CreateUpdateProductDto>, //Used to create/update
        IProductAppService 
    {
        private IProductRepository _productRepository;
        public ProductAppService(IRepository<Product, Guid> repository, IProductRepository productRepository)
            : base(repository)
        {
            _productRepository = productRepository;
        }



        /// <summary>
        /// 获取产品列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
      
        public override async Task<PagedResultDto<ProductDto>> GetListAsync(GetProductListDto input)
        {
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(Product.Sort);
            }

            var products = await _productRepository.GetListAsync(
                input.SkipCount,
                input.MaxResultCount,
                input.Sorting,
                input.Filter
            );

            var totalCount = _productRepository.Where(
                    product => String.IsNullOrEmpty(input.Filter) || product.Name.Contains(input.Filter)
                ).Count();

            return new PagedResultDto<ProductDto>(
                totalCount,
                ObjectMapper.Map<List<Product>, List<ProductDto>>(products)
            );
        }


       
    }
}
