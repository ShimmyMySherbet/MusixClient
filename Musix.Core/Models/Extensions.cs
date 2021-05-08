using System.Collections.Generic;
using System.Linq;
using Musix.Core.Attributes;

namespace Musix.Core.Models
{
    public static class Extensions
    {
        public static void OrderByWeight<T>(IEnumerable<T> objs)
        {
            objs = objs.OrderByDescending(x => Weight.GetWeight(x.GetType()));
        }
    }
}