using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Features;

namespace Tiger.Infrastructure.Features
{
    [RemoteService(false)]
    public class ReportingAppService : ApplicationService
    {
        //IFeatureChecker允许检查应用程序代码中的功能。
        private readonly IFeatureChecker _featureChecker;

        public ReportingAppService(IFeatureChecker featureChecker)
        {
            _featureChecker = featureChecker;
        }

        /// <summary>
        /// 功能特性使用demo
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// RequiresFeature(...)只需获取一个功能名称以检查它是否已启用。如果未启用，则会引发授权异常，并向客户端返回正确的响应。
        /// RequiresFeature可能会获得多个功能名称，例如 .在这种情况下，ABP检查是否启用了任何功能.使用选项，例如强制检查要启用的所有功能。[RequiresFeature("Feature1", "Feature2")]RequiresAll[RequiresFeature("Feature1", "Feature2", RequiresAll = true)]
        /// 可以多此使用
        /// 
        /// 拦截的方法要遵循规则
        /// </remarks>
        [RequiresFeature("MyApp.PdfReporting")]
        public async Task GetPdfReportAsync()
        {
            //TODO...
        }



        public async Task<int> GetPdfReportAsync(int page = 0)
        {   
            // 检查某个特性是否可用
            if (await _featureChecker.IsEnabledAsync("MyApp.PdfReporting"))
            {
                //TODO...
            }
            else
            {
                //TODO...   
            }






            // 获取功能的当前值。此方法返回 ，因此您可以通过转换为 或从 .stringstring
            var currentProductCount = 15;

            //GET THE FEATURE VALUE
            var maxProductCountLimit =
                await _featureChecker.GetOrNullAsync("MyApp.MaxProductCount");

            if (currentProductCount >= Convert.ToInt32(maxProductCountLimit))
            {
                throw new BusinessException(
                    "MyApp:ReachToMaxProductCountLimit",
                    $"You can not create more than {maxProductCountLimit} products!"
                );
            }

            //TODO: Create the product in the database...


            return 0;
        }








    }

}
