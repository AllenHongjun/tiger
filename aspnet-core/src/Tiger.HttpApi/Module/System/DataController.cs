using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Tiger.Module.System.Platform.Datas.Dtos;
using Tiger.Module.System.Platform.Datas;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp;
using Tiger.Module.System.Platform;

namespace Tiger.Module.System
{   
    /// <summary>
    /// 数据字典
    /// </summary>
    [RemoteService(Name = PlatformRemoteServiceConsts.RemoteServiceName)]
    [Area("platform")]
    [Route("api/platform/datas")]
    public class DataController : AbpController, IDataAppService
    {
        protected IDataAppService DataAppService { get; }

        public DataController(
            IDataAppService dataAppService)
        {
            DataAppService = dataAppService;
        }

        #region 数据字典
        /// <summary>
        /// 创建数据字典
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async virtual Task<DataDto> CreateAsync(DataCreateDto input)
        {
            return await DataAppService.CreateAsync(input);
        }

        /// <summary>
        /// 删除数据字典
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        public async virtual Task DeleteAsync(Guid id)
        {
            await DataAppService.DeleteAsync(id);
        }

        /// <summary>
        /// 根据名称查询数据字典
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("by-name/{name}")]
        public async virtual Task<DataDto> GetAsync(string name)
        {
            return await DataAppService.GetAsync(name);
        }

        /// <summary>
        /// 根据id查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public async virtual Task<DataDto> GetAsync(Guid id)
        {
            return await DataAppService.GetAsync(id);
        }

        /// <summary>
        /// 查询所有数据字典
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("all")]
        public async virtual Task<ListResultDto<DataDto>> GetAllAsync()
        {
            return await DataAppService.GetAllAsync();
        }

        /// <summary>
        /// 查询数据字典列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        public async virtual Task<PagedResultDto<DataDto>> GetListAsync(DataGetListInput input)
        {
            return await DataAppService.GetListAsync(input);
        }

        /// <summary>
        /// 移动数据字典
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}/move")]
        public async virtual Task<DataDto> MoveAsync(Guid id, DataMoveDto input)
        {
            return await DataAppService.MoveAsync(id, input);
        }

        /// <summary>
        /// 更新数据字典
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        public async virtual Task<DataDto> UpdateAsync(Guid id, DataUpdateDto input)
        {
            return await DataAppService.UpdateAsync(id, input);
        } 
        #endregion

        #region 字典数据
        /// <summary>
        /// 创建字典数据
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("{id}/items")]
        public async virtual Task CreateItemAsync(Guid id, DataItemCreateDto input)
        {
            await DataAppService.CreateItemAsync(id, input);
        }

        /// <summary>
        /// 删除字典数据
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}/items/{name}")]
        public async virtual Task DeleteItemAsync(Guid id, string name)
        {
            await DataAppService.DeleteItemAsync(id, name);
        }

        /// <summary>
        /// 修改字典数据
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}/items/{name}")]
        public async virtual Task UpdateItemAsync(Guid id, string name, DataItemUpdateDto input)
        {
            await DataAppService.UpdateItemAsync(id, name, input);
        } 
        #endregion
    }
}
