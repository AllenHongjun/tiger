﻿using System;

namespace Tiger.Volo.Abp.Sass.MultiTenancy
{
    public class ConnectionStringCreatedEventData
    {
        public Guid TenantId { get; set; }

        public string TenantName { get; set; }

        public string Name { get; set; }
    }
}
