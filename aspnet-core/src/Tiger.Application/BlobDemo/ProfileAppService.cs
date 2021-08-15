/**
 * 类    名：ProfileAppService   
 * 作    者：花生了什么树       
 * 创建时间：2021/8/15 19:49:09       
 * 说    明: 
 * 
 */

using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
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
    public class ProfileAppService : ApplicationService
    {
        private readonly IBlobContainer<ProfilePictureContainer> _blobContainer;

        public ProfileAppService(IBlobContainer<ProfilePictureContainer> blobContainer)
        {
            _blobContainer = blobContainer;
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
    }
}
