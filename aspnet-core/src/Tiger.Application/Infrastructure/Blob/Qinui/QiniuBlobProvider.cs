/**
 * 类    名：QiniuBlobProvider   
 * 作    者：花生了什么树       
 * 创建时间：2021/8/15 21:57:53       
 * 说    明: 
 * 
 */

using Microsoft.Extensions.Configuration;
using Qiniu.Http;
using Qiniu.Storage;
using Qiniu.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.BlobStoring;
using Volo.Abp.DependencyInjection;

namespace Tiger.Blob.Qinui
{
    /// <summary>
    /// 七牛 oss对象存储  https://developer.qiniu.com/kodo/1237/csharp  
    /// 
    /// </summary>
    public class QiniuBlobProvider : BlobProviderBase, ITransientDependency
    {
        private readonly IConfiguration _configuration;

        public QiniuBlobProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public override Task SaveAsync(BlobProviderSaveArgs args)
        {
            //TODO...
            var containerName = args.ContainerName;
            var blobName = $"{args.BlobName}";

            var configurationSection = _configuration.GetSection("Qiniu");

            Mac mac = new Mac(configurationSection["AccessKey"], configurationSection["SecretKey"]);// AK SK使用
            PutPolicy putPolicy = new PutPolicy();
            putPolicy.Scope = configurationSection["Bucket"];
            string token = Auth.CreateUploadToken(mac, putPolicy.ToJsonString());//token生成
            Config config = new Config()
            {
                Zone = Zone.ZONE_CN_South,
                UseHttps = true
            };
            FormUploader upload = new FormUploader(config);
            HttpResult result = new HttpResult();

            Stream stream = args.BlobStream;
            result = upload.UploadStream(stream, blobName, token, null);
            if (result.Code != 200)
            {
                throw new Exception(result.RefText);//上传失败错误信息
            }

            return Task.CompletedTask;
        }

        public override Task<bool> DeleteAsync(BlobProviderDeleteArgs args)
        {
            throw new NotImplementedException();
        }

        public override Task<bool> ExistsAsync(BlobProviderExistsArgs args)
        {
            throw new NotImplementedException();
        }

        public override Task<Stream> GetOrNullAsync(BlobProviderGetArgs args)
        {
            throw new NotImplementedException();
        }

        

    }
}
