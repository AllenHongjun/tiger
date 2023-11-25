using System;
using System.Linq;
using System.Threading.Tasks;
using Tiger.Permissions;
using Tiger.Module.Exams.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp;

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
            .WhereIf(input.Sort != null, x => x.Sort == input.Sort)
            ;
    }

    /// <summary>
    /// 删除大题
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public override async Task DeleteAsync(Guid id)
    {   
         var testPaperSection =  await _repository.GetAsync(id, true);
         testPaperSection.Questions.Clear();
         testPaperSection.Strategies.Clear();
         await _repository.DeleteAsync(id);
         await CurrentUnitOfWork.SaveChangesAsync();
    }

    // 下移大题

    // 修改名称

    // 批量设置题目分数
}
