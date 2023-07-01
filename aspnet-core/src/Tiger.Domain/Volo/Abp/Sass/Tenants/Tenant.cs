using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Tiger.Volo.Abp.Sass.Editions;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.TenantManagement;

// 完全限定 NotNullAttribute属性的命名空间
using NotNullAttribute = JetBrains.Annotations.NotNullAttribute;

namespace Tiger.Volo.Abp.Sass.Tenants
{
    /// <summary>
    /// 租户模型
    /// </summary>
    public class Tenant:FullAuditedAggregateRoot<Guid>
    {
        // Volo.Abp.Data.ConnectionStrings.DefaultConnectionStringName;
        protected const string DefaultConnectionStringName = "";

        public virtual string Name { get; protected set; }

        public virtual bool IsActive { get; protected set; }

        public virtual DateTime? EnableTime { get; protected set; }

        public virtual DateTime? DisableTime { get; protected set; }

        public virtual Guid? EditionId { get; set; }

        public virtual Edition Edition { get; set; }

        public virtual ICollection<TenantConnectionString> ConnectionStrings { get; protected set; }

        protected Tenant()
        {
            ConnectionStrings = new Collection<TenantConnectionString>();
        }

        protected internal Tenant(Guid id, [NotNull] string name)
            :base(id)
        {
            SetName(name);

            ConnectionStrings = new Collection<TenantConnectionString>();
        }

        protected internal virtual void SetName([NotNull] string name)
        {
            Name = Check.NotNullOrWhiteSpace(name, nameof(name), TenantConsts.MaxNameLength);
        }

        public virtual void SetEnableTime(DateTime? enableTime = null)
        {
            EnableTime = enableTime;
        }

        public virtual void SetDisableTIme(DateTime? disableTime = null)
        {
            DisableTime = disableTime;
        }



        #region ConnectionString
        [CanBeNull]
        public virtual string FindDefaultConnectionString()
        {
            return FindConnectionString(DefaultConnectionStringName);
        }

        [CanBeNull]
        public virtual string FindConnectionString(string name)
        {
            return ConnectionStrings.FirstOrDefault(c => c.Name == name)?.Value;
        }

        public virtual void SetDefaultConnectionString(string connectionString)
        {
            SetConnectionString(DefaultConnectionStringName, connectionString);
        }

        public virtual void SetConnectionString(string name, string connectionString)
        {
            var tenantConnectionString = ConnectionStrings.FirstOrDefault(x => x.Name == name);

            if (tenantConnectionString != null)
            {
                tenantConnectionString.SetValue(connectionString);
            }
            else
            {
                ConnectionStrings.Add(new TenantConnectionString(Id, name, connectionString));
            }
        }

        public virtual void RemoveDefaultConnectionString()
        {
            RemoveConnectionString(DefaultConnectionStringName);
        }

        public virtual void RemoveConnectionString(string name)
        {
            var tenantConnectionString = ConnectionStrings.FirstOrDefault(x => x.Name == name);

            if (tenantConnectionString != null)
            {
                ConnectionStrings.Remove(tenantConnectionString);
            }
        } 
        #endregion

    }
}
