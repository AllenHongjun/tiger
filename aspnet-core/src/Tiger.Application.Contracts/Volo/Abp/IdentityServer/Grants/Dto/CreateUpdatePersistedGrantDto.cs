using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Tiger.Volo.Abp.IdentityServer.Grants.Dto
{
    public class CreateUpdatePersistedGrantDto : EntityDto<Guid>
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
        /// 会话标识
        /// </summary>
        public virtual string SubjectId { get; set; }

        public virtual string ClientId { get; set; }

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
