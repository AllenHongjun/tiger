﻿using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.MultiTenancy;

namespace Tiger.Module.System.Platform.Routes
{
    public abstract class RouteEto:IMultiTenant
    {
        public Guid? TenantId { get; set; }
        public Guid Id { get; set; }
        public string Path { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public string Redirect { get; set; }
    }
}
