using FluentScheduler;
using SVideo.Scheduled.Http;
using System;

namespace SVideo.Scheduled.Job
{
    public class Job : IJob
    {
        
        private readonly int Days = 10;
        private readonly string Endpoint = "http://localhost:59260/api/Recycler{0}";

        public void Execute()
        {
            RecycleVideo();
            Console.WriteLine("São {0:HH:mm:ss} horas", DateTime.Now);
        }

        private void RecycleVideo()
        {
            if (Running() == Constants.NOT_RUNNING)
            {
                UpdateRunning();
                RecycleVideosWithDays(Days);
            }
        }

        private void RecycleVideosWithDays(int days)
        {
            Uri url = new Uri(string.Format(Endpoint, $"/process/{days}"));
            new HttpRequestRepository().Delete<string>(url, null, null);
            UpdateRunning();
        }

        private void UpdateRunning()
        {
            Uri url = new Uri(string.Format(Endpoint, ""));
            new HttpRequestRepository().Put<string>(url, null, null);
        }

        private string Running()
        {
            Uri url = new Uri(string.Format(Endpoint, "/status"));
            return new HttpRequestRepository().Get<string>(url, null, null);
        }
    }
}
