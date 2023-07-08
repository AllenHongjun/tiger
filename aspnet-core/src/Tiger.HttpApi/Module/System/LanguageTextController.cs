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

        [HttpPost]
        public async Task<LanguageTextDto> CreateAsync(CreateLanguageTextDto input)
        {
            return await LanguageTextAppService.CreateAsync(input);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task DeleteAsync(Guid id)
        {
            await LanguageTextAppService.DeleteAsync(id);
        }

        [HttpGet]
        public async Task<PagedResultDto<LanguageTextDto>> GetListAsync(LanguageTextGetListInput input)
        {
            return await LanguageTextAppService.GetListAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<LanguageTextDto> UpdateAsync(Guid id, UpdateLanguageTextDto input)
        {
            return await LanguageTextAppService.UpdateAsync(id,input);
        }
    }
}
