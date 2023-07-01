using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Features;
using Volo.Abp.Localization;
using Volo.Abp.Validation.StringValues;

namespace Tiger.Infrastructure.Features
{
    /// <summary>
    /// 定义特性
    /// </summary>
    /// <remarks>
    /// 定义特性常量应该放到 contrat 这一层当中使用
    /// 
    /// ABP会自动发现此类并注册特征.无需其他配置。
    /// 
    /// 此类通常在解决方案的项目中创建。Application.Contracts
    /// </remarks>
    public class MyFeatureDefinitionProvider : FeatureDefinitionProvider
    {
        public override void Define(IFeatureDefinitionContext context)
        {
            //var myGroup = context.AddGroup("MyApp");

            //myGroup.AddFeature("MyApp.PdfReporting", defaultValue: "false");
            //myGroup.AddFeature("MyApp.MaxProductCount", defaultValue: "10");


            //var myGroup = context.AddGroup("MyApp");

            //myGroup.AddFeature(
            //    "MyApp.PdfReporting",
            //    defaultValue: "false",
            //    //displayName: LocalizableString
            //    //                 .Create<FeaturesDemoResource>("PdfReporting"),
            //    valueType: new ToggleStringValueType()
            //);

            //myGroup.AddFeature(
            //    "MyApp.MaxProductCount",
            //    defaultValue: "10",
            //    //displayName: LocalizableString
            //    //                 .Create<FeaturesDemoResource>("MaxProductCount"),
            //    valueType: new FreeTextStringValueType(
            //                   new NumericValueValidator(0, 1000000))
            //);

            // 测试完成记得删除
            //var myGroup = context.AddGroup("MyApp");

            //var reportingFeature = myGroup.AddFeature(
            //    "MyApp.Reporting",
            //    defaultValue: "false",
            //    //displayName: LocalizableString
            //    //                 .Create<FeaturesDemoResource>("Reporting"),
            //    valueType: new ToggleStringValueType()
            //);

            //reportingFeature.CreateChild(
            //    "MyApp.PdfReporting",
            //    defaultValue: "false",
            //    //displayName: LocalizableString
            //    //                 .Create<FeaturesDemoResource>("PdfReporting"),
            //    valueType: new ToggleStringValueType()
            //);

            //reportingFeature.CreateChild(
            //    "MyApp.ExcelReporting",
            //    defaultValue: "false",
            //    //displayName: LocalizableString
            //    //                 .Create<FeaturesDemoResource>("ExcelReporting"),
            //    valueType: new ToggleStringValueType()
            //);
        }
    }
}
