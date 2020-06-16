using System;
using System.Collections.Generic;

namespace Musix.Core.Models
{
    public static class Extensions
    {
        public static IEnumerable<R> CastEnumerable<T, R>(this IEnumerable<T> Input, Func<T, R> CastingMethod)
        {
            List<R> ReturnResult = new List<R>();
            foreach (T Ent in Input)
            {
                ReturnResult.Add(CastingMethod(Ent));
            }
            return ReturnResult;
        }
    }
}