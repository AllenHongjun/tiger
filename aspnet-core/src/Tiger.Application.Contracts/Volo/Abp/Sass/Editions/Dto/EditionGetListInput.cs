using Volo.Abp.Application.Dtos;

namespace Tiger.Volo.Abp.Sass.Editions;

public class EditionGetListInput : PagedAndSortedResultRequestDto
{
    public string Filter { get; set; }
}
