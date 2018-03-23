using NectimaLogging.Models;
using NectimaLogging.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NectimaLogging.Helpers
{
    public class SearchHelper
    {
        private IMyServices _myServices;
        private ILogEntryRepository _logEntryRepository;
        private LogEntry _returnOneLog;
        private IEnumerable<LogEntry> _returnSeveralLogs;

        private string _input;
        private int _parsedId;
        private string _error = "error";
        private string _filteredLogs = "filteredLogs";
        private string _singleSearch = "singleSearch";

        public SearchHelper(string input, IMyServices myServices, ILogEntryRepository logEntryRepository)
        {
            _myServices = myServices;
            _logEntryRepository = logEntryRepository;
            _input = input;
        }

        public string SeachHelper()
        {
            _parsedId = _myServices.ParseInputToInt(_input);

            if (_input == null)
            {
                return _error;
            }

            _input = _myServices.RemoveWhiteSpace(_input);
            if (_myServices.RemoveUnWantedChars(_input))
            {
                return _error;
            }
            if ((_parsedId <= int.MaxValue && _parsedId != 0 && !_logEntryRepository.IsBiggerThenMaxId(_parsedId)))
            {
                if (_myServices.IsNumber(_input))
                {
                    var getlogsById = _logEntryRepository.GetLogbyId(
                    int.Parse(_input));

                    if (getlogsById == null)
                    {
                        return _error;
                    }
                    _returnOneLog = (getlogsById);
                    return _singleSearch;
                }
            }

            if (_myServices.IsLetters(_input))
            {
                _input = _myServices.FirstCharToUpper(_input);

                _returnSeveralLogs = _logEntryRepository.GetLogByLevel(_input);
                return _filteredLogs;
                
            }
            if (_myServices.IsNumberAndLetters(_input))
            {
                return _error;
            }
            return _error;
        }

        public LogEntry ReturnLog()
        {
            return _returnOneLog;
        }

        public IEnumerable<LogEntry> ReturnSeveralLogs()
        {
            return _returnSeveralLogs;
        }
    }
}
