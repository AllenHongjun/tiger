using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Linq;
using Tiger.Module.QuestionBank.Dtos;
using Tiger.Module.System.Platform.Layouts.Dto;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tiger.Module.QuestionBank;


/// <summary>
/// 实训平台
/// </summary>
[RemoteService(IsEnabled = false)]
public class TrainPlatformAppService : CrudAppService<TrainPlatform, TrainPlatformDto, Guid, TrainPlatformGetListInput, CreateUpdateTrainPlatformDto, CreateUpdateTrainPlatformDto>,
    ITrainPlatformAppService
{

    private readonly ITrainPlatformRepository _repository;

    public TrainPlatformAppService(ITrainPlatformRepository repository) : base(repository)
    {
        _repository = repository;
    }

    protected override IQueryable<TrainPlatform> CreateFilteredQuery(TrainPlatformGetListInput input)
    {
        // TODO: AbpHelper generated
        return base.CreateFilteredQuery(input)
            .WhereIf(!input.filter.IsNullOrWhiteSpace(), x => x.Name.Contains(input.filter) || x.Url.Contains(input.filter) || x.Url.Contains(input.filter) || x.CheckCode.Contains(input.filter))
            .WhereIf(input.CreateStartTime != null, x => x.CreationTime >= input.CreateStartTime)
            .WhereIf(input.CreateEndTime != null, x => x.CreationTime <= input.CreateEndTime)
            .WhereIf(!input.Name.IsNullOrWhiteSpace(), x => x.Name.Contains(input.Name))
            .WhereIf(input.TokenType != null, x => x.TokenType == input.TokenType)
            .WhereIf(input.Enable != null, x => x.Enable == input.Enable)
            ;
    }

    public ListResultDto<TrainPlatformDto> GetAll() {
        var trainPlatforms = _repository.Where(x => x.Enable == true).ToList();

        return new ListResultDto<TrainPlatformDto>(
                ObjectMapper.Map<List<TrainPlatform>, List<TrainPlatformDto>>(trainPlatforms));
    }
}
