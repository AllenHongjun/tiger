using System;
using System.Collections.Generic;
using System.Text;
using Tiger.Localization;
using Volo.Abp.Features;
using Volo.Abp.Identity.Localization;
using Volo.Abp.Localization;
using Volo.Abp.Validation.StringValues;

namespace Tiger.Features
{
    public class AbpFeatureDefinitionProvider : FeatureDefinitionProvider
    {
        public override void Define(IFeatureDefinitionContext context)
        {
            var group = context.AddGroup(AbpFeatures.GroupName);

            group.AddFeature(AbpFeatures.SocialLogins, "true", L("Feature:SocialLogins")
                , valueType: new ToggleStringValueType());
            group.AddFeature(AbpFeatures.UserCount, "10", L("Feature:UserCount")
                , valueType: new FreeTextStringValueType(new NumericValueValidator(1, 1000)));


            var myGroup = context.AddGroup("MyApp");

            myGroup.AddFeature("MyApp.PdfReporting", defaultValue: "false");
            myGroup.AddFeature("MyApp.MaxProductCount", defaultValue: "10");
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<TigerResource>(name);
        }
    }
}
