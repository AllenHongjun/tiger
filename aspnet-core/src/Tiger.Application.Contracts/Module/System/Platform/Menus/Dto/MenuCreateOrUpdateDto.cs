﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Tiger.Module.System.Platform.Routes;
using Volo.Abp.Validation;

namespace Tiger.Module.System.Platform.Menus.Dto
{
    public class MenuCreateOrUpdateDto
    {
        public Guid? ParentId { get; set; }

        [Required]
        [DynamicStringLength(typeof(RouteConsts), nameof(RouteConsts.MaxNameLength))]
        public string Name { get; set; }

        [Required]
        [DynamicStringLength(typeof(RouteConsts), nameof(RouteConsts.MaxDisplayNameLength))]
        public string DisplayName { get; set; }

        [DynamicStringLength(typeof(RouteConsts), nameof(RouteConsts.MaxDescriptionLength))]
        public string Description { get; set; }

        [Required]
        [DynamicStringLength(typeof(RouteConsts), nameof(RouteConsts.MaxPathLength))]
        public string Path { get; set; }

        [DynamicStringLength(typeof(RouteConsts), nameof(RouteConsts.MaxRedirectLength))]
        public string Redirect { get; set; }

        [Required]
        [DynamicStringLength(typeof(MenuConsts), nameof(MenuConsts.MaxComponentLength))]
        public string Component { get; set; }

        public bool IsPublic { get; set; }

        public Dictionary<string, object> Meta { get; set; } = new Dictionary<string, object>();
    }
}
