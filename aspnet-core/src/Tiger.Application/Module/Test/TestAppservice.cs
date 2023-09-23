using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Timing;

namespace Tiger.Module.Test
{
    /// <summary>
    /// 测试服务
    /// </summary>
    [RemoteService(IsEnabled = true)]
    [Area("test")]
    [ApiExplorerSettings(GroupName = "admin")]
    [Route("api/test")]
    public class TestAppservice: ApplicationService
    {
        private readonly ITimezoneProvider timezoneProvider;

        public TestAppservice(ITimezoneProvider timezoneProvider)
        {
            this.timezoneProvider=timezoneProvider;
        }

        [HttpGet]
        [Route("TestGet")]
        public List<NameValue> TestGet()
        {
            timezoneProvider.GetIanaTimezones();
            List<NameValue> windowsTimezones =  timezoneProvider.GetWindowsTimezones();
            return windowsTimezones;
        }
    }
}
