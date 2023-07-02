using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Validation;

namespace Tiger.Volo.Abp.Sass.Tenants;

public class TenantConnectionGetByNameInput
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    [DynamicStringLength(typeof(TenantConnectionStringConsts), nameof(TenantConnectionStringConsts.MaxNameLength))]
    public string Name { get; set; }
}
