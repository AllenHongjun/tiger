﻿using System;
using System.Collections.Generic;
using System.Text;
using Tiger.Module.OssManagement.Localization;
using Volo.Abp.Features;
using Volo.Abp.Localization;
using Volo.Abp.Validation.StringValues;

namespace Tiger.Module.OssManagement.Features
{
    /// <summary>
    /// Oss 功能特征定义
    /// </summary>
    /// <remarks>
    /// Features https://docs.abp.io/zh-Hans/abp/latest/Features
    /// </remarks>
    public class AbpOssManagementFeatureDefinitionProvider : FeatureDefinitionProvider
    {
        public override void Define(IFeatureDefinitionContext context)
        {
            var featureGroup = context.AddGroup(
                name: AbpOssManagementFeatureNames.GroupName,
                displayName: L("Features:OssManagement"));

            featureGroup.AddFeature(
                name: AbpOssManagementFeatureNames.PublicAccess,
                defaultValue: false.ToString(),
                displayName: L("Features:DisplayName:PublicAccess"),
                description: L("Features:Description:PublicAccess"),
                valueType: new ToggleStringValueType(new BooleanValueValidator()));

            var ossFeature = featureGroup.AddFeature(
                name: AbpOssManagementFeatureNames.OssObject.Default,
                defaultValue: true.ToString(),
                displayName: L("Features:DisplayName:OssObject"),
                description: L("Features:Description:OssObject"),
                valueType: new ToggleStringValueType(new BooleanValueValidator()));

            ossFeature.CreateChild(
                name: AbpOssManagementFeatureNames.OssObject.DownloadFile,
                defaultValue: false.ToString(),
                displayName: L("Features:DisplayName:DownloadFile"),
                description: L("Features:Description:DownloadFile"),
                valueType: new ToggleStringValueType(new BooleanValueValidator()));
            ossFeature.CreateChild(
                name: AbpOssManagementFeatureNames.OssObject.DownloadLimit,
                defaultValue: "1000",
                displayName: L("Features:DisplayName:DownloadLimit"),
                description: L("Features:Description:DownloadLimit"),
                valueType: new FreeTextStringValueType(new NumericValueValidator(0, 100_0000))); // 上限100万次调用
            ossFeature.CreateChild(
                name: AbpOssManagementFeatureNames.OssObject.DownloadInterval,
                defaultValue: "1",
                displayName: L("Features:DisplayName:DownloadInterval"),
                description: L("Features:Description:DownloadInterval"),
                valueType: new FreeTextStringValueType(new NumericValueValidator(1, 12))); // 上限12月

            ossFeature.CreateChild(
                name: AbpOssManagementFeatureNames.OssObject.UploadFile,
                defaultValue: true.ToString(),
                displayName: L("Features:DisplayName:UploadFile"),
                description: L("Features:Description:UploadFile"),
                valueType: new ToggleStringValueType(new BooleanValueValidator()));
            ossFeature.CreateChild(
                name: AbpOssManagementFeatureNames.OssObject.UploadLimit,
                defaultValue: "1000",
                displayName: L("Features:DisplayName:UploadLimit"),
                description: L("Features:Description:UploadLimit"),
                valueType: new FreeTextStringValueType(new NumericValueValidator(0, 100_0000))); // 上限100万次调用
            ossFeature.CreateChild(
                name: AbpOssManagementFeatureNames.OssObject.UploadInterval,
                defaultValue: "1",
                displayName: L("Features:DisplayName:UploadInterval"),
                description: L("Features:Description:UploadInterval"),
                valueType: new FreeTextStringValueType(new NumericValueValidator(1, 12))); // 上限12月

            // TODO: 此功能需要控制器协同,暂时不实现
            //fileSystemFeature.CreateChild(
            //    name: AbpOssManagementFeatureNames.OssObject.MaxUploadFileCount,
            //    defaultValue: 1.ToString(),
            //    displayName: L("Features:DisplayName:MaxUploadFileCount"),
            //    description: L("Features:Description:MaxUploadFileCount"),
            //    valueType: new FreeTextStringValueType(new NumericValueValidator(1, 10)));
        }

        protected ILocalizableString L(string name)
        {
            return LocalizableString.Create<AbpOssManagementResource>(name);
        }
    }
}
