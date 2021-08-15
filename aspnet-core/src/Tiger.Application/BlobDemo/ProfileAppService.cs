/**
 * 类    名：ProfileAppService   
 * 作    者：花生了什么树       
 * 创建时间：2021/8/15 19:49:09       
 * 说    明: 
 * 
 */

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.BlobStoring;
using Volo.Abp.Users;

namespace Tiger.BlobDemo
{
    /// <summary>
    /// 示例: 用于保存和读取当前用户的个人资料图片的应用服务
    /// </summary>
    [Authorize]
    public class ProfileAppService : TigerAppService
    {
        private readonly IBlobContainer<ProfilePictureContainer> _blobContainer;
        private readonly IBlobContainer<MyBlobContainer> _myBlobContainer;

        public ProfileAppService(
            //IBlobContainer<ProfilePictureContainer> blobContainer, 
            IBlobContainer<MyBlobContainer> myBlobContainer)
        {
            //_blobContainer = blobContainer;
            _myBlobContainer = myBlobContainer;
        }

        /// <summary>
        /// 保存图片
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public async Task SaveProfilePictureAsync(byte[] bytes)
        {
            var blobName = CurrentUser.GetId().ToString();
            await _blobContainer.SaveAsync(blobName, bytes);
        }

        /// <summary>
        /// 获取图片
        /// </summary>
        /// <returns></returns>
        public async Task<byte[]> GetProfilePictureAsync()
        {
            var blobName = CurrentUser.GetId().ToString();
            return await _blobContainer.GetAllBytesOrNullAsync(blobName);
        }


        /// <summary>
        /// 保存图片
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public async Task SaveMyBlobAsync(IFormFile file)
        {
            
            BinaryReader r = new BinaryReader(file.OpenReadStream());
            r.BaseStream.Seek(0, SeekOrigin.Begin);    //将文件指针设置到文件

            byte[] bytes = r.ReadBytes((int)r.BaseStream.Length);
            var blobName = CurrentUser.GetId().ToString();
            await _myBlobContainer.SaveAsync(blobName, bytes);
        }

        /// <summary>
        /// 获取图片
        /// </summary>
        /// <returns></returns>
        public async Task<byte[]> GetMyBlobAsync()
        {
            var blobName = CurrentUser.GetId().ToString();
            return await _myBlobContainer.GetAllBytesOrNullAsync(blobName);
        }
    }
}
