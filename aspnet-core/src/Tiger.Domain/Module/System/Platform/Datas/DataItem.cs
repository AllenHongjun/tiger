﻿using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Tiger.Module.System.Platform.Datas
{
    /// <summary>
    /// 数据字典项
    /// </summary>
    public class DataItem : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public Guid? TenantId {get; set;}

        public virtual string Name { get; set;}

        public virtual string DisplayName { get; set;}

        public virtual string Description { get; set;}  

        /// <summary>
        /// 默认值
        /// </summary>
        public virtual string DefaultValue { get; set;}

        /// <summary>
        /// 允许为NULL
        /// </summary>
        public virtual bool AllowBeNull { get; set;}

        /// <summary>
        /// 是否静态
        /// </summary>
        public virtual bool IsStatic { get; set;}

        public virtual ValueType ValueType { get; protected set;}

        public virtual Guid DataId { get; protected set; }

        internal DataItem(
            [NotNull] Guid id,
            [NotNull] Guid dataId,
            [NotNull] string name,
            [NotNull] string displayName,
            [CanBeNull] string defaultValue = null,
            ValueType valueType = ValueType.String,
            string description = "",
            bool allowBeNull = true,
            Guid? tenantId = null)
        {
            Check.NotNull(id, nameof(id));
            Check.NotNull(dataId, nameof(dataId));
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.NotNullOrWhiteSpace(displayName, nameof(displayName));

            Id = id;
            Name = name;
            DefaultValue = defaultValue ?? SetDefaultValue();
            ValueType = valueType;
            DisplayName = displayName;
            AllowBeNull = allowBeNull;

            DataId = dataId;
            TenantId = tenantId;
            Description = description;
        }

        public string SetDefaultValue()
        {
            switch (ValueType)
            {
                case ValueType.Array:
                    DefaultValue = "";// 当数据类型为数组对象时，需要前端来做转换了，约定的分隔符为英文逗号
                    break;
                case ValueType.Boolean:
                    DefaultValue = "false";
                    break;
                case ValueType.Date:
                    DefaultValue = !AllowBeNull ? DateTime.Now.ToString("yyyy-MM-dd") : "";
                    break;
                case ValueType.DateTime:
                    if (!AllowBeNull)
                    {
                        DefaultValue = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); // TODO: 以当前时间作为默认值?
                    }
                    DefaultValue = !AllowBeNull ? DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") : "";
                    break;
                case ValueType.Numeic:
                    DefaultValue = "0";
                    break;
                case ValueType.Object:
                    DefaultValue = "{}";
                    break;
                default:
                case ValueType.String:
                    DefaultValue = "";
                    break;
            }

            return DefaultValue;
        }
    }
}
