using System.Collections.Generic;
using Musix.Core.Abstractions;

namespace Musix.Core.Models
{
    public class MetaIndex : IMetaIndex
    {
        private Dictionary<string, object> m_Values = new Dictionary<string, object>();

        public void Add<T>(string key, T Instance)
        {
            lock (m_Values)
            {
                m_Values.Add(key, Instance);
            }
        }

        public bool ContainsKey(string key)
        {
            lock (m_Values)
            {
                return m_Values.ContainsKey(key);
            }
        }

        public void Delete(string key)
        {
            if (ContainsKey(key))
            {
                lock (m_Values)
                {
                    m_Values.Remove(key);
                }
            }
        }

        public T Get<T>(string key)
        {
            if (ContainsKey(key))
            {
                lock (m_Values)
                {
                    if (m_Values[key] is T instance)
                    {
                        return instance;
                    }
                }
            }
            return default(T);
        }

        public string GetString(string key)
        {
            if (ContainsKey(key))
            {
                lock (m_Values)
                {
                    return m_Values[key].ToString();
                }
            }
            return null;
        }
    }
}