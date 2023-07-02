using System.ComponentModel.DataAnnotations;
using Volo.Abp.Validation;

namespace Tiger.Volo.Abp.Sass.Tenants;

public class TenantConnectionStringCreateOrUpdate
{
    [Required]
    [DynamicStringLength(typeof(TenantConnectionStringConsts), nameof(TenantConnectionStringConsts.MaxNameLength))]
    public string Name { get; set; }

    [Required]
    [DynamicStringLength(typeof(TenantConnectionStringConsts), nameof(TenantConnectionStringConsts.MaxValueLength))]
    public string Value { get; set; }
}
