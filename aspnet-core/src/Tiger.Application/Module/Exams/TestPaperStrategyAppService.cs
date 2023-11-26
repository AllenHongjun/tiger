using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Tiger.Permissions;
using Tiger.Module.Exams.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp;

namespace Tiger.Module.Exams;


/// <summary>
/// 组卷策略配置表
/// </summary>
[RemoteService(IsEnabled = false)]
public class TestPaperStrategyAppService : CrudAppService<TestPaperStrategy, TestPaperStrategyDto, Guid, TestPaperStrategyGetListInput, CreateUpdateTestPaperStrategyDto, CreateUpdateTestPaperStrategyDto>,
    ITestPaperStrategyAppService
{
    private readonly ITestPaperStrategyRepository _repository;

    public TestPaperStrategyAppService(ITestPaperStrategyRepository repository) : base(repository)
    {
        _repository = repository;
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
}
