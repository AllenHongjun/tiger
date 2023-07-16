using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Guids;
using Volo.Abp.MultiTenancy;

namespace Tiger.Module.System.Platform.Datas
{
    /// <summary>
    /// 数据字典
    /// </summary>
    public class Data : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public Guid? TenantId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public virtual string Code { get; set; }

        /// <summary>
        /// 显示名称
        /// 
        /// </summary>
        public virtual string DisplayName { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public virtual string Description { get; set; }

        /// <summary>
        /// 父级ID
        /// </summary>
        public virtual Guid? ParentId { get; set; }

        /// <summary>
        /// 是否静态
        /// </summary>
        public virtual bool IsStatic { get; set; }

        public virtual ICollection<DataItem> Items { get; protected set; }

        protected Data()
        {
            Items = new Collection<DataItem>();
        }



        public Data(
            [NotNull] Guid id,
            [NotNull] string name,
            [NotNull] string code,
            [NotNull] string displayName,
            string description = "",
            Guid? parentId = null,
            Guid? tenantId = null)
        {
            Check.NotNull(id, nameof(id));
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.NotNullOrWhiteSpace(code, nameof(code));
            Check.NotNullOrWhiteSpace(displayName, nameof(displayName));

            Id = id;
            Name = name;
            Code = code;
            DisplayName = displayName;
            Description = description;
            ParentId = parentId;
            TenantId = tenantId;

            CreationTime = DateTime.Now;

            Items = new Collection<DataItem>();
        }

        public Data AddItem(
            [NotNull] IGuidGenerator guidGenerator,
            [NotNull] string name,
            [NotNull] string displayName,
            [CanBeNull] string defaultValue,
            ValueType valueType = ValueType.String,
            string description = "",
            bool allowBeNull = true,
            bool isStatic = false)
        {
            Check.NotNull(guidGenerator, nameof(guidGenerator));
            Check.NotNull(name, nameof(name));
            Check.NotNull(displayName, nameof(displayName));

            if (!IsInItem(name))
            {
                var dataItem = new DataItem(
                    guidGenerator.Create(),
                    Id,
                    name,
                    displayName,
                    defaultValue,
                    valueType,
                    description,
                    allowBeNull,
                    TenantId
                    )
                {
                    IsStatic = isStatic
                };
                Items.Add(dataItem);
            }

            return this;
        }

        public DataItem FindItem(string name)
        {
            return Items.FirstOrDefault(item => item.Name == name);
        }

        public DataItem FindItem(Guid id)
        {
            return Items.FirstOrDefault(item => item.Id == id);
        }

        public bool RemoveItem(string name)
        {
            if (IsInItem(name))
            {
                Items.RemoveAll(item => item.Name == name);
                return true;
            }
            return false;
        }

        public bool IsInItem(string name)
        {
            return Items.Any(item => item.Name == name);
        }
    }
}
