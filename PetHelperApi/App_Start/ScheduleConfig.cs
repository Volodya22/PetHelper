using PetHelperApi.Models;
using Quartz;
using Quartz.Impl;

namespace PetHelperApi.App_Start
{
    public class ScheduleConfig
    {
        private static IScheduler _scheduler;
        public static IScheduler Scheduler
        {
            get { return _scheduler ?? (_scheduler = StdSchedulerFactory.GetDefaultScheduler()); }
        }

        public static void RegisterSchedule()
        {
            RegisterNotifications();

            Scheduler.Start();
        }

        private static void RegisterNotifications()
        {
            var job = JobBuilder.Create<ServiceNotifier>().WithIdentity("ServicesJob").Build();
            var trigger =
                TriggerBuilder.Create()
                    .WithIdentity("VacationTrigger")
                    .StartNow()
                    .WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(8, 0))
                    .Build();

            Scheduler.ScheduleJob(job, trigger);
        }
    }
}