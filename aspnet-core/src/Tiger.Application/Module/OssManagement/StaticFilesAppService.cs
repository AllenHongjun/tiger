using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Tiger.Module.OssManagement.Dto;
using Tiger.Module.OssManagement.Dtos;
using Tiger.Module.OssManagement.Features;
using Volo.Abp;
using Volo.Abp.Content;
using Volo.Abp.Features;

namespace Tiger.Module.OssManagement
{
    /// <summary>
    /// 静态文件上传服务
    /// </summary>
    [RemoteService(IsEnabled = false)]
    public class StaticFilesAppService : OssManagementApplicationServiceBase, IStaticFilesAppService
    {
        protected IOssContainerFactory OssContainerFactory { get; }

        public StaticFilesAppService(
            IOssContainerFactory ossContainerFactory)
        {
            OssContainerFactory = ossContainerFactory;
        }

        [RequiresFeature(AbpOssManagementFeatureNames.OssObject.DownloadFile)]
        //[RequiresLimitFeature(
        //    AbpOssManagementFeatureNames.OssObject.DownloadLimit,
        //    AbpOssManagementFeatureNames.OssObject.DownloadInterval,
        //    LimitPolicy.Month)]
        public async virtual Task<Stream> GetAsync(GetStaticFileInput input)
        {
            var ossObjectRequest = new GetOssObjectRequest(
                HttpUtility.UrlDecode(input.Bucket), // 需要处理特殊字符
                HttpUtility.UrlDecode(input.Name),
                HttpUtility.UrlDecode(input.Path),
                HttpUtility.UrlDecode(input.Process))
            {
                MD5 = true,
            };

            var ossContainer = OssContainerFactory.Create();
            var ossObject = await ossContainer.GetObjectAsync(ossObjectRequest);

            return ossObject.Content;
        }

        Task<IRemoteStreamContent> IStaticFilesAppService.GetAsync(GetStaticFileInput input)
        {
            throw new NotImplementedException();
        }
    }
}
