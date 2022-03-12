using System;
using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace MonitoringAgent.Scheduler
{
    public static class MonitoringAgent_Scheduler
    {
        [Function("MonitoringAgent_Scheduler")]
        public static void Run([TimerTrigger("0 17,22 * * * 1-5")] MyInfo myTimer, FunctionContext context)
        {
            var logger = context.GetLogger("MonitoringAgent_Scheduler");
            logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            logger.LogInformation($"Next timer schedule at: {myTimer.ScheduleStatus.Next}");

            var client = new WebClient();
            var content = client.DownloadString(GetEnvironmentVariable("MONITORING_AGENT_URL"));
        }

        /// <summary>
        /// Returns an environment variable from Azure
        /// </summary>
        /// <param name="name">The name or key of the environment variable to grab.</param>
        /// <returns>The value of the environment variable.</returns>
        private static string GetEnvironmentVariable(string name)
        {
            return System.Environment.GetEnvironmentVariable(name, EnvironmentVariableTarget.Process);
        }
    }

    public class MyInfo
    {
        public MyScheduleStatus ScheduleStatus { get; set; }

        public bool IsPastDue { get; set; }
    }

    public class MyScheduleStatus
    {
        public DateTime Last { get; set; }

        public DateTime Next { get; set; }

        public DateTime LastUpdated { get; set; }
    }
}
