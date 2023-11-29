using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Tiger.Infrastructure.ExportImport.Help;
using Tiger.Module.QuestionBank.Dtos;
using Tiger.Volo.Abp.Identity.ClaimTypes.Dto;
using Tiger.Volo.Abp.Identity;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Identity;
using Tiger.Infrastructure.ExportImport;
using Volo.Abp.Identity;
using Microsoft.AspNetCore.Http;

namespace Tiger.Module.QuestionBank;


/// <summary>
/// 题目分类
/// </summary>
[RemoteService(false)]
public class QuestionCategoryAppService : CrudAppService<QuestionCategory, QuestionCategoryDto, Guid, QuestionCategoryGetListInput, CreateUpdateQuestionCategoryDto, CreateUpdateQuestionCategoryDto>,
    IQuestionCategoryAppService
{

    #region 字段和构造函数
    private readonly IQuestionCategoryRepository _repository;

    public QuestionCategoryAppService(IQuestionCategoryRepository repository) : base(repository)
    {
        _repository = repository;
    }
    #endregion

    #region CRUD
    protected override IQueryable<QuestionCategory> CreateFilteredQuery(QuestionCategoryGetListInput input)
    {
        // TODO: AbpHelper generated
        return base.CreateFilteredQuery(input)
            .WhereIf(!input.Filter.IsNullOrWhiteSpace(), x => x.Name.Contains(input.Filter))
            .WhereIf(input.ParentId != null, x => x.ParentId == input.ParentId)
            .WhereIf(!input.Name.IsNullOrWhiteSpace(), x => x.Name.Contains(input.Name))
            .WhereIf(!input.Cover.IsNullOrWhiteSpace(), x => x.Cover.Contains(input.Cover))
            .WhereIf(!input.Code.IsNullOrWhiteSpace(), x => x.Code.Contains(input.Code))
            .WhereIf(input.Enable != null, x => x.Enable == input.Enable)
            .WhereIf(input.IsPublic != null, x => x.IsPublic == input.IsPublic)
            ;
    }


    public ListResultDto<QuestionCategoryDto> GetListByParentId(QuestionCategoryGetListInput input)
    {
        var questionCategories = _repository.WhereIf(input.ParentId !=null, x => x.ParentId == input.ParentId)
                .ToList();
        return new ListResultDto<QuestionCategoryDto>(
            ObjectMapper.Map<List<QuestionCategory>, List<QuestionCategoryDto>>(questionCategories));

    }


    public ListResultDto<QuestionCategoryDto> GetAllList(QuestionCategoryGetListInput input)
    {
        var questionCategories = CreateFilteredQuery(input).ToList();
        return new ListResultDto<QuestionCategoryDto>(
            ObjectMapper.Map<List<QuestionCategory>, List<QuestionCategoryDto>>(questionCategories));
    }

    public override async Task<QuestionCategoryDto> CreateAsync(CreateUpdateQuestionCategoryDto input)
    {
        var questionCategory = _repository.Where(x => x.Name == input.Name).FirstOrDefault();
        var pid = input.ParentId;
        if (questionCategory != null)
        {
            throw new UserFriendlyException(L["DuplicateQuestionCategory", input.Name]);
        }

        return await base.CreateAsync(input);
    }

    public override async Task<QuestionCategoryDto> UpdateAsync(Guid id, CreateUpdateQuestionCategoryDto input)
    {
        var questionCategory = _repository.Where(x => x.Name == input.Name).FirstOrDefault();
        if (questionCategory != null && questionCategory.Id != id)
        {
            throw new UserFriendlyException(L["DuplicateQuestionCategory", input.Name]);
        }

        return await base.UpdateAsync(id, input);
    }

    /// <summary>
    /// 删除数据
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public override async Task DeleteAsync(Guid id)
    {
        /*
         TODO:
        1. 如果父类关联删除子类 
        1. 删除分类关联的题目
         
         */
        await base.DeleteAsync(id);
    }
    #endregion


    #region 导入/导出 XLSX

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
            var properties = ImportManager.GetPropertiesByExcelCells<QuestionCategoryDto>(worksheet);

            var manager = new PropertyManager<QuestionCategoryDto>(properties);
            var iRow = 2;

            while (true)
            {
                var allColumnsAreEmpty = manager.GetProperties
                    .Select(property => worksheet.Row(iRow).Cell(property.PropertyOrderPosition))
                    .All(cell => cell?.Value == null || string.IsNullOrEmpty(cell.Value.ToString()));

                if (allColumnsAreEmpty)
                    break;

                manager.ReadFromXlsx(worksheet, iRow);

                var model = await _repository.GetAsync(manager.GetProperty("Id").GuidValue);

                var isNew = model == null;


                model ??= new QuestionCategory(
                    GuidGenerator.Create(),
                    CurrentTenant.Id,
                    manager.GetProperty("ParentId").GuidValue,
                    manager.GetProperty("Name").StringValue,
                    manager.GetProperty("Cover").StringValue,
                    manager.GetProperty("Code").StringValue,
                    manager.GetProperty("Enable").BooleanValue,
                    manager.GetProperty("Sorting").IntValue,
                    manager.GetProperty("IsPublic").BooleanValue
                    );

                foreach (var property in manager.GetProperties)
                {
                    if (property.PropertyName == L["DisplayName:ParentId"]) model.ParentId = property.GuidValue;
                    if (property.PropertyName == L["DisplayName:Name"]) model.Name = property.StringValue;
                    if (property.PropertyName == L["DisplayName:Cover"]) model.Cover = property.StringValue;
                    if (property.PropertyName == L["DisplayName:Enable"]) model.Enable = property.BooleanValue;
                    if (property.PropertyName == L["DisplayName:Sorting"]) model.Sorting = property.IntValue;
                    if (property.PropertyName == L["DisplayName:IsPublic"]) model.IsPublic = property.BooleanValue;
                }

                if (isNew)
                    await _repository.InsertAsync(model);
                else
                    await _repository.UpdateAsync(model);

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
    /// <param name="QuestionCategoryGetListInput">input</param>
    /// <returns>查询到的所有角色</returns>
    public virtual async Task<IActionResult> ExportToXlsxAsync(QuestionCategoryGetListInput input)
    {
        var list = CreateFilteredQuery(input).ToList();

        //property manager 
        var manager = new PropertyManager<QuestionCategory>(new[]
        {
                new PropertyByName<QuestionCategory>("Id", p => p.Id),
                new PropertyByName<QuestionCategory>(L["ParentId"], p => p.ParentId),
                new PropertyByName<QuestionCategory>(L["DisplayName:Name"], p => p.Name),
                new PropertyByName<QuestionCategory>(L["DisplayName:Cover"], p => p.Cover),
                new PropertyByName<QuestionCategory>(L["DisplayName:Enable"], p => p.Enable),
                new PropertyByName<QuestionCategory>(L["DisplayName:Sorting"], p => p.Sorting),
                new PropertyByName<QuestionCategory>(L["DisplayName:IsPublic"], p => p.IsPublic),
            });

        var bytes = await manager.ExportToXlsxAsync(list);

        return new FileContentResult(bytes, MimeTypes.TextXlsx);
    } 
    #endregion

}
