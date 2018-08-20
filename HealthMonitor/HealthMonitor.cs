using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace HealthMonitor
{
    public class HealthMonitor
    {
        public void RunCheck()
        {
            var logs = LogProvider.GetLogs();
            var logsIn5Minutes = logs.Where(l => l.dateTime > DateTime.UtcNow.AddMinutes(-5));
            var errors = logsIn5Minutes.Where(l => int.Parse(l.sc_status) >= 400).ToList();
            foreach (var e in errors)
                e.dateTime = e.dateTime.ToLocalTime();

            var body = errors.ToList().ToHtmlTable(new List<string> {
                "dateTime" ,
                "c_ip","sc_status","s_port","time_taken","cs_host",
                "cs_method","cs_uri_stem","cs_uri_query"});

            if (!string.IsNullOrEmpty(body))
                EmailSender.SendEmail(body, "Health Monitoring Report " + System.Environment.MachineName);
        }
    }


}
