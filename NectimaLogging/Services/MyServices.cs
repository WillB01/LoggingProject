using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NectimaLogging.Services
{
    public class MyServices : IMyServices
    {
        public string FirstCharToUpper(string input)
        {
            if (String.IsNullOrEmpty(input))
                throw new ArgumentException("ARGH!");
            return input.First().ToString().ToUpper() + input.Substring(1);
        }

        public bool ContainsLetters(string input)
        {
            if (input.Any(Char.IsLetter) && input.Any(Char.IsDigit) || input.Any(Char.IsDigit))
            {
                return false;
            }
            return true;
        }
    }

    public interface IMyServices
    {
        string FirstCharToUpper(string input);
        bool ContainsLetters(string input);
    }
}
