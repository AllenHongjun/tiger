using System;
using Tiger.Permissions;
using Tiger.Marketing.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp;

namespace Tiger.Marketing
{
    [RemoteService(false)]
    public class FlashPromotionAppService : CrudAppService<FlashPromotion, FlashPromotionDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateFlashPromotionDto, CreateUpdateFlashPromotionDto>,
        IFlashPromotionAppService
    {
        //protected override string GetPolicyName { get; set; } = TigerPermissions.FlashPromotion.Default;
        //protected override string GetListPolicyName { get; set; } = TigerPermissions.FlashPromotion.Default;
        //protected override string CreatePolicyName { get; set; } = TigerPermissions.FlashPromotion.Create;
        //protected override string UpdatePolicyName { get; set; } = TigerPermissions.FlashPromotion.Update;
        //protected override string DeletePolicyName { get; set; } = TigerPermissions.FlashPromotion.Delete;

        private readonly IFlashPromotionRepository _repository;
        
        public FlashPromotionAppService(IFlashPromotionRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
