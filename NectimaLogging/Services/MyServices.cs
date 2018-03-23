using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NectimaLogging.Services
{
    public class MyServices : IMyServices
    {
        private int _number;
        private bool _result;
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
            _result = int.TryParse(num, out _number);
            if (_result)
            {   if (_number <= 0)
                    return 0;
                return _number;
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

       
    }



    public interface IMyServices
    {
        string FirstCharToUpper(string input);
        bool ContainsLetters(string input);      
        bool IsNumber(string input);
        bool IsLetters(string input);
        bool IsNumberAndLetters(string input);
        string RemoveWhiteSpace(string input);
        bool RemoveUnWantedChars(string input);
        int ParseInputToInt(string num);
    }
}
