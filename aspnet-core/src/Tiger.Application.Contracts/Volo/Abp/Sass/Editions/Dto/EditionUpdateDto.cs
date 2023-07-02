using Volo.Abp.Domain.Entities;

namespace Tiger.Volo.Abp.Sass.Editions;

public class EditionUpdateDto : EditionCreateOrUpdateBase, IHasConcurrencyStamp
{
    public string ConcurrencyStamp { get; set; }
}
