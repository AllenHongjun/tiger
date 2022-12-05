using System;
using System.Collections.Generic;
using System.Text;

namespace Tiger.Volo.Abp.IdentityServer
{
    public class SecretDto
    {
        public string Type { get; set; }

        public string Value { get; set; }

        public string Description { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime? Expiration { get; set; }
    }
}
