using System;
using System.Linq;

namespace NectimaLogging.Controllers
{
    public static class RemoveStuffFromDate
    {
        public static string RemoveTime(this DateTime str)
        {
            return str.ToString().Split(" ").First().Substring(0);
        }
        public static string RemoveTime(this string str)
        {
            return str.ToString().Split(" ").First().Substring(0);
        }

    }
}