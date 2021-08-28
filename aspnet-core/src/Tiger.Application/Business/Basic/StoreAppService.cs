using System;
using Tiger.Permissions;
using Tiger.Basic.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp;

namespace Tiger.Basic
{
    [RemoteService(false)]
    public class StoreAppService : CrudAppService<Store, StoreDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateStoreDto, CreateUpdateStoreDto>,
        IStoreAppService
    {
        //protected override string GetPolicyName { get; set; } = TigerPermissions.Store.Default;
        //protected override string GetListPolicyName { get; set; } = TigerPermissions.Store.Default;
        //protected override string CreatePolicyName { get; set; } = TigerPermissions.Store.Create;
        //protected override string UpdatePolicyName { get; set; } = TigerPermissions.Store.Update;
        //protected override string DeletePolicyName { get; set; } = TigerPermissions.Store.Delete;

        private readonly IStoreRepository _repository;
        
        public StoreAppService(IStoreRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
