using System;

namespace Musix.Core.Models.Debug
{
    public class StopWatch
    {
        public DateTime StartTime = DateTime.Now;
        public TimeSpan GetDuration()
        {
            return DateTime.Now.Subtract(StartTime); 
        }
        public void PrintDur(string MethodName)
        {
            TimeSpan span = GetDuration();
            Console.WriteLine($"Method {MethodName} took {span.TotalSeconds}s to run ({span.Milliseconds}ms).");
        }
    }
}
