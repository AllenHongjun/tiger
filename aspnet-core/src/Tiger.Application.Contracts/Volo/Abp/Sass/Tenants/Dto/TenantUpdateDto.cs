using Volo.Abp.Domain.Entities;

namespace Tiger.Volo.Abp.Sass.Tenants;
public class TenantUpdateDto : TenantCreateOrUpdateBase, IHasConcurrencyStamp
{
    public string ConcurrencyStamp { get; set; }
}