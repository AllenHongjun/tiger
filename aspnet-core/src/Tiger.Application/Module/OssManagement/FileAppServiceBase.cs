using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Tiger.Module.OssManagement.Dto;
using Tiger.Module.OssManagement.Dtos;
using Tiger.Module.OssManagement.Features;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Content;
using Volo.Abp.Features;
using Volo.Abp.Validation;

namespace Tiger.Module.OssManagement
{
    public abstract class FileAppServiceBase : OssManagementApplicationServiceBase, IFileAppService
    {
        protected IFileUploader FileUploader { get; }
        protected IFileValidater FileValidater { get; }
        protected IOssContainerFactory OssContainerFactory { get; }

        protected FileAppServiceBase(
            IFileUploader fileUploader,
            IFileValidater fileValidater,
            IOssContainerFactory ossContainerFactory)
        {
            FileUploader = fileUploader;
            FileValidater = fileValidater;
            OssContainerFactory = ossContainerFactory;
        }

        /// <summary>
        /// 文件直接上传
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequiresFeature(AbpOssManagementFeatureNames.OssObject.UploadFile)]
        public async virtual Task UploadAsync(UploadFileChunkInput input)
        {
            input.Bucket = GetCurrentBucket();
            input.Path = GetCurrentPath(HttpUtility.UrlDecode(input.Path));
            await FileUploader.UploadAsync(input);
        }


        /// <summary>
        /// 文件分片上传
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RequiresFeature(AbpOssManagementFeatureNames.OssObject.UploadFile)]
        //[RequiresLimitFeature(
        //    AbpOssManagementFeatureNames.OssObject.UploadLimit,
        //    AbpOssManagementFeatureNames.OssObject.UploadInterval,
        //    LimitPolicy.Month)]
        public async virtual Task<OssObjectDto> UploadAsync(UploadFileInput input)
        {
            if (input.File == null || input.File.ContentLength.Value > 0)
            {
                ThrowValidationException(L["FileNotBeNullOrEmpty"], "File");
            }

            await FileValidater.ValidationAsync(new UploadFile
            {
                TotalSize = input.File.ContentLength.Value,
                FileName = input.Object
            });

            var oss = OssContainerFactory.Create();

            var createOssObjectRequest = new CreateOssObjectRequest(
                 GetCurrentBucket(),
                 HttpUtility.UrlDecode(input.Object),
                 input.File.GetStream(),
                 GetCurrentPath(HttpUtility.UrlDecode(input.Path)))
            {
                Overwrite = input.Overwrite
            };

            var ossObject = await oss.CreateObjectAsync(createOssObjectRequest);

            return ObjectMapper.Map<OssObject, OssObjectDto>(ossObject);
        }

        public async virtual Task<ListResultDto<OssObjectDto>> GetListAsync(GetFilesInput input)
        {
            var ossContainer = OssContainerFactory.Create();
            var response = await ossContainer.GetObjectsAsync(
                GetCurrentBucket(),
                GetCurrentPath(HttpUtility.UrlDecode(input.Path)),
                skipCount: 0,
                maxResultCount: input.MaxResultCount);

            return new ListResultDto<OssObjectDto>(
                ObjectMapper.Map<List<OssObject>, List<OssObjectDto>>(response.Objects));
        }

        [RequiresFeature(AbpOssManagementFeatureNames.OssObject.DownloadFile)]
        //[RequiresFeature(
        //    AbpOssManagementFeatureNames.OssObject.DownloadLimit,
        //    AbpOssManagementFeatureNames.OssObject.DownloadInterval)]
        public async virtual Task<IRemoteStreamContent> GetAsync(GetPublicFileInput input)
        {
            var ossObjectRequest = new GetOssObjectRequest(
                GetCurrentBucket(),
                // 需要处理特殊字符
                HttpUtility.UrlDecode(input.Name),
                GetCurrentPath(HttpUtility.UrlDecode(input.Path)),
                HttpUtility.UrlDecode(input.Process))
            {
                MD5 = true,
            };

            var ossContainer = OssContainerFactory.Create();
            var ossObject = await ossContainer.GetObjectAsync(ossObjectRequest);

            return new RemoteStreamContent(ossObject.Content, ossObject.Name);
        }

        public async virtual Task DeleteAsync(GetPublicFileInput input)
        {
            var ossContainer = OssContainerFactory.Create();

            await ossContainer.DeleteObjectAsync(
                GetCurrentBucket(),
                HttpUtility.UrlDecode(input.Name),
                GetCurrentPath(input.Path));
        }

        protected virtual string GetCurrentBucket()
        {
            throw new NotImplementedException();
        }

        protected virtual string GetCurrentPath(string path)
        {
            if (path.IsNullOrWhiteSpace())
            {
                return "";
            }
            path = HttpUtility.UrlDecode(path);
            path = path.RemovePreFix(".").RemovePreFix("/");
            return path;
        }

        private static void ThrowValidationException(string message, string memberName)
        {
            throw new AbpValidationException(message,
                new List<ValidationResult>
                {
                    new ValidationResult(message, new[] {memberName})
                });
        }
    }
}
