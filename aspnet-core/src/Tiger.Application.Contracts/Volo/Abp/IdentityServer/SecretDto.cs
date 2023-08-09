using System;
using System.Collections.Generic;
using System.Text;

namespace Tiger.Volo.Abp.IdentityServer
{
    public class SecretDto
    {
        /// <summary>
        /// 密钥类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; set; }

        public string Description { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime? Expiration { get; set; }
    }
}
