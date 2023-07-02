using Volo.Abp.Application.Dtos;

namespace Tiger.Volo.Abp.Sass.Tenants;

public class TenantGetListInput : PagedAndSortedResultRequestDto
{
    public string Filter { get; set; }
}