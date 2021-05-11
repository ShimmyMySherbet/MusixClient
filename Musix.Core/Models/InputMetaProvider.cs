using System;
using System.Collections.Generic;
using Musix.Core.Abstractions;

namespace Musix.Core.Models
{
    public class InputMetaProvider : IInputMetaProvider
    {
        private List<IInputMetaParser> m_Parsers = new List<IInputMetaParser>();

        public InputMetaProvider()
        {
            AddParser<BasicInputParser>();
        }

        public void AddParser<T>() where T : IInputMetaParser
        {
            lock (m_Parsers)
            {
                m_Parsers.Add(Activator.CreateInstance<T>());
                m_Parsers.OrderByWeight();
            }
        }

        public void AddParser(IInputMetaParser parser)
        {
            lock (m_Parsers)
            {
                m_Parsers.Add(parser);
                m_Parsers.OrderByWeight();
            }
        }

        public bool DerriveMeta(string input, DownloadContext context)
        {
            bool m = false;
            lock (m_Parsers)
            {
                foreach (var parser in m_Parsers)
                {
                    if (parser.DerriveMeta(input, context)) m = true;
                }
            }
            return m;
        }

        public void RemoveParser(IInputMetaParser parser)
        {
            lock (m_Parsers)
            {
                if (m_Parsers.Contains(parser))
                {
                    m_Parsers.Remove(parser);
                }
            }
        }

        public void RemoveParser<T>() where T : IInputMetaParser
        {
            lock (m_Parsers)
            {
                m_Parsers.RemoveAll(x => x.GetType() == typeof(T));
            }
        }
    }
}