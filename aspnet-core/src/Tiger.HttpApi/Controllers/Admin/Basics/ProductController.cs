using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tiger.Basic.Products;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Tiger.Controllers.Admin.Basics
{

    /// <summary>
    /// 产品
    /// </summary>
    [RemoteService(Name = "Product")]
    [Area("Basics")]
    [ControllerName("Product")]
    [Route("api/basic/product")]
    [ApiExplorerSettings(GroupName = "admin-basic")]
    public class ProductController : TigerController, IProductAppService
    {
        private readonly IProductAppService _productAppService;

        public ProductController(IProductAppService productAppService)
        {
            _productAppService = productAppService;
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<ProductDto> CreateAsync(CreateUpdateProductDto input)
        {
            return _productAppService.CreateAsync(input);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        public Task DeleteAsync(Guid id)
        {
            return _productAppService.DeleteAsync(id);
        }

        /// <summary>
        /// 获取明细
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public Task<ProductDto> GetAsync(Guid id)
        {
            return _productAppService.GetAsync(id);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        public Task<PagedResultDto<ProductDto>> GetListAsync(GetProductListDto input)
        {
            return _productAppService.GetListAsync(input);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        public Task<ProductDto> UpdateAsync(Guid id, CreateUpdateProductDto input)
        {
            return _productAppService.UpdateAsync(id, input);
        }
    }
}
