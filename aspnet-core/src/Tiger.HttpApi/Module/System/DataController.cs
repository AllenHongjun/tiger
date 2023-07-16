﻿using Microsoft.AspNetCore.Mvc;
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

        [HttpPost]
        public async virtual Task<DataDto> CreateAsync(DataCreateDto input)
        {
            return await DataAppService.CreateAsync(input);
        }

        [HttpPost]
        [Route("{id}/items")]
        public async virtual Task CreateItemAsync(Guid id, DataItemCreateDto input)
        {
            await DataAppService.CreateItemAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public async virtual Task DeleteAsync(Guid id)
        {
            await DataAppService.DeleteAsync(id);
        }

        [HttpDelete]
        [Route("{id}/items/{name}")]
        public async virtual Task DeleteItemAsync(Guid id, string name)
        {
            await DataAppService.DeleteItemAsync(id, name);
        }

        [HttpGet]
        [Route("by-name/{name}")]
        public async virtual Task<DataDto> GetAsync(string name)
        {
            return await DataAppService.GetAsync(name);
        }

        [HttpGet]
        [Route("{id}")]
        public async virtual Task<DataDto> GetAsync(Guid id)
        {
            return await DataAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("all")]
        public async virtual Task<ListResultDto<DataDto>> GetAllAsync()
        {
            return await DataAppService.GetAllAsync();
        }

        [HttpGet]
        public async virtual Task<PagedResultDto<DataDto>> GetListAsync(DataGetListInput input)
        {
            return await DataAppService.GetListAsync(input);
        }

        [HttpPut]
        [Route("{id}/move")]
        public async virtual Task<DataDto> MoveAsync(Guid id, DataMoveDto input)
        {
            return await DataAppService.MoveAsync(id, input);
        }

        [HttpPut]
        [Route("{id}")]
        public async virtual Task<DataDto> UpdateAsync(Guid id, DataUpdateDto input)
        {
            return await DataAppService.UpdateAsync(id, input);
        }

        [HttpPut]
        [Route("{id}/items/{name}")]
        public async virtual Task UpdateItemAsync(Guid id, string name, DataItemUpdateDto input)
        {
            await DataAppService.UpdateItemAsync(id, name, input);
        }
    }
}
