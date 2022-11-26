using Quartz.Impl;
using Quartz;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tiger.Infrastructure.BackgroundWorker
{
    public class GetScheduler
    {

        private static IScheduler CreateScheduler()
        {
            var schedulerFactory = new StdSchedulerFactory();
            var scheduler = schedulerFactory.GetScheduler().Result;


            scheduler.Start();
            return scheduler;
        }
    }
}
