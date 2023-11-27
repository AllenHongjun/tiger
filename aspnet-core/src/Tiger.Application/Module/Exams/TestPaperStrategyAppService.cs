using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Tiger.Permissions;
using Tiger.Module.Exams.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp;
using Tiger.Module.QuestionBank.Dtos;
using Volo.Abp.Application.Dtos;

namespace Tiger.Module.Exams;


/// <summary>
/// 组卷策略配置表
/// </summary>
[RemoteService(IsEnabled = false)]
public class TestPaperStrategyAppService : CrudAppService<TestPaperStrategy, TestPaperStrategyDto, Guid, TestPaperStrategyGetListInput, CreateUpdateTestPaperStrategyDto, CreateUpdateTestPaperStrategyDto>,
    ITestPaperStrategyAppService
{
    private readonly ITestPaperStrategyRepository _repository;
    private readonly ITestPaperSectionRepository _sectionRepository;

    public TestPaperStrategyAppService(
        ITestPaperStrategyRepository repository, 
        ITestPaperSectionRepository sectionRepository) : base(repository)
    {
        _repository = repository;
        _sectionRepository=sectionRepository;
    }

    protected override  IQueryable<TestPaperStrategy> CreateFilteredQuery(TestPaperStrategyGetListInput input)
    {
        // TODO: AbpHelper generated
        return base.CreateFilteredQuery(input)
            .WhereIf(input.TestPaperId != null, x => x.TestPaperId == input.TestPaperId)
            .WhereIf(input.TestPaperSectionId != null, x => x.TestPaperSectionId == input.TestPaperSectionId)
            .WhereIf(input.QuestionCategoryId != null, x => x.QuestionCategoryId == input.QuestionCategoryId)
            .WhereIf(input.QuestionType != null, x => x.QuestionType == input.QuestionType)
            .WhereIf(input.UnlimitedDifficultyCount != null, x => x.UnlimitedDifficultyCount == input.UnlimitedDifficultyCount)
            .WhereIf(input.EasyCount != null, x => x.EasyCount == input.EasyCount)
            .WhereIf(input.OrdinaryCount != null, x => x.OrdinaryCount == input.OrdinaryCount)
            .WhereIf(input.DifficultCount != null, x => x.DifficultCount == input.DifficultCount);
    }


    public override async Task<PagedResultDto<TestPaperStrategyDto>> GetListAsync(TestPaperStrategyGetListInput input)
    {
        var testPaperStrategies = await base.GetListAsync(input);
        foreach (var item in testPaperStrategies.Items)
        {
            item.TotalSelectQuestionsCount = item.UnlimitedDifficultyCount + item.EasyCount + item.OrdinaryCount + item.DifficultCount;
        }
        return testPaperStrategies;
    }

    public override async Task<TestPaperStrategyDto> UpdateAsync(Guid id, CreateUpdateTestPaperStrategyDto input)
    {
        TestPaperStrategyDto testPaperStrategy = await base.UpdateAsync(id, input);

        // 计算试卷大题题目数量和题目总分
        var testPaperSection = await _sectionRepository.GetAsync(testPaperStrategy.TestPaperSectionId);
        var testPaperStrategies =  _repository.Where(x => x.TestPaperSectionId == testPaperStrategy.TestPaperSectionId).ToList();
        testPaperSection.QuestionCount = testPaperStrategies.Sum(x => x.UnlimitedDifficultyCount + x.EasyCount + x.OrdinaryCount + x.DifficultCount);
        testPaperSection.TotalScore = testPaperStrategies
            .Sum(x => (x.UnlimitedDifficultyCount + x.EasyCount + x.OrdinaryCount + x.DifficultCount) * x.ScorePerQuestion);
        await _sectionRepository.UpdateAsync(testPaperSection);
        await CurrentUnitOfWork.SaveChangesAsync();
        return testPaperStrategy;
    }
}
