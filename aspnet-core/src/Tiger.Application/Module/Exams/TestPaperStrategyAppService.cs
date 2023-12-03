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
using Tiger.Module.QuestionBank;
using System.Collections.Generic;

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
    private readonly TestPaperSectionManager _testPaperSectionManager;
    private readonly IQuestionRepository _questionRepository;

    public TestPaperStrategyAppService(
        ITestPaperStrategyRepository repository,
        ITestPaperSectionRepository sectionRepository,
        TestPaperSectionManager testPaperSectionManager,
        IQuestionRepository questionRepository) : base(repository)
    {
        _repository = repository;
        _sectionRepository=sectionRepository;
        _testPaperSectionManager=testPaperSectionManager;
        _questionRepository=questionRepository;
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
            .WhereIf(input.SimpleCount != null, x => x.SimpleCount == input.SimpleCount)
            .WhereIf(input.OrdinaryCount != null, x => x.OrdinaryCount == input.OrdinaryCount)
            .WhereIf(input.DifficultCount != null, x => x.DifficultCount == input.DifficultCount);
    }


    public override async Task<PagedResultDto<TestPaperStrategyDto>> GetListAsync(TestPaperStrategyGetListInput input)
    {
        var testPaperStrategies = await base.GetListAsync(input);

        List<Guid> questionCategoryIds = null;
        if (testPaperStrategies.TotalCount > 0)
        {   
            // TODO:优化获取所有子类的题目数量
            questionCategoryIds = testPaperStrategies.Items.Select(x => x.QuestionCategoryId).ToList();
        }
        var differentDegreeQuestionCounts =  await _questionRepository.GetDifferentDegreeQuestionCount(questionCategoryIds, input.QuestionType);

        foreach (var item in testPaperStrategies.Items)
        {
            item.TotalSelectQuestionsCount = item.UnlimitedDifficultyCount + item.SimpleCount + item.OrdinaryCount + item.DifficultCount;

            var cateogryCount = differentDegreeQuestionCounts.FirstOrDefault(x => x.QuestionCategoryId == item.QuestionCategoryId);
            if (cateogryCount == null) continue;
            item.TotalUnlimitedDifficultyCount = cateogryCount.UnlimitedDifficultyCount;
            item.TotalSimpleCount = cateogryCount.SimpleCount;
            item.TotalOrdinaryCount = cateogryCount.OrdinaryCount;
            item.TotalDifficultCount = cateogryCount.DifficultCount;
        }
        return testPaperStrategies;
    }

    public override async Task<TestPaperStrategyDto> UpdateAsync(Guid id, CreateUpdateTestPaperStrategyDto input)
    {
        TestPaperStrategyDto testPaperStrategy = await base.UpdateAsync(id, input);
        await _testPaperSectionManager.CalcuTotalScoreAndQusetionCount(testPaperStrategy.TestPaperSectionId);
        return testPaperStrategy;
    }
}
