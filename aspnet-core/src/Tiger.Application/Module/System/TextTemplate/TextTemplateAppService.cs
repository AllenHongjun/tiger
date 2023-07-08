using System;
using System.Linq;
using System.Threading.Tasks;
using Tiger.Permissions;
using Tiger.Module.System.TextTemplate.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Application.Dtos;
using Microsoft.AspNetCore.Authorization;
using Tiger.Module.System.TextTemplate.Permissions;
using Volo.Abp.TextTemplating;
using System.Collections.Generic;
using Volo.Abp;

namespace Tiger.Module.System.TextTemplate;


/// <summary>
/// 文本模板服务
/// </summary>
[Authorize(AbpTextTemplatePermissions.TextTemplate.Default)]
[RemoteService(IsEnabled = false)]
public class TextTemplateAppService : ApplicationService,
    ITextTemplateAppService
{
    protected ITextTemplateRepository TextTemplateRepository { get; }
    protected ITemplateContentProvider TextTemplateContentProvider { get; }
    protected ITemplateDefinitionManager TemplateDefinitionManager { get; }

    public TextTemplateAppService(
        ITextTemplateRepository textTemplateRepository,
        ITemplateDefinitionManager templateDefinitionManager,
        ITemplateContentProvider textTemplateContentProvider)
    {
        TextTemplateRepository = textTemplateRepository;
        TemplateDefinitionManager=templateDefinitionManager;
        TextTemplateContentProvider=textTemplateContentProvider;
    }

    /// <summary>
    /// 根据文本模板名称获取定义内容
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task<TextTemplateDefinitionDto> GetAsync(string name)
    {
        //获取文本模板定义的内容
        var templateDefinition = GetTemplateDefinition(name);

        // 判断布局页是否存在
        var layout = templateDefinition.Layout;
        if (!layout.IsNullOrWhiteSpace())
        {
            // 获取布局页名称
            var layoutDefinition = GetTemplateDefinition(templateDefinition.Layout);
            // 本地化显示
            layout = layoutDefinition.DisplayName.Localize(StringLocalizerFactory);
        }

        var result = new TextTemplateDefinitionDto
        {
            DefaultCultureName = templateDefinition.DefaultCultureName,
            IsInlineLocalized = templateDefinition.IsInlineLocalized,
            IsLayout = templateDefinition.IsLayout,
            Layout = layout,
            Name = templateDefinition.Name,
            DisplayName = templateDefinition.DisplayName.Localize(StringLocalizerFactory)
        };

        return Task.FromResult(result);
    }

    /// <summary>
    /// 获取文本模板内容
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<TextTemplateContentDto> GetContentAsync(TextTemplateContentGetInput input)
    {
        var templateDefinition = GetTemplateDefinition(input.Name);

        var content = await TextTemplateContentProvider.GetContentOrNullAsync(templateDefinition, input.Culture);

        return new TextTemplateContentDto
        {
            Name = templateDefinition.Name,
            Culture = input.Culture,
            Content = content,
        };
    }

    /// <summary>
    /// 获取所有的文本模板内容
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    /// <remarks>
    /// abp的文本模板作为资源定义在文件中 从文件中读取出来展示
    /// </remarks>
    public Task<PagedResultDto<TextTemplateDefinitionDto>> GetListAsync(TextTemplateGetListInput input)
    {
        var templates = new List<TextTemplateDefinitionDto>();

        // 使用abp自定义的方法获取所有定义的文本模板
        var templateDefinitions = TemplateDefinitionManager.GetAll();
        var filterTemplates = templateDefinitions
            .WhereIf(!input.Filter.IsNullOrWhiteSpace(), x => x.Name.Contains(input.Filter) || x.Layout.Contains(input.Filter))
            .Skip(input.SkipCount)
            .Take(input.MaxResultCount);

        foreach (var templateDefinition in filterTemplates)
        {
            var layout = templateDefinition.Layout;
            if (!layout.IsNullOrWhiteSpace())
            {
                var layoutDefinition = GetTemplateDefinition(templateDefinition.Layout);
                layout = layoutDefinition.DisplayName.Localize(StringLocalizerFactory);
            }

            var result = new TextTemplateDefinitionDto
            {
                DefaultCultureName = templateDefinition.DefaultCultureName,
                IsInlineLocalized = templateDefinition.IsInlineLocalized,
                IsLayout = templateDefinition.IsLayout,
                Layout = layout,
                Name = templateDefinition.Name,
                DisplayName = templateDefinition.DisplayName.Localize(StringLocalizerFactory),
            };

            templates.Add(result);
        }

        return Task.FromResult(new PagedResultDto<TextTemplateDefinitionDto>(templateDefinitions.Count, templates));
    }

    /// <summary>
    /// 重置默认值
    /// </summary>
    /// <param name="restoreInput"></param>
    /// <returns></returns>
    [Authorize(AbpTextTemplatePermissions.TextTemplate.Delete)]
    public async Task RestoreToDefaultAsync(TextTemplateRestoreInput restoreInput)
    {
        var templateDefinition = GetTemplateDefinition(restoreInput.Name);

        var templates = TextTemplateRepository
            .Where(x => x.Name == templateDefinition.Name && x.Culture == restoreInput.Culture).ToList();
        foreach (var template in templates)
        {
            // DeleteManyAsync 这个3.2.1 版本没有这个方法
            await TextTemplateRepository.DeleteAsync(template);
        }
        await CurrentUnitOfWork.SaveChangesAsync();
    }

    /// <summary>
    /// 更新文本模板内容
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [Authorize(AbpTextTemplatePermissions.TextTemplate.Update)]
    public async Task<TextTemplateDefinitionDto> UpdateAsync(TextTemplateUpdateInput input)
    {
        var templateDefinition = GetTemplateDefinition(input.Name);
        var template = await TextTemplateRepository.FindByNameAsync(input.Name, input.Culture);

        if (template == null)
        {
            template = new TextTemplate(
                GuidGenerator.Create(),
                templateDefinition.Name,
                templateDefinition.DisplayName.Localize(StringLocalizerFactory),
                input.Content,
                input.Culture);
            await TextTemplateRepository.InsertAsync(template);
        }
        else
        {
            template.SetContent(input.Content);
            await TextTemplateRepository.UpdateAsync(template);
        }

        await CurrentUnitOfWork.SaveChangesAsync();

        var layout = templateDefinition.Layout;
        if (!layout.IsNullOrWhiteSpace())
        {
            var layoutDefinition = GetTemplateDefinition(templateDefinition.Layout);
            layout = layoutDefinition.DisplayName.Localize(StringLocalizerFactory);
        }

        return new TextTemplateDefinitionDto
        {
            DefaultCultureName = templateDefinition.DefaultCultureName,
            IsInlineLocalized = templateDefinition.IsInlineLocalized,
            IsLayout = templateDefinition.IsLayout,
            Layout = layout,
            Name = templateDefinition.Name,
            DisplayName = templateDefinition.DisplayName.Localize(StringLocalizerFactory)
        };
    }


    /// <summary>
    /// 根据名称获取模板定义
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    /// <remarks>
    /// TemplateDefinition abp的定义模板获取  使用之前需要先定义
    /// 详细见文档 https://docs.abp.io/en/abp/latest/Text-Templating-Razor
    /// </remarks>
    protected virtual TemplateDefinition GetTemplateDefinition(string name)
    {
        var template = TemplateDefinitionManager.GetOrNull(name);
        if (template == null)
        {
            throw new BusinessException(
                AbpTextTemplateErrorCodes.TemplateNotFound,
                $"The text template {name} does not exist.").WithData("Name", name);
        }

        return template;
    }
}
