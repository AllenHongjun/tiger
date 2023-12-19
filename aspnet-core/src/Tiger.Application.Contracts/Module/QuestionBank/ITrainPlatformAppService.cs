using System;
using Tiger.Module.QuestionBank.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tiger.Module.QuestionBank;


/// <summary>
/// 实训平台
/// </summary>
public interface ITrainPlatformAppService :
    ICrudAppService<
                TrainPlatformDto,
        Guid,
        TrainPlatformGetListInput,
        CreateUpdateTrainPlatformDto,
        CreateUpdateTrainPlatformDto>
{
    ListResultDto<TrainPlatformDto> GetAll();
}