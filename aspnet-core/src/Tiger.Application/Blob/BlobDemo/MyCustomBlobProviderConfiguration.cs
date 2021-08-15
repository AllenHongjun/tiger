/**
 * 类    名：MyCustomBlobProviderConfiguration   
 * 作    者：花生了什么树       
 * 创建时间：2021/8/15 20:08:48       
 * 说    明: 
 * 
 */

using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.BlobStoring;

namespace Tiger.BlobDemo
{
    /// <summary>
    /// BlobContainerConfiguration 允许添加/删除提供程序特定的配置对象. 
    /// 如果你的提供者需要额外的配置,你可以为 BlobContainerConfiguration 创建一个包装类提供的类型安全配置选项:
    /// </summary>
    public class MyCustomBlobProviderConfiguration
    {
        public string MyOption1
        {
            get => _containerConfiguration
                .GetConfiguration<string>("MyCustomBlobProvider.MyOption1");
            set => _containerConfiguration
                .SetConfiguration("MyCustomBlobProvider.MyOption1", value);
        }

        private readonly BlobContainerConfiguration _containerConfiguration;

        public MyCustomBlobProviderConfiguration(
            BlobContainerConfiguration containerConfiguration)
        {
            _containerConfiguration = containerConfiguration;
        }
    }
}
