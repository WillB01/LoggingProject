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

        public bool IsNumber(string input)
        {
            if(input.Any(Char.IsDigit) && !input.Any(Char.IsLetter) )
            {
                return true;
            }
            return false;
        }

        public bool IsLetters(string input)
        {
            if(Regex.IsMatch(input, @"^[a-zA-Z]+$"))
            {
                return true;
            }
            return false;
        }

        public bool IsNumberAndLetters(string input)
        {
            if(Regex.IsMatch(input, @"^[a-zA-Z0-9]+$"))
            {
                return true;
            }
            return false;
        }

        public string RemoveWhiteSpace(string input)
        {
            var result = Regex.Replace(input, @"\s+", "");
            return result;
        }
        public bool RemoveUnWantedChars(string input)
        {
            if(!Regex.IsMatch(input, @"^[\w'""&:;-]+$"))
            {
                return true;
            }
            return false;
           
        }

        public int ParseInputToInt(string num)
        {
            int number;
            bool result = int.TryParse(num, out number);
            if (result)
            {   if (number <= 0)
                    return 0;
                return number;
            }
            return 0;
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

        bool IsNumber(string input);
        bool IsLetters(string input);
        bool IsNumberAndLetters(string input);
        string RemoveWhiteSpace(string input);
        bool RemoveUnWantedChars(string input);
        int ParseInputToInt(string num);
    }
}
