using Quartz;
using TestRest.Services;

namespace TestRest.Models
{
    public class ServiceNotifier : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            ScheduleService.Instance.CheckServices();
        }
    }
}