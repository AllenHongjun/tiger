using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;
using Volo.Abp.MultiTenancy;

namespace Tiger.Volo.Abp.SettingManagementAppService
{
    public class SettingManagementDto : ExtensibleFullAuditedEntityDto<Guid>, IMultiTenant
    {
        public SettingManagementDto()
        {
            
        }

  
        public virtual string Name { get; protected set; }

        public virtual string Value { get; internal set; }

        public virtual string ProviderName { get; protected set; }

        public virtual string ProviderKey { get; protected set; }

        public Guid? TenantId => throw new NotImplementedException();

        // , IHasConcurrencyStamp

        //public string ConcurrencyStamp { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
