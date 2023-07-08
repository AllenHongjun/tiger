﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tiger.Volo.Abp.Sass.Editions;
using Tiger.Volo.Abp.Sass.Permissions;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Tiger.Volo.Abp.Sass
{

    /// <summary>
    /// 版本
    /// </summary>
    [Controller]
    [Authorize(AbpSaasPermissions.Editions.Default)]
    [RemoteService(Name = AbpSaasRemoteServiceConsts.RemoteServiceName)]
    [Area(AbpSaasRemoteServiceConsts.ModuleName)]
    [Route("api/saas/editions")]
    public class EditionController : AbpSaasControllerBase, IEditionAppService
    {
        public EditionController(IEditionAppService editionAppService)
        {
            EditionAppService=editionAppService;
        }

        protected IEditionAppService EditionAppService { get; }

        [HttpPost]
        [Authorize(AbpSaasPermissions.Editions.Create)]
        public async Task<EditionDto> CreateAsync(EditionCreateDto input)
        {
            return await EditionAppService.CreateAsync(input);
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(AbpSaasPermissions.Editions.Delete)]
        public async virtual Task DeleteAsync(Guid id)
        {
             await EditionAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<EditionDto> GetAsync(Guid id)
        {
            return await EditionAppService.GetAsync(id);
        }

        [HttpGet]
        public async Task<PagedResultDto<EditionDto>> GetListAsync(EditionGetListInput input)
        {
            return await EditionAppService.GetListAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        [Authorize(AbpSaasPermissions.Editions.Update)]
        public async Task<EditionDto> UpdateAsync(Guid id, EditionUpdateDto input)
        {
            return await EditionAppService.UpdateAsync(id, input);
        }
    }
}