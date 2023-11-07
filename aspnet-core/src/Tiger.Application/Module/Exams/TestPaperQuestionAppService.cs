using System;
using System.Linq;
using System.Threading.Tasks;
using Tiger.Permissions;
using Tiger.Module.Exams.Dtos;
using Volo.Abp.Application.Services;

namespace Tiger.Module.Exams;


/// <summary>
/// 试卷内容(题目)表
/// </summary>
public class TestPaperQuestionAppService : CrudAppService<TestPaperQuestion, TestPaperQuestionDto, Guid, TestPaperQuestionGetListInput, CreateUpdateTestPaperQuestionDto, CreateUpdateTestPaperQuestionDto>,
    ITestPaperQuestionAppService
{
    protected override string GetPolicyName { get; set; } = TigerPermissions.TestPaperQuestion.Default;
    protected override string GetListPolicyName { get; set; } = TigerPermissions.TestPaperQuestion.Default;
    protected override string CreatePolicyName { get; set; } = TigerPermissions.TestPaperQuestion.Create;
    protected override string UpdatePolicyName { get; set; } = TigerPermissions.TestPaperQuestion.Update;
    protected override string DeletePolicyName { get; set; } = TigerPermissions.TestPaperQuestion.Delete;

    private readonly ITestPaperQuestionRepository _repository;

    public TestPaperQuestionAppService(ITestPaperQuestionRepository repository) : base(repository)
    {
        _repository = repository;
    }

    protected override IQueryable<TestPaperQuestion> CreateFilteredQuery(TestPaperQuestionGetListInput input)
    {
        // TODO: AbpHelper generated
        return base.CreateFilteredQuery(input)
            .WhereIf(input.TestPaperId != null, x => x.TestPaperId == input.TestPaperId)
            .WhereIf(input.QuestionCategoryId != null, x => x.QuestionCategoryId == input.QuestionCategoryId)
            .WhereIf(input.QuestionId != null, x => x.QuestionId == input.QuestionId)
            .WhereIf(input.TestPaperType != null, x => x.TestPaperType == input.TestPaperType)
            .WhereIf(input.QuestionDegree != null, x => x.QuestionDegree == input.QuestionDegree)
            .WhereIf(input.Sorting != null, x => x.Sorting == input.Sorting)
            .WhereIf(input.Score != null, x => x.Score == input.Score)
            .WhereIf(input.MissOptionInvalid != null, x => x.MissOptionInvalid == input.MissOptionInvalid)
            ;
    }
}
