/**
 * 类    名：MyCustomBlobProvider   
 * 作    者：花生了什么树       
 * 创建时间：2021/8/15 19:59:40       
 * 说    明: 
 * 
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.BlobStoring;
using Volo.Abp.DependencyInjection;

namespace Tiger.BlobDemo
{
    /// <summary>
    /// 自定义 对象存储提供程序
    /// 
    /// MyCustomBlobProvider 继承 BlobProviderBase 并覆盖 abstract 方法. 实际的实现取决于你.
    ///实现 ITransientDependency 接口将这个类注做为瞬态服务注册到依赖注入系统.
    ///注意: 命名约定很重要. 如果类名没有以 BlobProvider 结尾,则必须手动注册/公开你的服务为 IBlobProvider.
    /// </summary>
    public class MyCustomBlobProvider : BlobProviderBase, ITransientDependency
    {
        public override Task SaveAsync(BlobProviderSaveArgs args)
        {
            //TODO...
            //使用 GetMyCustomBlobProviderConfiguration 方法访问额外的选项:

            var config = args.Configuration.GetMyCustomBlobProviderConfiguration();
            var value = config.MyOption1;

            return null;
        }

        public override Task<bool> DeleteAsync(BlobProviderDeleteArgs args)
        {
            //TODO...
            return null;
        }

        public override Task<bool> ExistsAsync(BlobProviderExistsArgs args)
        {
            //TODO...
            return null;
        }

        public override Task<Stream> GetOrNullAsync(BlobProviderGetArgs args)
        {
            //TODO...
            return null;
        }
    }
}
