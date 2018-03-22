using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NectimaLogging.Services
{
    public static class KeywordHighlight
    {
        public static string HighlightKeywords(this string input, string keywords)
        {
            if (input == string.Empty || keywords == string.Empty)
            {
                return input;
            }

            string[] sKeywords = keywords.Split(' ');
            foreach (string sKeyword in sKeywords)
            {
                try
                {
                    input = Regex.Replace(input, sKeyword, string.Format("<span class=\"hit\">{0}</span>", "$0"), RegexOptions.IgnoreCase);
                }
                catch
                {
                    //
                }
            }
            return input;
        }
    }
}
