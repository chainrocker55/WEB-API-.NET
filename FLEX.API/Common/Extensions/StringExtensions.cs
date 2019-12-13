using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FLEX.API.Common.Extensions
{
    public static class StringExtensions
    {
        public static string NullIfEmpty(this string str)
        {
            if (String.IsNullOrWhiteSpace(str))
                return null;
            else
                return str;
        }
    }
}
