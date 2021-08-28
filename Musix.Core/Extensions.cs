using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Musix.Core
{
    public static class Extensions
    {
        /// <summary>
        /// This is only temp code, Musix will be re-made to much better code practices
        /// </summary>
        public static T GetSync<T>(this Task<T> task)
        {
            if (task.IsCompleted)
            {
                return task.Result;
            }
            task.Wait();
            return task.Result;
        }

        public static T GetSync<T>(this ValueTask<T> task)
        {
            if (task.IsCompleted)
            {
                return task.Result;
            }
            SpinWait.SpinUntil(() => task.IsCompleted);
            return task.Result;
        }
    }
}
