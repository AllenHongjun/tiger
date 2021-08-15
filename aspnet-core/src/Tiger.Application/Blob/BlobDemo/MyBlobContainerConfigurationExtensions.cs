/**
 * 类    名：MyBlobContainerConfigurationExtensions   
 * 作    者：花生了什么树       
 * 创建时间：2021/8/15 20:02:39       
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
    /// 一个更简单的配置方式,可以为 BlobContainerConfiguration 类创建一个扩展方法:
    /// </summary>
    public static class MyBlobContainerConfigurationExtensions
    {
        public static BlobContainerConfiguration UseMyCustomBlobProvider(
            this BlobContainerConfiguration containerConfiguration,
            Action<MyCustomBlobProviderConfiguration> configureAction
            )
        {   
            // 添加自定义的配置
            configureAction.Invoke(
                new MyCustomBlobProviderConfiguration(containerConfiguration)
            );

            containerConfiguration.ProviderType = typeof(MyCustomBlobProvider);
            return containerConfiguration;
        }

        public static MyCustomBlobProviderConfiguration GetMyCustomBlobProviderConfiguration(
        this BlobContainerConfiguration containerConfiguration)
        {
            return new MyCustomBlobProviderConfiguration(containerConfiguration);
        }
    }
}
