using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tiger.Volo.Abp.IdentityServer.Grants;
using Tiger.Volo.Abp.IdentityServer.Grants.Dto;
using Tiger.Volo.Abp.IdentityServer.IdentityResources;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.IdentityServer;
using Volo.Abp.IdentityServer.Grants;

namespace Tiger.Volo.Abp.IdentityServer
{
    /// <summary>
    /// 持续授予服务
    /// </summary>
    [RemoteService(false)]
    public class PersistedGrantAppService :
        CrudAppService<
            PersistedGrant,  // Entity
            PersistedGrantDto, // Dto
            Guid, //Primary key 
            GetPersistedGrantInput, //Used for paging/sorting
            CreateUpdatePersistedGrantDto>, IPersistedGrantAppService
    {   
        protected ITigerPersistentGrantRepository PersistentGrantRepository { get;}

        public PersistedGrantAppService(
            IRepository<PersistedGrant, Guid> repository,
            ITigerPersistentGrantRepository persistentGrantRepository) : base(repository)
        {
            PersistentGrantRepository = persistentGrantRepository;
        }


        public override async Task DeleteAsync(Guid id)
        {
            var persistedGrand = await PersistentGrantRepository.GetAsync(id);

            await PersistentGrantRepository.DeleteAsync(persistedGrand);

            await CurrentUnitOfWork.SaveChangesAsync();
        }

        public override async Task<PersistedGrantDto> GetAsync(Guid id)
        {
            var persistedGrant = await PersistentGrantRepository.GetAsync(id);

            return ObjectMapper.Map<PersistedGrant,PersistedGrantDto>(persistedGrant);
        }


        public override async Task<PagedResultDto<PersistedGrantDto>> GetListAsync(GetPersistedGrantInput input)
        {
            var persistedGrantCount = await PersistentGrantRepository
                .GetCountAsync(input.SubjectId,input.Filter);

            var persistedGrantList = await PersistentGrantRepository
                .GetListAsync(input.SubjectId, input.Filter, input.Sorting);
            return new PagedResultDto<PersistedGrantDto>(persistedGrantCount,
                ObjectMapper.Map<List<PersistedGrant>, List<PersistedGrantDto>>(persistedGrantList));
           
        }

    }
}
