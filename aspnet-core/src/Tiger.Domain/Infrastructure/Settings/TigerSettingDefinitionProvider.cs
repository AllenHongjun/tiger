using Volo.Abp.Settings;

namespace Tiger.Settings
{
    public class TigerSettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            //Define your own settings here. Example:
            //context.Add(new SettingDefinition(TigerSettings.MySetting1));

            context.Add(
                new SettingDefinition("App.UI.LayoutType", "LeftMenu"),
                new SettingDefinition("App.UI.MenuType", "Righnt"),
                new SettingDefinition("App.UI.BackgroundColor","Black")
            );



            context.Add(
                new SettingDefinition("Qiniu.Oss.AccessKey", "LeftMenu"),
                new SettingDefinition("Qiniu.Oss.SecretKey", "Righnt12"),
                new SettingDefinition("Qiniu.Oss.Bucket", "blog-hongjy")
            );


        }
    }
}
