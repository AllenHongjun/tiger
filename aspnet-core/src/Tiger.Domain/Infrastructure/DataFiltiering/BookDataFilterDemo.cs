using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities;
using Volo.Abp;
using Volo.Abp.MultiTenancy;

namespace Tiger.CoreModule.DataFiltiering
{

    /// <summary>
    /// 数据过滤演示实体类
    /// </summary>
    public class BookDataFilterDemo : AggregateRoot<Guid>, ISoftDelete, IMultiTenant, IsActive
    {
        public string Name { get; set; }

        /// <summary>
        /// 将实体标记为已删除,并不是物理删除. 实现 ISoftDelete 接口将你的实体"软删除".
        /// ISoftDelete 定义了 IsDeleted 属性. 当你使用仓储删除一条记录时, ABP会自动将 IsDeleted 设置为true,并将删除操作替换为修改操作(如果需要,也可以手动将 IsDeleted 设置为true). 在查询数据库时会自动过滤软删除的实体.
        /// </summary>
        public bool IsDeleted { get; set; } //Defined by ISoftDelete


        /// <summary>
        /// IMultiTenant 接口定义了 TenantId 属性用于自动过滤当前租户实体
        /// </summary>
        public Guid? TenantId => throw new NotImplementedException();

        public bool? IsActive => throw new NotImplementedException();

    }
}
