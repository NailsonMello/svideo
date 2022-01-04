using FluentScheduler;
using SVideo.Scheduled.Http;

namespace SVideo.Scheduled.Job
{
    public class RegisteringScheduled : Registry
    {
        public RegisteringScheduled()
        {
            // It will run every day at 6:10 am
            Schedule<Job>().ToRunEvery(1).Days().At(6, 10);
        }
    }
}
