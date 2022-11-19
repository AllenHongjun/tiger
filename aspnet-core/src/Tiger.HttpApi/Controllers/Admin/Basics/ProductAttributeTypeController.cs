using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tiger.Basic.ProductAttributeTypes;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Tiger.Controllers.Admin.Basics
{

    /// <summary>
    /// 商品属性类型
    /// </summary>
    [RemoteService(Name = "ProductAttributeType")]
    [Area("Basics")]
    [ControllerName("ProductAttributeType")]
    [Route("api/basic/productAttributeType")]
    [ApiExplorerSettings(GroupName = "admin-basic")]
    public class ProductAttributeTypeController : TigerController, IProductAttributeTypeAppService
    {
        protected readonly IProductAttributeTypeAppService _productAttributeTypeAppService;

        public ProductAttributeTypeController(IProductAttributeTypeAppService productAttributeTypeAppService)
        {
            _productAttributeTypeAppService = productAttributeTypeAppService;
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ProductAttributeTypeDto> CreateAsync(CreateUpdateProductAttributeTypeDto input)
        {
            return await _productAttributeTypeAppService.CreateAsync(input);
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
            return  _productAttributeTypeAppService.DeleteAsync(id);
        }

        /// <summary>
        /// 获取明细
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<ProductAttributeTypeDto> GetAsync(Guid id)
        {
            return await _productAttributeTypeAppService.GetAsync(id);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<PagedResultDto<ProductAttributeTypeDto>> GetListAsync(GetProductAttributeTypeListDto input)
        {
            return await _productAttributeTypeAppService.GetListAsync(input);
        }

        /// <summary>
        /// 获取所有列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("all")]
        public async Task<List<ProductAttributeTypeDto>> GetAllAsync()
        {
            return await _productAttributeTypeAppService.GetAllAsync();
        }


        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        public async Task<ProductAttributeTypeDto> UpdateAsync(Guid id, CreateUpdateProductAttributeTypeDto input)
        {
            return await _productAttributeTypeAppService.UpdateAsync(id, input);
        }
    }
}
