/**
 * 类    名：MyService   
 * 作    者：花生了什么树       
 * 创建时间：2021/8/15 19:44:44       
 * 说    明: 
 * 
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.BlobStoring;
using Volo.Abp.DependencyInjection;

namespace Tiger.BlobDemo
{
    /// <summary>
    /// 该服务用 my-blob-1 名称保存给定的字节,然后以相同的名称获取先前保存的字节.
    /// 一个BLOB是一个命名对象,每个BLOB都应该有一个唯一的名称,它是一个任意的字符串.
    /// </summary>
    public class MyService : ITransientDependency
    {
        private readonly IBlobContainer _blobContainer;

        public MyService(IBlobContainer blobContainer, IBlobContainerFactory blobContainerFactory)
        {
            _blobContainer = blobContainer;

            //以注入并使用 IBlobContainerFactory 来获得一个BLOB容器的名称:
            _blobContainer = blobContainerFactory.Create("profile-pictures");

            //示例: 通过类型创建容器
            _blobContainer = blobContainerFactory.Create<ProfilePictureContainer>();

        }

        public async Task SaveBytesAsync(byte[] bytes)
        {
            await _blobContainer.SaveAsync("my-blob-1", bytes);
        }

        public async Task<byte[]> GetBytesAsync()
        {
            return await _blobContainer.GetAllBytesOrNullAsync("my-blob-1");
        }
    }
}
