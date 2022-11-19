/**
 * 类    名：ProfilePictureContainer   
 * 作    者：花生了什么树       
 * 创建时间：2021/8/15 19:47:39       
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
    [BlobContainerName("profile-pictures")]
    public class ProfilePictureContainer
    {
        //每个容器分别存储. 这意味着BLOB名称在一个容器中应该是唯一的,两个具有相同名称的BLOB可以存在不同的容器中不会互相影响.
        //每个容器可以单独配置,因此每个容器可以根据你的配置使用不同的存储提供程序.
        //要创建类型化容器,需要创建一个简单的用 BlobContainerName 属性装饰的类:
    }
}
