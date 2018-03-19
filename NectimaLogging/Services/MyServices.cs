using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NectimaLogging.Services
{
    public class MyServices : IMyServices
    {

        public string SearchInput { get; set; }

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

        public string AddWhiteSpace(string input)
        {
            var correctedValue = Regex.Replace(
            input,
                 "(?<=[0-9])(?=[A-Za-z])|(?<=[A-Za-z])(?=[0-9])",
                    " ");
            return correctedValue;
        }

        public string CheckIfLastCharIsDigit(string input)
        {
            if(!input.Any(Char.IsLetter))
            {
                return input;
            }
            var digits = new[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            
            var result = input.TrimEnd(digits);
            result.Trim(digits);
            return result;
        }
    }



    public interface IMyServices
    {
        string FirstCharToUpper(string input);
        bool ContainsLetters(string input);
        string SearchInput { get; set; }
        string AddWhiteSpace(string input);
        string CheckIfLastCharIsDigit(string input);
    }
}
