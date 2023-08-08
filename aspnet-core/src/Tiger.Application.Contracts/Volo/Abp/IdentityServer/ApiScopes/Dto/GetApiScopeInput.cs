using Volo.Abp.Application.Dtos;

namespace Tiger.Volo.Abp.IdentityServer.ApiScopes
{
    public class GetApiScopeInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
