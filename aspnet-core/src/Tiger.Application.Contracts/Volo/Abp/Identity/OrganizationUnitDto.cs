using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;
using Volo.Abp.MultiTenancy;

namespace Volo.Abp.Identity
{
    public class OrganizationUnitDto : ExtensibleFullAuditedEntityDto<Guid>, IMultiTenant, IHasConcurrencyStamp
    {
        public OrganizationUnitDto()
        {
            Children = new List<OrganizationUnitDto>();
        }

        /// <summary>
        /// 租户id
        /// </summary>
        public Guid? TenantId { get; set; }

        public string ConcurrencyStamp { get; set; }

        /// <summary>
        /// 父级id
        /// </summary>
        public virtual Guid? ParentId { get; set; }

        public virtual string Code { get; set; }

        /// <summary>
        /// 展示名称
        /// </summary>
        public virtual string DisplayName { get; set; }

        public int Level => GetLevel(Code);

        /// <summary>
        /// 是否页节点
        /// </summary>
        public virtual bool IsLeaf { get; set; }

        /// <summary>
        /// 子组织
        /// </summary>
        public List<OrganizationUnitDto> Children { get; set; }

        private int GetLevel(string code)
        {
            return Code.Split('.').Length;
        }

        public void SetLeaf()
        {
            IsLeaf = true;
        }
    }
}
