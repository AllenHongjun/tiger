using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Tiger.Permissions;
using Tiger.Module.Exams.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.ObjectMapping;
using System.Collections.Generic;
using Volo.Abp.SettingManagement;

namespace Tiger.Module.Exams;


/// <summary>
/// 试卷大题
/// </summary>
[RemoteService(IsEnabled = false)]
public class TestPaperSectionAppService : CrudAppService<TestPaperSection, TestPaperSectionDto, Guid, TestPaperSectionGetListInput, CreateUpdateTestPaperSectionDto, CreateUpdateTestPaperSectionDto>,
    ITestPaperSectionAppService
{

    private readonly ITestPaperSectionRepository _repository;

    public TestPaperSectionAppService(ITestPaperSectionRepository repository) : base(repository)
    {
        _repository = repository;
    }

    protected override  IQueryable<TestPaperSection> CreateFilteredQuery(TestPaperSectionGetListInput input)
    {
        // TODO: AbpHelper generated
        return base.CreateFilteredQuery(input)
            .WhereIf(input.TestPaperId != null, x => x.TestPaperId == input.TestPaperId)
            .WhereIf(!input.Name.IsNullOrWhiteSpace(), x => x.Name.Contains(input.Name))
            .WhereIf(!input.Description.IsNullOrWhiteSpace(), x => x.Description.Contains(input.Description))
            .WhereIf(input.QuestionCount != null, x => x.QuestionCount == input.QuestionCount)
            .WhereIf(input.TotalScore != null, x => x.TotalScore == input.TotalScore)
            ;
    }

    /// <summary>
    /// 查询所有
    /// </summary>
    /// <returns></returns>
    public ListResultDto<TestPaperSectionDto> GetAll(TestPaperSectionGetListInput input)
    {
        var testPaperSections =  CreateFilteredQuery(input)
            .OrderBy(input.Sorting ?? "sort asc").ToList();
        return new ListResultDto<TestPaperSectionDto>(
            ObjectMapper.Map<List<TestPaperSection>, List<TestPaperSectionDto>>(testPaperSections));
    }

    /// <summary>
    /// 删除大题
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public override async Task DeleteAsync(Guid id)
    {   
        //base.DeleteAsync(id);
         var testPaperSection =  await _repository.GetAsync(id, true);
         testPaperSection.Questions.Clear();
         testPaperSection.Strategies.Clear();
         await _repository.DeleteAsync(id);
         await CurrentUnitOfWork.SaveChangesAsync();
    }


    /// <summary>
    /// 下移大题
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task MoveDownAsync(Guid id)
    {
        var testPaperSection = await _repository.GetAsync(id);

        // 将相邻的下一题顺序减1  (把数据库查询的语句需要做一个封装)
        var nextTestPaperSection =  _repository.FirstOrDefault(x => x.TestPaperId == testPaperSection.TestPaperId && x.Sort == (testPaperSection.Sort + 1));
        if (nextTestPaperSection != null)
        {
            nextTestPaperSection.Sort -= 1;
            await _repository.UpdateAsync(nextTestPaperSection);
        }
        testPaperSection.Sort += 1;
        await _repository.UpdateAsync(testPaperSection);
        await CurrentUnitOfWork.SaveChangesAsync();
    }

    // 修改名称

    // 批量设置题目分数
}
