using System;
using System.Linq;
using System.Threading.Tasks;
using Tiger.Permissions;
using Tiger.Module.System.Localization.Dtos;
using Volo.Abp.Application.Services;

namespace Tiger.Module.System.Localization;


public class ResourceAppService : IApplicationService,IResourceAppService
{
    

    private readonly IResourceRepository _repository;

    public ResourceAppService(IResourceRepository repository) 
    {
        _repository = repository;
    }

    //protected override async Task<IQueryable<Resource>> CreateFilteredQueryAsync(ResourceGetListInput input)
    //{
    //    // TODO: AbpHelper generated
    //    return (await base.CreateFilteredQueryAsync(input))
    //        .WhereIf(input.Enable != null, x => x.Enable == input.Enable)
    //        .WhereIf(input.Name != null, x => x.Name == input.Name)
    //        .WhereIf(!input.DisplayName.IsNullOrWhiteSpace(), x => x.DisplayName.Contains(input.DisplayName))
    //        .WhereIf(!input.Description.IsNullOrWhiteSpace(), x => x.Description.Contains(input.Description))
    //        ;
    //}
}
