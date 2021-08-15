/**
 * 类    名：MyBlobContainer   
 * 作    者：花生了什么树       
 * 创建时间：2021/8/15 20:50:53       
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
    /// 类型化BLOB容器系统是一种在应用程序中创建和管理多个容器的方法;
    /// </summary>
    [BlobContainerName("my-custom-blob")]
    public class MyBlobContainer
    {
    }
}
