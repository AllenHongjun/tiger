using Volo.Abp.Application.Dtos;

namespace Tiger.Volo.Abp.IdentityServer.IdentityResources
{
    public class IdentityResourceGetByPagedDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
