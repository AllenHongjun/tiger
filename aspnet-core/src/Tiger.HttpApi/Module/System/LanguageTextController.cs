using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Tiger.Module.System.Localization;
using Tiger.Module.System.Localization.Dtos;
using Tiger.Module.System.Localization.Permissions;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace Tiger.Module.System
{

    /// <summary>
    /// 语言文本管理
    /// </summary>
    [Controller]
    [Authorize(LocalizationPermissions.LanguageTexts.Default)]
    [RemoteService(Name = "LanguageText")]
    [Area("Localization")]
    [Route("api/localization/language-text")]
    public class LanguageTextController : AbpController, ILanguageTextAppService
    {
        public LanguageTextController(ILanguageTextAppService languageTextAppService)
        {
            LanguageTextAppService=languageTextAppService;
        }

        protected ILanguageTextAppService LanguageTextAppService { get; }

        #region 语言文本
        /// <summary>
        /// 创建语言文本
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<LanguageDto> CreateAsync(CreateLanguageTextDto input)
        {
            return await LanguageTextAppService.CreateAsync(input);
        }

        /// <summary>
        /// 删除语言文本
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        public async Task DeleteAsync(Guid id)
        {
            await LanguageTextAppService.DeleteAsync(id);
        }

        /// <summary>
        /// 分页获取语言文本列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<PagedResultDto<LanguageDto>> GetListAsync(LanguageTextGetListInput input)
        {
            return await LanguageTextAppService.GetListAsync(input);
        }

        /// <summary>
        /// 更新语言文本
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        public async Task<LanguageDto> UpdateAsync(Guid id, UpdateLanguageTextDto input)
        {
            return await LanguageTextAppService.UpdateAsync(id, input);
        } 
        #endregion
    }
}
