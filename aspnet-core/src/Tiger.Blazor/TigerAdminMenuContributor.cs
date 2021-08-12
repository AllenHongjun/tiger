using System.Threading.Tasks;
using TigerAdmin.Localization;
using Volo.Abp.UI.Navigation;

namespace TigerAdmin.Blazor
{
    public class TigerAdminMenuContributor : IMenuContributor
    {
        public Task ConfigureMenuAsync(MenuConfigurationContext context)
        {
            if(context.Menu.DisplayName != StandardMenus.Main)
            {
                return Task.CompletedTask;
            }

            var l = context.GetLocalizer<TigerAdminResource>();

            context.Menu.Items.Insert(
                0,
                new ApplicationMenuItem(
                    "TigerAdmin.Home",
                    l["Menu:Home"],
                    "/",
                    icon: "fas fa-home"
                )
            );

            return Task.CompletedTask;
        }
    }
}
