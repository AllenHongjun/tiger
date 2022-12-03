using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Auditing;
using Volo.Abp.Data;

namespace Volo.Abp.AuditLogging
{
    
    /// <summary>
    /// 实体变更
    /// </summary>
    public class EntityChangeDto : EntityDto<Guid>, IHasExtraProperties
    {   
        /// <summary>
        /// 变更时间
        /// </summary>
        public DateTime ChangeTime { get; set; }

        /// <summary>
        /// 实体变更类型 0:创建； 1:更新；  2:删除
        /// </summary>
        public EntityChangeType ChangeType { get; set; }
        
        /// <summary>
        /// 租户id
        /// </summary>
        public Guid? EntityTenantId { get; set; }
        
        /// <summary>
        /// 实体Id
        /// </summary>
        public string EntityId { get; set; }
        
        /// <summary>
        /// 实体类型全称
        /// </summary>
        public string EntityTypeFullName { get; set; }
        
        /// <summary>
        /// 实体属性变更
        /// </summary>
        public List<EntityPropertyChangeDto> PropertyChanges { get; set; }
        
        /// <summary>
        /// 额外属性
        /// </summary>
        public Dictionary<string, object> ExtraProperties { get; set; }
    }
}
