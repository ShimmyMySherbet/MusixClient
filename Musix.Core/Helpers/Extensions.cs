using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Musix.Core.Helpers
{
    public static class Extensions
    {
        public static string RemoveBaseSubStrings(this string Base, params string[] SubStrings)
        {
            string Result = Base;
            string LastRes = "";

            while (!(LastRes == Result))
            {
                LastRes = Result;
                foreach (string substr in SubStrings)
                {
                    Result = RemoveBaseSubString(Result, substr);
                }
            }
            return Result;
        }
        public static string RemoveBaseSubString(this string Base, string Content)
        {
            if (Base.ToLower().StartsWith(Content.ToLower()))
            {
                Base = Base.Remove(0, Content.Length);
            }
            return Base;
        }
    }
}
