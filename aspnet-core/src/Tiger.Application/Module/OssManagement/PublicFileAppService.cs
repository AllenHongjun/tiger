using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Tiger.Module.OssManagement.Dtos;
using Tiger.Module.OssManagement.Features;
using Tiger.Module.OssManagement.Permissions;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Content;
using Volo.Abp.Features;

namespace Tiger.Module.OssManagement
{
    [RemoteService(IsEnabled = false)]
    [AllowAnonymous]
    public class PublicFileAppService : FileAppServiceBase, IPublicFileAppService
    {
        public PublicFileAppService(
           IFileUploader fileUploader,
           IFileValidater fileValidater,
           IOssContainerFactory ossContainerFactory)
           : base(fileUploader, fileValidater, ossContainerFactory)
        {
        }

        [Authorize(AbpOssManagementPermissions.OssObject.Delete)]
        public override async Task DeleteAsync(GetPublicFileInput input)
        {
            await CheckPublicAccessAsync();
            await CheckPolicyAsync(AbpOssManagementPermissions.OssObject.Delete);

            await base.DeleteAsync(input);
        }

        public override async Task UploadAsync(UploadFileChunkInput input)
        {
            await CheckPublicAccessAsync();
            await FeatureChecker.CheckEnabledAsync(AbpOssManagementFeatureNames.OssObject.UploadFile);

            await base.UploadAsync(input);
        }

        //[RequiresLimitFeature(
        //    AbpOssManagementFeatureNames.OssObject.UploadLimit,
        //    AbpOssManagementFeatureNames.OssObject.UploadInterval,
        //    LimitPolicy.Month)]
        public override async Task<OssObjectDto> UploadAsync(UploadFileInput input)
        {
            await CheckPublicAccessAsync();
            await FeatureChecker.CheckEnabledAsync(AbpOssManagementFeatureNames.OssObject.UploadFile);

            // 公共目录不允许覆盖
            input.Overwrite = false;

            return await base.UploadAsync(input);
        }

        public override async Task<ListResultDto<OssObjectDto>> GetListAsync(GetFilesInput input)
        {
            await CheckPublicAccessAsync();

            return await base.GetListAsync(input);
        }

        //[RequiresLimitFeature(
        //    AbpOssManagementFeatureNames.OssObject.DownloadLimit,
        //    AbpOssManagementFeatureNames.OssObject.DownloadInterval,
        //    LimitPolicy.Month)]
        public override async Task<IFormFile> GetAsync(GetPublicFileInput input)
        {
            await CheckPublicAccessAsync();
            await FeatureChecker.CheckEnabledAsync(AbpOssManagementFeatureNames.OssObject.DownloadFile);

            return await base.GetAsync(input);
        }

        protected override string GetCurrentBucket()
        {
            return "tiger-blob-public";
        }

        protected  virtual Task CheckPublicAccessAsync()
        {   
            return Task.CompletedTask;

            //if (!CurrentUser.IsAuthenticated)
            //{
            //    await FeatureChecker.CheckEnabledAsync(AbpOssManagementFeatureNames.PublicAccess);
            //}
        }
    }
}
