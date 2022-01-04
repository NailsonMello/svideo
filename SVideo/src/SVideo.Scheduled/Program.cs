using FluentScheduler;
using SVideo.Scheduled.Job;
using System;

namespace SVideo.Scheduled
{
    class Program
    {
        static void Main(string[] args)
        {
            StartScheduler();
        }

        public static void StartScheduler()
        {
            JobManager.Initialize(new RegisteringScheduled());
            Console.ReadLine();
            JobManager.StopAndBlock();
        }
    }
}
