using Quartz;
using Quartz.Impl;
using System;
namespace SDMM_API.Modules
{
    public class JobScheduler
    {
        public static void Start()
        {
            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
            scheduler.Start();

            IJobDetail job = JobBuilder.Create<InventarioDiarioJob>().Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithDailyTimeIntervalSchedule
                  (s =>
                     s.WithIntervalInHours(24)
                    .OnEveryDay()
                    .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(22, 00))
                    .InTimeZone(TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time"))
                  )
                .Build();

            scheduler.ScheduleJob(job, trigger);
        }
    }
}