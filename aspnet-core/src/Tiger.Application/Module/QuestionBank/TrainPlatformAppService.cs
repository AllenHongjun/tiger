using System;
using System.Linq;
using System.Threading.Tasks;
using Tiger.Permissions;
using Tiger.Module.QuestionBank.Dtos;
using Volo.Abp.Application.Services;

namespace Tiger.Module.QuestionBank;


/// <summary>
/// 实训平台
/// </summary>
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
            .WhereIf(!input.Name.IsNullOrWhiteSpace(), x => x.Name.Contains(input.Name))
            .WhereIf(!input.Description.IsNullOrWhiteSpace(), x => x.Description.Contains(input.Description))
            .WhereIf(!input.Icon.IsNullOrWhiteSpace(), x => x.Icon.Contains(input.Icon))
            .WhereIf(!input.Url.IsNullOrWhiteSpace(), x => x.Url.Contains(input.Url))
            .WhereIf(!input.CheckCode.IsNullOrWhiteSpace(), x => x.CheckCode.Contains(input.CheckCode))
            .WhereIf(input.TokenType != null, x => x.TokenType == input.TokenType)
            .WhereIf(input.Enable != null, x => x.Enable == input.Enable)
            ;
    }
}
