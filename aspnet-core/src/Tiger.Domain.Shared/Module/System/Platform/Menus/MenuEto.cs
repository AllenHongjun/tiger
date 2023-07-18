using System;
using System.Collections.Generic;
using System.Text;
using Tiger.Module.System.Platform.Routes;
using Volo.Abp.EventBus;

namespace Tiger.Module.System.Platform.Menus
{
    [EventName("platform.menus.menu")]
    public class MenuEto:RouteEto
    {
        public string Framework { get; set; }
    }
}
