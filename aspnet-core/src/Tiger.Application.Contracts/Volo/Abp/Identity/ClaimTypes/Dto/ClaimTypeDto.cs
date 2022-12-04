using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Identity;

namespace Tiger.Volo.Abp.Identity.ClaimTypes.Dto
{
    /// <summary>
    /// 声明类型
    /// </summary>
    /// <remarks>
    /// 自定义声明可用于存储用户或角色的其他信息。
	/// 然后，可以在经过身份验证的用户的应用程序代码中访问自定义声明值。
	/// 声明类型也由身份服务器模块使用（如果使用）。
    /// </remarks>
    public class ClaimTypeDto : ExtensibleEntityDto<Guid>
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 必须
        /// </summary>
        public bool Required { get; set; }

        /// <summary>
        /// 是否静态
        /// </summary>
        public bool IsStatic { get; set; }

        /// <summary>
        /// 正则
        /// </summary>
        public string Regex { get; set; }

        /// <summary>
        /// 正则描述
        /// </summary>
        public string RegexDescription { get; set; }

        /// <summary>
        /// 声明类型
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 声明类型值  0:String,1:Int,2:Boolean,3:DateTime
        /// </summary>
        public IdentityClaimValueType ValueType { get; set; }

        public string ValueTypeAsString { get; set; }
    }
}
