using System;

namespace Musix.Core.Models
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
            Console.WriteLine($"Method {MethodName} took {GetDuration().TotalSeconds}s to run.");
        }
    }
}
