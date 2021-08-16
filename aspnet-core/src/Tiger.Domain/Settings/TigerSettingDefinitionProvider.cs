using Volo.Abp.Settings;

namespace Tiger.Settings
{
    public class TigerSettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            //Define your own settings here. Example:
            //context.Add(new SettingDefinition(TigerSettings.MySetting1));
        }
    }
}
