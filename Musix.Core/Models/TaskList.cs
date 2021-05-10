using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Musix.Core.Models
{
    public class TaskList : IDisposable
    {
        private List<Task> m_Tasks = new List<Task>();
        public bool Locked { get; private set; }

        public int Count
        {
            get
            {
                lock (m_Tasks)
                {
                    return m_Tasks.Count;
                }
            }
        }

        public void Add(Task task)
        {
            if (Locked) return;
            lock (m_Tasks)
            {
                m_Tasks.Add(task);
            }
        }

        public void Remove(Task task)
        {
            if (Locked) return;
            lock (m_Tasks)
            {
                m_Tasks.Remove(task);
            }
        }

        public async Task WaitAll()
        {
            Locked = true;

            foreach (Task t in m_Tasks)
            {
                await t;
            }
        }

        public void CancelAll()
        {
            foreach (Task task in m_Tasks.ToList())
            {
                try
                {
                    task.Dispose();
                }
                catch (TaskCanceledException)
                {
                }
                finally
                {
                    m_Tasks.Remove(task);
                }
            }
        }

        public T GetResult<T>()
        {
            if (Locked) return default(T);

            lock (m_Tasks)
            {
                foreach (Task<T> task in m_Tasks.OfType<Task<T>>())
                {
                    if (!task.IsCanceled && task.Status == TaskStatus.RanToCompletion)
                    {
                        return task.Result;
                    }
                }
            }

            return default(T);
        }
        public List<T> GetResults<T>()
        {
            if (Locked) return new List<T>();
            var results = new List<T>();
            lock (m_Tasks)
            {
                foreach (Task<T> task in m_Tasks.OfType<Task<T>>())
                {
                    if (!task.IsCanceled && task.Status == TaskStatus.RanToCompletion)
                    {
                        results.Add(task.Result);
                    }
                }
            }
            return results;
        }

        public void Dispose()
        {
            lock (m_Tasks)
            {
                foreach (var t in m_Tasks)
                {
                    t.Dispose();
                }
            }
        }
    }
}