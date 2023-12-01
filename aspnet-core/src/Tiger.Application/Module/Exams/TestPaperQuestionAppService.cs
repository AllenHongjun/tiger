using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tiger.Module.Exams.Dtos;
using Tiger.Module.QuestionBank;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tiger.Module.Exams;


/// <summary>
/// 试卷内容(题目)表
/// </summary>
[RemoteService(IsEnabled = false)]
public class TestPaperQuestionAppService : CrudAppService<TestPaperQuestion, TestPaperQuestionDto, Guid, TestPaperQuestionGetListInput, CreateUpdateTestPaperQuestionDto, CreateUpdateTestPaperQuestionDto>,
    ITestPaperQuestionAppService
{
    #region 字段和构造函数
    private readonly ITestPaperQuestionRepository _repository;
    private readonly IQuestionRepository _questionRepository;
    private readonly TestPaperSectionManager _testPaperSectionManager;

    public TestPaperQuestionAppService(
        ITestPaperQuestionRepository repository,
        IQuestionRepository questionRepository,
        TestPaperSectionManager testPaperSectionManager) : base(repository)
    {
        _repository = repository;
        _questionRepository=questionRepository;
        _testPaperSectionManager=testPaperSectionManager;
    } 
    #endregion

    #region CRUD
    protected override IQueryable<TestPaperQuestion> CreateFilteredQuery(TestPaperQuestionGetListInput input)
    {
        // TODO: AbpHelper generated
        return base.CreateFilteredQuery(input)
            .WhereIf(input.TestPaperId != null, x => x.TestPaperId == input.TestPaperId)
            .WhereIf(input.QuestionCategoryId != null, x => x.QuestionCategoryId == input.QuestionCategoryId)
            .WhereIf(input.QuestionId != null, x => x.QuestionId == input.QuestionId)
            .WhereIf(input.TestPaperType != null, x => x.TestPaperType == input.TestPaperType)
            .WhereIf(input.QuestionDegree != null, x => x.QuestionDegree == input.QuestionDegree)
            .WhereIf(input.Score != null, x => x.Score == input.Score)
            ;
    }

    public override async Task<TestPaperQuestionDto> UpdateAsync(Guid id, CreateUpdateTestPaperQuestionDto input)
    {
        var testPaperQuestion = await base.UpdateAsync(id, input);

        return testPaperQuestion;
    }

    /// <summary>
    /// 查询指定大题id的所有题目
    /// </summary>
    /// <param name="testPaperSectionId"></param>
    /// <returns></returns>
    public async Task<ListResultDto<TestPaperQuestionDto>> GetAllAsync(Guid testPaperSectionId)
    {
        var testPaperQuestions = await _repository.GetAllListAsync(testPaperSectionId);

        return new ListResultDto<TestPaperQuestionDto>(
            ObjectMapper.Map<List<TestPaperQuestion>, List<TestPaperQuestionDto>>(testPaperQuestions));
    }

    public override async Task DeleteAsync(Guid id)
    {
        await base.DeleteAsync(id);

        var testPaperQuestion = await _repository.GetAsync(id);
        await _testPaperSectionManager.CalcuTotalScoreAndQusetionCount(testPaperQuestion.TestPaperSectionId);
    }
    #endregion


    #region 从题库中选题
    /// <summary>
    /// 确认选中试卷题目（手动选题）
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task ComfirmSelect(TestPaperQuestionComfirmSelectDto input)
    {
        var questionIds = input.Questions.Select(x => x.QuestionId).ToList();
        var questions = _questionRepository.Where(x => questionIds.Contains(x.Id)).ToList();
        foreach (var question in questions)
        {
            TestPaperQuestion testPaperQuestion = new TestPaperQuestion(
                GuidGenerator.Create(),
                CurrentTenant.Id,
                input.TestPaperId,
                input.TestPaperSectionId,
                question.QuestionCategoryId,
                question.Id,
                TestPaperType.FixedQuestions,
                question.Degree,
                0,
                question.Score
                );
            await _repository.InsertAsync(testPaperQuestion);
        }
        await CurrentUnitOfWork.SaveChangesAsync();

        await _testPaperSectionManager.CalcuTotalScoreAndQusetionCount(input.TestPaperSectionId);
    }

    /// <summary>
    /// 随机选题
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task RandomSelentQuestions(TestPaperQuestionRandomSelectDto input)
    {
        // 查询不同难度指定数量的随机题目
        var unlimitedDifficultyQuestions = await _questionRepository.GetRandomList(input.QuestionCategoryId, input.QuestionType, QuestionDegree.UnlimitedDifficultyCount, input.UnlimitedDifficultyCount);
        var simpleQuestions = await _questionRepository.GetRandomList(input.QuestionCategoryId, input.QuestionType, QuestionDegree.Simple, input.SimpleCount);
        var ordinaryQuestions = await _questionRepository.GetRandomList(input.QuestionCategoryId, input.QuestionType, QuestionDegree.Ordinary, input.OrdinaryCount);
        var difficultQuestions = await _questionRepository.GetRandomList(input.QuestionCategoryId, input.QuestionType, QuestionDegree.Difficult, input.DifficultCount);
        var randomQuestions = unlimitedDifficultyQuestions.Concat(simpleQuestions)
                                .Concat(ordinaryQuestions)
                                .Concat(difficultQuestions);
        // 将题目添加到试卷当中
        foreach (var question in randomQuestions)
        {
            TestPaperQuestion testPaperQuestion = new TestPaperQuestion(
                GuidGenerator.Create(),
                CurrentTenant.Id,
                input.TestPaperId,
                input.TestPaperSectionId,
                question.QuestionCategoryId,
                question.Id,
                TestPaperType.FixedQuestions,
                question.Degree,
                0, //TODO: 顺序不写死 获取当前大题的题目顺序最大值，然后累加1
                question.Score
                );
            await _repository.InsertAsync(testPaperQuestion);
        }
        await CurrentUnitOfWork.SaveChangesAsync();

        await _testPaperSectionManager.CalcuTotalScoreAndQusetionCount(input.TestPaperSectionId);
    }

    #endregion

}
