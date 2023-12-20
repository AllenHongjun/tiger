using ClosedXML.Excel;
using DocumentFormat.OpenXml.Office2010.CustomUI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Tiger.Infrastructure.ExportImport;
using Tiger.Infrastructure.ExportImport.Help;
using Tiger.Module.QuestionBank.Dtos;
using Tiger.Module.QuestionBank.Localization;
using Tiger.Module.QuestionBank.Permissions;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

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
    private readonly IQuestionCategoryRepository _categoryRepository;

    public QuestionAppService(
        IQuestionRepository repository, 
        IQuestionCategoryRepository categoryRepository) : base(repository)
    {
        LocalizationResource = typeof(QuestionBankResources);
        _repository = repository;
        _categoryRepository=categoryRepository;
    }
    #endregion

    #region CRUD
    protected override IQueryable<Question> CreateFilteredQuery(QuestionGetListInput input)
    {
        // TODO: AbpHelper generated
        return _repository.WithDetails(x => x.QuestionCategory)
            .WhereIf(!input.Filter.IsNullOrWhiteSpace(), x => x.Name.Contains(input.Filter) || x.Content.Contains(input.Filter))
            .WhereIf(input.CreateStartTime != null, x => x.CreationTime >= input.CreateStartTime)
            .WhereIf(input.CreateEndTime != null, x => x.CreationTime <= input.CreateEndTime)
            .WhereIf(input.QuestionCategoryId != null, x => x.QuestionCategoryId == input.QuestionCategoryId)
            .WhereIf(input.PracticalTrainingId != null, x => x.TrainPlatformId == input.PracticalTrainingId)
            .WhereIf(input.Type != null, x => x.Type == input.Type)
            .WhereIf(input.Degree != null, x => x.Degree == input.Degree)
            .WhereIf(!input.Analysis.IsNullOrWhiteSpace(), x => x.Analysis.Contains(input.Analysis))
            .WhereIf(input.IsShow != null, x => x.IsShow == input.IsShow)
            .WhereIf(input.Enable != null, x => x.Enable == input.Enable)
            ;
    }

    public override async Task<PagedResultDto<QuestionDto>> GetListAsync(QuestionGetListInput input)
    {
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
        DifferentDegreeQuestionCountDto result = new DifferentDegreeQuestionCountDto();
        List<Guid> questionCategoryIds = new List<Guid> { input.QuestionCategoryId };
        var differentDegreeQuestionCount = (await _repository.GetDifferentDegreeQuestionCount(questionCategoryIds, input.Type)).FirstOrDefault();
        if (differentDegreeQuestionCount != null)
        {
            result = ObjectMapper
            .Map<DifferentDegreeQuestionCountInfo, DifferentDegreeQuestionCountDto>(differentDegreeQuestionCount);
        }
        return result;
    }
    #endregion

    #region 导入/导出

    protected QuestionType GetQuestionType(string name)
    {
        // TODO：重构一个通用的遍历枚举的方法
        QuestionType type = QuestionType.None;
        if (name == L["DisplayName:TrueOrFalse"]) type = QuestionType.TrueOrFalse;
        if (name == L["DisplayName:SingleChoice"]) type = QuestionType.SingleChoice;
        if (name == L["DisplayName:MultipleChoice"]) type = QuestionType.MultipleChoice;
        if (name == L["DisplayName:Completion"]) type = QuestionType.Completion;
        if (name == L["DisplayName:QA"]) type = QuestionType.QA;
        if (name == L["DisplayName:PracticalTraining"]) type = QuestionType.PracticalTraining;
        return type;
    }

    protected QuestionDegree GetQuestionDegree(string name)
    {
        QuestionDegree degree = QuestionDegree.UnlimitedDifficulty;
        if (name == L["DisplayName:UnlimitedDifficulty"]) degree = QuestionDegree.UnlimitedDifficulty;
        if (name == L["DisplayName:Simple"]) degree = QuestionDegree.Simple;
        if (name == L["DisplayName:Ordinary"]) degree = QuestionDegree.Ordinary;
        if (name == L["DisplayName:Difficult"]) degree = QuestionDegree.Difficult;
        return degree;
    }

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
            var properties = ImportManager.GetPropertiesByExcelCells<QuestionDto>(worksheet);

            var manager = new PropertyManager<QuestionDto>(properties);
            var iRow = 2;

            while (true)
            {
                var allColumnsAreEmpty = manager.GetProperties
                    .Select(property => worksheet.Row(iRow).Cell(property.PropertyOrderPosition))
                    .All(cell => cell?.Value == null || string.IsNullOrEmpty(cell.Value.ToString()));

                if (allColumnsAreEmpty)
                    break;

                manager.ReadFromXlsx(worksheet, iRow);
                Guid id = manager.GetProperty("Id").GuidValue;
                Question question = null;
                var isNew = id == Guid.Empty;

                // 根据分类名称查询分类
                var category = await _categoryRepository.GetByName(manager.GetProperty(L["DisplayName:QuestionCateogryName"]).StringValue);
                if (category == null) throw new UserFriendlyException(L["CategoryIsNotFound"], manager.GetProperty(L["DisplayName:QuestionCateogryName"]).StringValue);

                // 移除每个选项开头的A B C D ,后保存到数据库中
                string optionContent = manager.GetProperty(L["DisplayName:OptionContent"]).StringValue;
                if (!string.IsNullOrWhiteSpace(optionContent))
                {
                    // C#下分割string （spilt）回车换行符（\r\n）
                    var optionContentArr = optionContent.Split(new string[] { "\r\n" }, StringSplitOptions.None);
                    List<string> temp = new List<string>();
                    for (int i = 0; i < optionContentArr.Length; i++)
                    {
                        temp.Add(optionContentArr[i].Substring(2));
                    }
                    optionContent = string.Join("\r\n", temp);
                }

                if (isNew)
                {
                    question ??= new Question(
                        GuidGenerator.Create(), 
                        CurrentTenant.Id,
                        category.Id,
                        null,
                        GetQuestionType(manager.GetProperty(L["DisplayName:Type"]).StringValue),
                        "",
                        manager.GetProperty(L["DisplayName:Content"]).StringValue,
                        optionContent,
                        null,
                        manager.GetProperty(L["DisplayName:Answer"]).StringValue,
                        manager.GetProperty(L["DisplayName:Score"]).DecimalValue,
                        GetQuestionDegree(manager.GetProperty(L["DisplayName:Answer"]).StringValue),
                        manager.GetProperty(L["DisplayName:Analysis"]).StringValue,
                        manager.GetProperty(L["DisplayName:Source"]).StringValue
                        );
                    await _repository.InsertAsync(question);
                }
                else
                {
                    question = await _repository.GetAsync(manager.GetProperty("Id").GuidValue);
                    foreach (var property in manager.GetProperties)
                    {
                        if (property.PropertyName == L["DisplayName:QuestionCateogryName"]) {
                            question.QuestionCategoryId = category.Id;
                        } 
                        if (property.PropertyName == L["DisplayName:Type"]) question.Type = GetQuestionType(property.StringValue);
                        if (property.PropertyName == L["DisplayName:Content"]) question.Content = property.StringValue;
                        if (property.PropertyName == L["DisplayName:OptionContent"]) question.OptionContent = optionContent;
                        if (property.PropertyName == L["DisplayName:Answer"]) question.Answer = property.StringValue;
                        if (property.PropertyName == L["DisplayName:Score"]) question.Score = property.DecimalValue;
                        if (property.PropertyName == L["DisplayName:Degree"]) question.Degree = GetQuestionDegree(property.StringValue);
                        if (property.PropertyName == L["DisplayName:Analysis"]) question.Analysis = property.StringValue;
                        if (property.PropertyName == L["DisplayName:Source"]) question.Source = property.StringValue;
                    }
                    await _repository.UpdateAsync(question);
                }

                iRow++;
            }
        }
        else
        {
            throw new Exception(L["FileIsEmpty"]);
        }
    }

    

    /// <summary>
    /// Export to XLSX
    /// </summary>
    /// <param name="GetIdentityRolesInput">input</param>
    /// <returns>查询到的所有角色</returns>
    public virtual async Task<IActionResult> ExportToXlsxAsync(QuestionGetListInput input)
    {
        input.MaxResultCount = input.MaxResultCount == 1 ? 1 : int.MaxValue;
        var list = await base.GetListAsync(input);

        var alphas = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };

        //property manager 
        var manager = new PropertyManager<QuestionDto>(new[]
        {
                new PropertyByName<QuestionDto>("Id", p => p.Id),
                new PropertyByName<QuestionDto>(L["DisplayName:QuestionCateogryName"], p => p.QuestionCateogryName),
                new PropertyByName<QuestionDto>(L["DisplayName:Type"], p => L["DisplayName:" + p.Type + ""]),
                new PropertyByName<QuestionDto>(L["DisplayName:Content"], p => p.Content),
                new PropertyByName<QuestionDto>(L["DisplayName:OptionContent"], p => {
                    // 单元格中每个选项一行，然后每个选项前面拼上 A  B  C 等选项
                    if (!string.IsNullOrWhiteSpace(p.OptionContent))
                    {
                        // C#下分割string （spilt）回车换行符（\r\n）
                        var optionContentArr = p.OptionContent.Split(new string[] { "\r\n" }, StringSplitOptions.None);
                        List<string> temp = new List<string>();
                        for (int i = 0; i < optionContentArr.Length; i++)
                        {
                            temp.Add( alphas[i] + ". " + optionContentArr[i]);
                        }
                        p.OptionContent = string.Join("\r\n", temp);
                    }
                    return p.OptionContent;
                }),
                new PropertyByName<QuestionDto>(L["DisplayName:Answer"], p => p.Answer),
                new PropertyByName<QuestionDto>(L["DisplayName:Degree"], p => L["DisplayName:" + p.Degree + ""]),
                new PropertyByName<QuestionDto>(L["DisplayName:Analysis"], p => p.Analysis),
                new PropertyByName<QuestionDto>(L["DisplayName:Source"], p => p.Source),
            });

        var bytes = await manager.ExportToXlsxAsync(list.Items.ToList(), true);

        return new FileContentResult(bytes, MimeTypes.TextXlsx);
    } 
    #endregion

    #region 批量操作
    [Authorize(QuestionBankPermissions.Question.Delete)]
    public async virtual Task BatchDeleteAsync(QuestionBatchInput input)
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
