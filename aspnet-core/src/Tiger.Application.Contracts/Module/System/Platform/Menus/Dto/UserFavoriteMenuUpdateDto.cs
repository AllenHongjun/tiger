using Volo.Abp.Domain.Entities;

namespace Tiger.Module.System.Platform.Menus.Dto;

public class UserFavoriteMenuUpdateDto : UserFavoriteMenuCreateOrUpdateDto, IHasConcurrencyStamp
{

    public string ConcurrencyStamp { get; set; }
}
