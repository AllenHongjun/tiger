using System;
using System.Collections.Generic;
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
            PagedAndSortedResultRequestDto, //Used for paging/sorting
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
        public async Task<PagedResultDto<ProductDto>> GetListV2Async(GetProductListDto input)
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

            //var totalCount = await AsyncExecuter.CountAsync<Product>(
            //    _productRepository.WhereIf(
            //        !input.Filter.IsNullOrWhiteSpace(),
            //        product => product.Name.Contains(input.Filter) 
            //    )
            //);

            var totalCount = 10;

            return new PagedResultDto<ProductDto>(
                totalCount,
                ObjectMapper.Map<List<Product>, List<ProductDto>>(products)
            );
        }
    }
}
