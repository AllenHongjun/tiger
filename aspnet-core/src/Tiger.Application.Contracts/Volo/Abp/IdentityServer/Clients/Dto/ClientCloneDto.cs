using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Tiger.Volo.Abp.IdentityServer.Clients.Dto
{   

    public class ClientCloneDto
    {   
        /// <summary>
        /// 客户端Id
        /// </summary>
        [Required]
        public string ClientId { get; set; }

        /// <summary>
        /// 客户端名称
        /// </summary>
        [Required]
        public string ClientName { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 复制客户端授权类型
        /// </summary>
        public bool CopyAllowedGrantType { get; set; }

        /// <summary>
        /// 复制客户端作用域
        /// </summary>
        public bool CopyAllowdScope { get; set; }

        /// <summary>
        /// 复制客户端重定向 Uri
        /// </summary>
        public bool CopyRedirectUri { get; set;}

        /// <summary>
        /// 复制客户端声明
        /// </summary>
        public bool CopyClaim { get; set; }

        /// <summary>
        /// 复制客户端密钥
        /// </summary>
        public bool CopySecret { get; set; }

        /// <summary>
        /// 复制客户端跨域来源
        /// </summary>
        public bool CopyAllowedCorsOrigin { get; set; }

        /// <summary>
        /// 复制客户端注销重定向Uri
        /// </summary>
        public bool CopyPostLogouRedirectUri { get; set; }

        /// <summary>
        /// 复制客户端属性
        /// </summary>
        public bool CopyPropertie { get; set; }

        /// <summary>
        /// 复制客户端IdP限制
        /// </summary>
        public bool CopyIdentityProviderRestriction { get; set; }


        /// <summary>
        /// 构造函数
        /// </summary>
        public ClientCloneDto()
        {   
            // 通过构造函数设置参数的默认值
            CopyAllowedCorsOrigin = true;
            CopyAllowedGrantType = true;
            CopyAllowdScope = true;
            CopyClaim = true;
            CopyIdentityProviderRestriction = true;
            CopyPostLogouRedirectUri = true;
            CopyPropertie = true;
            CopyRedirectUri = true;
            CopySecret = true;
        }

    }
}
