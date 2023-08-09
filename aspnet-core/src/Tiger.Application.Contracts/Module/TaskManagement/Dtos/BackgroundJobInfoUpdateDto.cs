using Volo.Abp.Domain.Entities;

namespace Tiger.Module.TaskManagement.Dtos;

public class BackgroundJobInfoUpdateDto : BackgroundJobInfoCreateOrUpdateDto, IHasConcurrencyStamp
{
    public string ConcurrencyStamp { get; set; }
}
