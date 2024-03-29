﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Identity;
using Volo.Abp.ObjectExtending;
using Volo.Abp.Validation;

namespace Tiger.Volo.Abp.Identity.OrganizationUnits.Dto
{
    public abstract class OrganizationUnitCreateOrUpdateDtoBase : ExtensibleObject
    {
        [DynamicStringLength(typeof(OrganizationUnitConsts), nameof(OrganizationUnitConsts.MaxDisplayNameLength))]
        public string DisplayName { get; set; }

        protected OrganizationUnitCreateOrUpdateDtoBase() : base(false)
        {

        }
    }
}
