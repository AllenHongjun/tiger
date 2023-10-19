using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Tiger.Module.System.Area;
using Tiger.Module.System.Area.Dtos;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace Tiger.Module.System
{
    /// <summary>
    /// 区域
    /// </summary>
    [RemoteService(Name = "region")]
    [Area("area")]
    [Route("api/area/regions")]
    public class RegionController : AbpController, IRegionAppService
    {
        #region 构造函数
        public RegionController(IRegionAppService regionAppService)
        {
            RegionAppService=regionAppService;
        }

        protected IRegionAppService RegionAppService { get; } 
        #endregion

        #region CRUD
        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<RegionDto> CreateAsync(CreateUpdateRegionDto input)
        {
            return await RegionAppService.CreateAsync(input);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        public async Task DeleteAsync(int id)
        {
            await RegionAppService.DeleteAsync(id);
        }

        /// <summary>
        /// 详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<RegionDto> GetAsync(int id)
        {
            return await RegionAppService.GetAsync(id);
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<PagedResultDto<RegionDto>> GetListAsync(RegionGetListInput input)
        {
            return await RegionAppService.GetListAsync(input);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        public async Task<RegionDto> UpdateAsync(int id, CreateUpdateRegionDto input)
        {
            return await RegionAppService.UpdateAsync(id, input);
        } 
        #endregion
    }
}
