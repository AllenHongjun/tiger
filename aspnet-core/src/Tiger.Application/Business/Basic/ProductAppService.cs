using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tiger.Basic.Products;
using Tiger.Business.Basic;
using Tiger.Business.Basic.Dtos;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Tiger.Basic
{
    [RemoteService(false)]
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
        private readonly IRepository<ProductAttributeValue, Guid> _productAttributeValueRepository;
        private readonly ISkuRepository _skuRepository;
        public ProductAppService(
            IRepository<Product, Guid> repository,
            IRepository<ProductAttributeValue, Guid> productAttributeValueRepository,
            ISkuRepository skuRepository,
            IProductRepository productRepository)
            : base(repository)
        {
            _productRepository = productRepository;
            _productAttributeValueRepository = productAttributeValueRepository;
            _skuRepository = skuRepository;
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


        // TODO: 保存商品关联的规格  和商品绑定的规格属性

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


        public override async Task<ProductDto> CreateAsync(CreateUpdateProductDto input)
        {
            //var productAttributeResultDtos = input.productAttributeResultDtos;
            //foreach (var item in productAttributeResultDtos)
            //{
            //    var productAttributeResultDto = new ProductAttributeValueDto();
            //    productAttributeResultDto.Value = item.Item.ToString();
            //    productAttributeResultDto.ProductAttributeId = new Guid();
            //    input.ProductAttributeValues.Add(productAttributeResultDto);
            //}
            var product = await base.CreateAsync(input);
            foreach (var item in input.productAttributeResultDtos)
            {
                var value = String.Join(',', item.Item);
                var productAttributeValue = new ProductAttributeValue(GuidGenerator.Create(), product.Id,item.Id, value);
                await _productAttributeValueRepository.InsertAsync(productAttributeValue, true);
            }

            foreach (var item in input.SkuItemDtos)
            {
                var sku = new Sku(item.Code, item.Price, item.Stock, item.Sku, product.Id);
                await _skuRepository.InsertAsync(sku, true);
            }
            return product;
        }

        public override async Task<ProductDto> UpdateAsync(Guid id, CreateUpdateProductDto input)
        {
            var product = await base.UpdateAsync(id, input);
            // 更新商品属性
            var productAttributeValues = _productAttributeValueRepository.Where(x => x.ProductId == id).ToList();
            foreach (var item in productAttributeValues)
            {
                await _productAttributeValueRepository.DeleteAsync(item.Id, true);
            }

            foreach (var item in input.productAttributeResultDtos)
            {
                var value = String.Join(',', item.Item);
                var productAttributeValue = new ProductAttributeValue(GuidGenerator.Create(), product.Id, item.Id, value);
                await _productAttributeValueRepository.InsertAsync(productAttributeValue, true);
            }

            //更新商品sku
            var skus = _skuRepository.Where(x => x.ProductId == id).ToList();
            foreach (var item in skus)
            {
                await _skuRepository.DeleteAsync(item.Id, true);
            }

            foreach (var item in input.SkuItemDtos)
            {
                var sku = new Sku(item.Code, item.Price, item.Stock, item.Sku, product.Id);
                await _skuRepository.InsertAsync(sku, true);
            }

            return product;
        }

        public override async Task<ProductDto> GetAsync(Guid id)
        {
            var product = await base.GetAsync(id);
            var productAttributeValues = _productAttributeValueRepository
                .WithDetails(x => x.ProductAttribute)
                .Where(x => x.ProductId == id)
                .ToList();
            List<ProductAttributeResultDto> productAttributeResultDtos = new List<ProductAttributeResultDto>();
            foreach (var item in productAttributeValues)
            {
                ProductAttributeResultDto productAttributeResultDto = new ProductAttributeResultDto();
                productAttributeResultDto.Id = item.Id;
                productAttributeResultDto.Name = item.ProductAttribute.Name;
                productAttributeResultDto.Item = item.Value.Split(',');
                productAttributeResultDtos.AddIfNotContains(productAttributeResultDto);
            }
            product.ProductAttributeResultDtos = productAttributeResultDtos.ToArray();

            var skus = _skuRepository.Where(x => x.ProductId == id).ToList();
            List<SkuItemDto> skuItemDtos = new List<SkuItemDto>();


            foreach (var item in skus)
            {
                SkuItemDto skuItemDto = new SkuItemDto();
                skuItemDto.Code = item.SkuCode;
                skuItemDto.Price = item.Price;
                skuItemDto.Stock = item.Stock;
                skuItemDto.Sku = item.SPData;
                skuItemDtos.AddIfNotContains(skuItemDto);
            }
            product.SkuItemDtos = skuItemDtos.ToArray();
            
            return product;
        }

        // TODO: 返回和保存商品管理的sku值

        /*
         [
              {
                "sku": "黑,16G,4.7寸,4000万",
                "price": "",
                "stock": ""
              },
              {
                "sku": "黑,16G,6.3寸,4000万",
                "price": "",
                "stock": ""
              },
              {
                "sku": "黑,32G,4.7寸,4000万",
                "price": "",
                "stock": ""
              },
              {
                "sku": "黑,32G,6.3寸,4000万",
                "price": "",
                "stock": ""
              }
            ]
         */

    }
}
