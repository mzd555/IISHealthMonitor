using System;

namespace HealthMonitor
{
    class Program
    {
        static void Main(string[] args)
        {
            var monitor = new HealthMonitor();
            monitor.RunCheck();
        }
    }
}
