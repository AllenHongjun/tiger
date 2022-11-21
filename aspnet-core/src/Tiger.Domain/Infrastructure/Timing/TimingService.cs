using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Timing;

namespace Tiger.Infrastructure.Timing
{

    /// <summary>
    /// 时间和时区
    /// </summary>
    /// <remarks>
    /// 解决此问题的一种方法是始终使用 DateTime.UtcNow 并将所有 DateTime 对象假定为UTC时间. 在这种情况下你可以在需要时将其转换为目标客户端的时区.
    /// </remarks>
    public class MyService : ITransientDependency
    {
        //IClock 在获取当前时间的同时提供了一种抽象,你可以在应用程序中的单个点上控制日期时间的类型(UTC或本地时间).
        private readonly IClock _clock;

        // ITimezoneProvider 是一个服务,可将Windows时区ID值简单转换为Iana时区名称值,反之亦然. 它还提供了获取这些时区列表与获取具有给定名称的 TimeZoneInfo 的方法.
        // 时区转换 https://github.com/mattjohnsonpint/TimeZoneConverter 其他时区和utc时间转化
        private readonly ITimezoneProvider _timezoneProvider;

        public MyService(IClock clock, ITimezoneProvider timezoneProvider)
        {
            _clock = clock;
            _timezoneProvider=timezoneProvider;
        }

        public void Foo()
        {
            //G获取当前时间
            var now = _clock.Now;


            DateTime dateTime = Convert.ToDateTime("2022-12-25"); //Get from somewhere


            // IClock 的其他重要功能是规范化 DateTime 对象.

            /*
             Normalize 方法的工作原理如下:
                如果当前时钟为UTC,并且给定的 DateTime 为本地时间,将给定的 DateTime 转换为UTC(通过使用 DateTime.ToUniversalTime() 方法).
                如果当前时钟是本地的,并且给定的 DateTime 是UTC,将给定的 DateTime 转换为本地时间(通过使用 DateTime.ToUniversalTime() 方法).
                如果未指定给定的 DateTime 的 Kind,将给定的 DateTime 的 Kind(使用 DateTime.SpecifyKind(...) 方法)设置为当前时钟的 Kind.
             
             */

            // 本地时间和utc时间转化  
            DateTime normalizedDateTime = _clock.Normalize(dateTime);

        }
    }
}
