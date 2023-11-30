using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Tiger.Permissions;
using Tiger.Module.QuestionBank.Dtos;
using Volo.Abp.Application.Services;
using Tiger.Module.System.Platform.Layouts.Dto;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;
using Volo.Abp;
using Microsoft.AspNetCore.Authorization;
using Tiger.Infrastructure.BackgroundTasks.Abstractions.Enum;
using Tiger.Module.TaskManagement.Dtos;
using Tiger.Module.TaskManagement.Permissions;
using Tiger.Module.TaskManagement;
using Tiger.Infrastructure.BackgroundTasks.Abstractions;
using Volo.Abp.Uow;
using Tiger.Module.QuestionBank.Permissions;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Tiger.Infrastructure.ExportImport.Help;
using Tiger.Infrastructure.ExportImport;
using Tiger.Volo.Abp.Identity.ClaimTypes.Dto;
using Tiger.Volo.Abp.Identity;
using Volo.Abp.Identity;
using Microsoft.AspNetCore.Http;
using DocumentFormat.OpenXml.Math;

namespace Tiger.Module.QuestionBank;


/// <summary>
/// 题目
/// </summary>
[RemoteService(false)]
public class QuestionAppService : CrudAppService<Question, QuestionDto, Guid, QuestionGetListInput, CreateUpdateQuestionDto, CreateUpdateQuestionDto>,
    IQuestionAppService
{

    #region 字段和构造函数
    private readonly IQuestionRepository _repository;

    public QuestionAppService(IQuestionRepository repository) : base(repository)
    {
        _repository = repository;
    }
    #endregion

    #region CRUD
    protected override IQueryable<Question> CreateFilteredQuery(QuestionGetListInput input)
    {
        // TODO: AbpHelper generated
        return _repository.WithDetails(x => x.QuestionCategory)
            .WhereIf(!input.Filter.IsNullOrWhiteSpace(), x => x.Name.Contains(input.Filter) || x.Content.Contains(input.Filter))
            .WhereIf(input.CreateEndTime != null, x => x.CreationTime >= input.CreateStartTime)
            .WhereIf(input.CreateEndTime != null, x => x.CreationTime <= input.CreateEndTime)
            .WhereIf(input.QuestionCategoryId != null, x => x.QuestionCategoryId == input.QuestionCategoryId)
            .WhereIf(input.PracticalTrainingId != null, x => x.TrainPlatformId == input.PracticalTrainingId)
            .WhereIf(input.Type != null, x => x.Type == input.Type)
            .WhereIf(!input.Name.IsNullOrWhiteSpace(), x => x.Name.Contains(input.Name))
            .WhereIf(!input.Content.IsNullOrWhiteSpace(), x => x.Content.Contains(input.Content))
            //.WhereIf(!input.OptionContent.IsNullOrWhiteSpace(), x => x.OptionContent.Contains(input.OptionContent))
            //.WhereIf(input.OptionSize != null, x => x.OptionSize == input.OptionSize)
            //.WhereIf(!input.Answer.IsNullOrWhiteSpace(), x => x.Answer.Contains(input.Answer))
            .WhereIf(input.Degree != null, x => x.Degree == input.Degree)
            .WhereIf(!input.Analysis.IsNullOrWhiteSpace(), x => x.Analysis.Contains(input.Analysis))
            .WhereIf(!input.Source.IsNullOrWhiteSpace(), x => x.Source.Contains(input.Source))
            .WhereIf(!input.HelpMessage.IsNullOrWhiteSpace(), x => x.HelpMessage.Contains(input.HelpMessage))
            .WhereIf(!input.HelpVideo.IsNullOrWhiteSpace(), x => x.HelpVideo.Contains(input.HelpVideo))
            .WhereIf(!input.FileUrl.IsNullOrWhiteSpace(), x => x.FileUrl.Contains(input.FileUrl))
            .WhereIf(!input.FileName.IsNullOrWhiteSpace(), x => x.FileName.Contains(input.FileName))
            .WhereIf(input.IsShow != null, x => x.IsShow == input.IsShow)
            .WhereIf(input.Enable != null, x => x.Enable == input.Enable)
            .WhereIf(input.IsShowTextButton != null, x => x.IsShowTextButton == input.IsShowTextButton)
            .WhereIf(input.IsShowImageButton != null, x => x.IsShowImageButton == input.IsShowImageButton)
            .WhereIf(input.IsShowLinkButton != null, x => x.IsShowLinkButton == input.IsShowLinkButton)
            ;
    }

    public override async Task<PagedResultDto<QuestionDto>> GetListAsync(QuestionGetListInput input)
    {
        // TODO:获取关联表的数据 
        /*
         1. 二次查询
         2. joio关联查询数据
         3. 导航属性 关联数据
         */
        return await base.GetListAsync(input);
    }

    public override async Task<QuestionDto> GetAsync(Guid id)
    {
        return await base.GetAsync(id);
    }

    public override async Task<QuestionDto> CreateAsync(CreateUpdateQuestionDto input)
    {
        return await base.CreateAsync(input);
    }

    public override async Task DeleteAsync(Guid id)
    {
        await base.DeleteAsync(id);
    }

    public override async Task<QuestionDto> UpdateAsync(Guid id, CreateUpdateQuestionDto input)
    {
        return await base.UpdateAsync(id, input);
    } 

    public async Task<DifferentDegreeQuestionCountDto> GetDifferentDegreeQuestionCount(GetDifferentDegreeQuestionCountInput input)
    {
        List<Guid> questionCategoryIds = new List<Guid> { input.QuestionCategoryId };
        var differentDegreeQuestionCount = (await _repository.GetDifferentDegreeQuestionCount(questionCategoryIds, input.Type)).FirstOrDefault();
        return ObjectMapper
            .Map<DifferentDegreeQuestionCountInfo, DifferentDegreeQuestionCountDto>(differentDegreeQuestionCount);
    }
    #endregion

    #region 导入/导出
    /// <summary>
    /// 从xlsx导入
    /// </summary>
    /// <param name="importexcelfile"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public virtual async Task ImportFromXlsxAsync(IFormFile importexcelfile)
    {

        if (importexcelfile != null && importexcelfile.Length > 0)
        {
            using var workbook = new XLWorkbook(importexcelfile.OpenReadStream());
            // get the first worksheet in the workbook
            var worksheet = workbook.Worksheets.FirstOrDefault();
            if (worksheet == null)
                throw new Exception("No worksheet found");

            //the columns
            var properties = ImportManager.GetPropertiesByExcelCells<IdentityRoleDto>(worksheet);

            var manager = new PropertyManager<IdentityRoleDto>(properties);
            var iRow = 2;

            while (true)
            {
                var allColumnsAreEmpty = manager.GetProperties
                    .Select(property => worksheet.Row(iRow).Cell(property.PropertyOrderPosition))
                    .All(cell => cell?.Value == null || string.IsNullOrEmpty(cell.Value.ToString()));

                if (allColumnsAreEmpty)
                    break;

                manager.ReadFromXlsx(worksheet, iRow);

                var question = await _repository.GetAsync(manager.GetProperty("Id").GuidValue);

                var isNew = question == null;

                // TODO: 增加验证 如果错误抛出异常

                question ??= new Question(GuidGenerator.Create(), CurrentTenant.Id, manager.GetProperty("Content").StringValue);

                foreach (var property in manager.GetProperties)
                {
                    if (property.PropertyName == L["DisplayName:Content"]) question.Content = property.StringValue;
                    if (property.PropertyName == L["DisplayName:QuestionCateogryName"]) question.QuestionCategoryId = property.GuidValue;
                    if (property.PropertyName == L["DisplayName:Type"]) question.Type = (QuestionType)property.IntValue;
                    if (property.PropertyName == L["DisplayName:OptionContent"]) question.OptionContent = property.StringValue;
                    if (property.PropertyName == L["DisplayName:Answer"]) question.Answer = property.StringValue;
                    if (property.PropertyName == L["DisplayName:Degree"]) question.Degree = (QuestionDegree)property.IntValue;
                    if (property.PropertyName == L["DisplayName:Analysis"]) question.Analysis = property.StringValue;
                    if (property.PropertyName == L["DisplayName:Source"]) question.Source = property.StringValue;
                    if (property.PropertyName == L["DisplayName:Enable"]) question.Enable = property.BooleanValue;
                }

                if (isNew)
                    await _repository.InsertAsync(question);
                else
                    await _repository.UpdateAsync(question);

                iRow++;
            }

        }
        else
        {
            throw new Exception("文件不能空");
        }
    }

    /// <summary>
    /// Export to XLSX
    /// </summary>
    /// <param name="GetIdentityRolesInput">input</param>
    /// <returns>查询到的所有角色</returns>
    public virtual async Task<IActionResult> ExportToXlsxAsync(QuestionGetListInput input)
    {
        int maxResultCount = input.MaxResultCount == 1 ? 1 : int.MaxValue;
        IPagedResult<QuestionDto> entityDtos = await GetListAsync(input);

        //property manager 
        var manager = new PropertyManager<QuestionDto>(new[]
        {
                new PropertyByName<QuestionDto>("Id", p => p.Id),
                new PropertyByName<QuestionDto>(L["QuestionCateogryName"], p => p.QuestionCateogryName),
                new PropertyByName<QuestionDto>(L["DisplayName:Type"], p => p.Type), // TODO:获取枚举值的注释描述 封装的方法
                new PropertyByName<QuestionDto>(L["DisplayName:Content"], p => p.Content),
                new PropertyByName<QuestionDto>(L["DisplayName:OptionContent"], p => p.OptionContent),
                new PropertyByName<QuestionDto>(L["DisplayName:OptionSize"], p => p.OptionSize),
                new PropertyByName<QuestionDto>(L["DisplayName:Answer"], p => p.Answer),
                new PropertyByName<QuestionDto>(L["DisplayName:Degree"], p => p.Degree), // TODO:获取枚举值的注释描述 封装的方法
                new PropertyByName<QuestionDto>(L["DisplayName:Analysis"], p => p.Analysis),
                new PropertyByName<QuestionDto>(L["DisplayName:Source"], p => p.Source),
                new PropertyByName<QuestionDto>(L["DisplayName:Enable"], p => p.Enable),
            });

        var bytes = await manager.ExportToXlsxAsync(entityDtos.Items.ToList());

        return new FileContentResult(bytes, MimeTypes.TextXlsx);
    } 
    #endregion

    #region 批量操作
    [Authorize(QuestionBankPermissions.Question.Delete)]
    public async virtual Task BulkDeleteAsync(QuestionBatchInput input)
    {
        if (!input.QuestionIds.Any())
        {
            return;
        }

        var quaryble = _repository.Where(x => input.QuestionIds.Contains(x.Id));

        var questions = await AsyncExecuter.ToListAsync(quaryble);

        foreach (var item in questions)
        {
            await _repository.DeleteAsync(item);
        }

        await CurrentUnitOfWork.SaveChangesAsync();
    } 
    #endregion
}
