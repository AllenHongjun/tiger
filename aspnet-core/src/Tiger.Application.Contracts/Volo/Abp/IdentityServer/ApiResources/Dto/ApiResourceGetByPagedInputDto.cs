using Volo.Abp.Application.Dtos;

namespace Tiger.Volo.Abp.IdentityServer.ApiResources
{
    public class ApiResourceGetByPagedInputDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
