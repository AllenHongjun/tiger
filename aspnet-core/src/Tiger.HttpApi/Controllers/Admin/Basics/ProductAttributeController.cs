using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tiger.Basic.ProductAttributes;
using Tiger.Basic.Products;
using Tiger.Business.Basic.Dtos;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Tiger.Controllers.Admin.Basics
{

    /// <summary>
    /// 商品属性
    /// </summary>
    [RemoteService(Name = "ProductAttribute")]
    [Area("Basics")]
    [ControllerName("ProductAttribute")]
    [Route("api/basic/productAttribute")]
    [ApiExplorerSettings(GroupName = "admin-basic")]
    public class ProductAttributeController : TigerController
    {
        private readonly IProductAttributeAppService _productAttributeAppService;

        public ProductAttributeController(IProductAttributeAppService productAppService)
        {
            _productAttributeAppService = productAppService;
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ProductAttributeDto> CreateAsync(CreateUpdateProductAttributeDto input)
        {
            return await _productAttributeAppService.CreateAsync(input);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        public  Task DeleteAsync(Guid id)
        {
            return  _productAttributeAppService.DeleteAsync(id);
        }

        /// <summary>
        /// 获取明细
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<ProductAttributeDto> GetAsync(Guid id)
        {
            return await _productAttributeAppService.GetAsync(id);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<PagedResultDto<ProductAttributeDto>> GetListAsync(GetProductAttributeListDto input)
        {
            return await _productAttributeAppService.GetListAsync(input);
        }

        /// <summary>
        /// 根据类型获取列表
        /// </summary>
        /// <param name="productAttributeTypeId">属性类型id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("getListByType")]
        public  ProductAttributeResultDto[] GetListByTypeAsync(Guid productAttributeTypeId)
        {
            return  _productAttributeAppService.GetProductAttributeDtoByTypeId(productAttributeTypeId);
        }



        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        public async Task<ProductAttributeDto> UpdateAsync(Guid id, CreateUpdateProductAttributeDto input)
        {
            return await _productAttributeAppService.UpdateAsync(id, input);
        }
    }
}
