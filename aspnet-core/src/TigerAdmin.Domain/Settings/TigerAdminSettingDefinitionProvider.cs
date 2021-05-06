using Volo.Abp.Settings;

namespace TigerAdmin.Settings
{
    public class TigerAdminSettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            //Define your own settings here. Example:
            //context.Add(new SettingDefinition(TigerAdminSettings.MySetting1));
        }
    }
}
