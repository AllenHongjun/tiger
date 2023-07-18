using System;
using System.Collections.Generic;
using System.Text;
using Tiger.Module.System.Platform.Routes;
using Volo.Abp.EventBus;

namespace Tiger.Module.System.Platform.Layouts
{
    [EventName("platform.layouts.layout")]
    public class LayoutEto:RouteEto
    {
    }
}
