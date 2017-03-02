using PetHelperApi.Services;
using Quartz;

namespace PetHelperApi.Models
{
    public class ServiceNotifier : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            ScheduleService.Instance.CheckServices();
        }
    }
}