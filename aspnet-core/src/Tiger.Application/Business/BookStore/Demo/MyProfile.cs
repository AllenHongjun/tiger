/**
 * 类    名：MyProfile   
 * 作    者：花生了什么树       
 * 创建时间：2021/8/16 15:19:31       
 * 说    明: 
 * 
 */

using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Tiger.Business.Demo;
using Volo.Abp.AutoMapper;

namespace Tiger.Books.Demo
{   
    ///// <summary>
    ///// 注意此处的 profile 不是用户信息的意思 这个是 automapper的基类
    ///// </summary>
    //public class MyProfile : Profile
    //{
    //    public MyProfile()
    //    {
    //        CreateMap<UserDto, User>()

    //            // 或者使用这个扩展方法来忽略
    //            .Ignore(x => x.Surname)

    //            // 在AutoMapper中,通常可以编写这样的映射代码来忽略属性:
    //            .ForMember(x => x.Name, map => map.Ignore());

    //        // 还有更多扩展方法, 如 IgnoreFullAuditedObjectProperties() 和 IgnoreCreationAuditedObjectProperties(),你可以根据实体类型使用.
    //        //.IgnoreAuditedObjectProperties<UserDto, User>();

    //        // 对象扩展系统 允许为已存在的类定义额外属性. ABP 框架提供了一个映射定义扩展可以正确的映射两个对象的额外属性.
    //        // .MapExtraProperties();
    //    }
    //}
}
