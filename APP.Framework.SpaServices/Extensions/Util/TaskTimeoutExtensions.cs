using System;
using System.Threading.Tasks;

namespace APP.Framework.SpaServices.Extensions.Util
{
    internal static class TaskTimeoutExtensions
    {
        public static async Task WithTimeout(this Task task, TimeSpan timeoutDelay, string message)
        {
            if (task == await Task.WhenAny(task, Task.Delay(timeoutDelay)))
            {
                task.Wait();
            }
            else
            {
                throw new TimeoutException(message);
            }
        }

        public static async Task<T> WithTimeout<T>(this Task<T> task, TimeSpan timeoutDelay, string message)
        {
            if (task == await Task.WhenAny(task, Task.Delay(timeoutDelay)))
            {
                return task.Result;
            }
            else
            {
                throw new TimeoutException(message);
            }
        }
    }
}
