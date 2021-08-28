using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Tiger.Basic;
using Tiger.Basic.Dtos;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Tiger.Controllers.Admin.Basics
{
    /// <summary>
    /// 产品分类
    /// </summary>

    [RemoteService(Name = "Store")]
    [Area("Basics")]
    [ControllerName("Store")]
    [Route("api/basic/store")]
    [ApiExplorerSettings(GroupName = "admin-basic")]
    public class StoreController : TigerController, IStoreAppService
    {
        protected readonly IStoreAppService _storeAppService;

        public StoreController(IStoreAppService storeAppService)
        {
            _storeAppService = storeAppService;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<StoreDto> CreateAsync(CreateUpdateStoreDto input)
        {
            return _storeAppService.CreateAsync(input);
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
            return _storeAppService.DeleteAsync(id);
        }

        /// <summary>
        /// 获取单条
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public Task<StoreDto> GetAsync(Guid id)
        {
            return _storeAppService.GetAsync(id);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        //[Route("all")]
        public Task<PagedResultDto<StoreDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            return _storeAppService.GetListAsync(input);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        public Task<StoreDto> UpdateAsync(Guid id, CreateUpdateStoreDto input)
        {
            return _storeAppService.UpdateAsync(id, input);
        }


        

        
    }
}
