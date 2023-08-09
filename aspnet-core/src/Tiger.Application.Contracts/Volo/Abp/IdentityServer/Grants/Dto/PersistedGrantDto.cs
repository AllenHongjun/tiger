using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Tiger.Volo.Abp.IdentityServer.Grants.Dto
{
    /// <summary>
    /// 持久授权
    /// </summary>
    public class PersistedGrantDto : EntityDto<Guid>
    {
        /// <summary>
        /// 授权标识
        /// </summary>
        public virtual string Key { get; set; }

        /// <summary>
        /// 授权类型
        /// </summary>
        public virtual string Type { get; set; }

        /// <summary>
        /// 主体标识
        /// </summary>
        public virtual string SubjectId { get; set; }

        /// <summary>
        /// 客户端Id
        /// </summary>
        public virtual string ClientId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreationTime { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public virtual DateTime? Expiration { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public virtual string Data { get; set; }
    }
}
