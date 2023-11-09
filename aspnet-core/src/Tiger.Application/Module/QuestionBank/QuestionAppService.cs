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

namespace Tiger.Module.QuestionBank;


/// <summary>
/// 题目
/// </summary>
[RemoteService(false)]
public class QuestionAppService : CrudAppService<Question, QuestionDto, Guid, QuestionGetListInput, CreateUpdateQuestionDto, CreateUpdateQuestionDto>,
    IQuestionAppService
{

    private readonly IQuestionRepository _repository;

    public QuestionAppService(IQuestionRepository repository) : base(repository)
    {
        _repository = repository;
    }

    protected override IQueryable<Question> CreateFilteredQuery(QuestionGetListInput input)
    {
        // TODO: AbpHelper generated
        return  base.CreateFilteredQuery(input)
            .WhereIf(!input.Filter.IsNullOrWhiteSpace(), x => x.Name.Contains(input.Filter))
            .WhereIf(input.QuestionCategoryId != null, x => x.QuestionCategoryId == input.QuestionCategoryId)
            .WhereIf(input.PracticalTrainingId != null, x => x.TrainPlatformId == input.PracticalTrainingId)
            .WhereIf(input.Type != null, x => x.Type == input.Type)
            .WhereIf(!input.Name.IsNullOrWhiteSpace(), x => x.Name.Contains(input.Name))
            .WhereIf(!input.Content.IsNullOrWhiteSpace(), x => x.Content.Contains(input.Content))
            //.WhereIf(!input.OptionContent.IsNullOrWhiteSpace(), x => x.OptionContent.Contains(input.OptionContent))
            //.WhereIf(input.OptionSize != null, x => x.OptionSize == input.OptionSize)
            .WhereIf(!input.OptionA.IsNullOrWhiteSpace(), x => x.OptionA.Contains(input.OptionA))
            .WhereIf(!input.OptionB.IsNullOrWhiteSpace(), x => x.OptionB.Contains(input.OptionB))
            .WhereIf(!input.OptionC.IsNullOrWhiteSpace(), x => x.OptionC.Contains(input.OptionC))
            .WhereIf(!input.OptionD.IsNullOrWhiteSpace(), x => x.OptionD.Contains(input.OptionD))
            .WhereIf(!input.OptionE.IsNullOrWhiteSpace(), x => x.OptionE.Contains(input.OptionE))
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

}
