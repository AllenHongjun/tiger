using System;
using Tiger.Permissions;
using Tiger.Marketing.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tiger.Marketing
{
    public class FlashPromotionSessionAppService : CrudAppService<FlashPromotionSession, FlashPromotionSessionDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateFlashPromotionSessionDto, CreateUpdateFlashPromotionSessionDto>,
        IFlashPromotionSessionAppService
    {
        protected override string GetPolicyName { get; set; } = TigerPermissions.FlashPromotionSession.Default;
        protected override string GetListPolicyName { get; set; } = TigerPermissions.FlashPromotionSession.Default;
        protected override string CreatePolicyName { get; set; } = TigerPermissions.FlashPromotionSession.Create;
        protected override string UpdatePolicyName { get; set; } = TigerPermissions.FlashPromotionSession.Update;
        protected override string DeletePolicyName { get; set; } = TigerPermissions.FlashPromotionSession.Delete;

        private readonly IFlashPromotionSessionRepository _repository;
        
        public FlashPromotionSessionAppService(IFlashPromotionSessionRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
