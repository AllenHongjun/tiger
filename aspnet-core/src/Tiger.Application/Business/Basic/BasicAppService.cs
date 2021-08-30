using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Tiger.Blob.Qinui;
using Volo.Abp;
using Volo.Abp.BlobStoring;

namespace Tiger.Business.Basic
{
    [RemoteService(true)]
    [ApiExplorerSettings(GroupName = "admin-basic")]
    public class BasicAppService:TigerAppService
    {

        private readonly IBlobContainer<QiniuBlobContainer> _qiniuBlobContainer;

        public BasicAppService(IBlobContainer<QiniuBlobContainer> qiniuBlobContainer)
        {
            _qiniuBlobContainer = qiniuBlobContainer;
        }


        // 发送邮件

        // 发送验证码

        // 通用数据

        // 文件上传

        /// <summary>
        /// 七牛文件上传保存图片
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public JsonResult QiniuBlobSaveAsync(IFormFile file)
        {
            // 将 file类型转为 byte[]
            BinaryReader r = new BinaryReader(file.OpenReadStream());
            //将文件指针设置到文件
            r.BaseStream.Seek(0, SeekOrigin.Begin);
            byte[] bytes = r.ReadBytes((int)r.BaseStream.Length);

            var blobName = Guid.NewGuid().ToString() + file.FileName;
            _qiniuBlobContainer.SaveAsync(blobName, bytes);
            return new JsonResult(new { blobName });
        }

    }
}
