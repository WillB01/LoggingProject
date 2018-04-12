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

        public static string KeepOnlyMonth(this string str)
        {
            string[] mySplitString = str.Split('-');

            return mySplitString[1];
        }
        public static string KeepOnlyDay(this string str)
        {
            string[] mySplitString = str.RemoveTime().Split('-');
            

            return mySplitString[2];
        }

    }
}