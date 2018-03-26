using NectimaLogging.Models;
using NectimaLogging.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NectimaLogging.Helpers
{
    public class AdvSearchHelper
    {
        private ILogEntryRepository _logEntryRepository;
        private IMyServices _myServices;
        private IEnumerable<LogEntry> _filteredLogs;
        private Level _levelInput;
        private string _id;
        private int _parsedId;
        private string _dateInput;
        private string _thread;
        private string _message;
        private string _searchBarInput;


        public AdvSearchHelper(ILogEntryRepository logEntryRepository, IMyServices myServices, Level levelInput, string id, string dateInput, string thread, string message, string searchBarInput)
        {
            _logEntryRepository = logEntryRepository;
            _myServices = myServices;
            _levelInput = levelInput;
            _id = id;
            _dateInput = dateInput;
            _thread = thread;
            _message = message;
            _searchBarInput = searchBarInput;
        }

        public IEnumerable<LogEntry> FilteredLogs()
        {
            return _filteredLogs;
        }

        public bool SearchFilter()
        {
           
                if (_levelInput == Level.Empty &&
                   string.IsNullOrWhiteSpace(_id) && string.IsNullOrWhiteSpace(_dateInput) &&
                   string.IsNullOrWhiteSpace(_thread) && string.IsNullOrWhiteSpace(_message) && string.IsNullOrWhiteSpace(_searchBarInput))
                {
                    _filteredLogs = _logEntryRepository.GetAllLogs;
                    return true;
                }

                if (string.IsNullOrWhiteSpace(_id) && string.IsNullOrWhiteSpace(_searchBarInput))
                {
                   
                    _filteredLogs = _logEntryRepository.AdvancedSearchFilter(_levelInput, _dateInput, _thread, _message);
                    return true;
                }
                if (string.IsNullOrWhiteSpace(_id) && !string.IsNullOrWhiteSpace(_searchBarInput))
                {
                    _id = _searchBarInput;
                }

                
                _parsedId = _myServices.ParseInputToInt(_id);

                if (_logEntryRepository.IsBiggerThenMaxId(_parsedId) ||
                   _parsedId == 0 && _id != null && _dateInput == null || _logEntryRepository.GetLogbyId(_parsedId) == null)
                {
                    if (_myServices.RemoveUnWantedChars(_searchBarInput))
                    {
                        return false;
                    }
                    if (_myServices.IsLetters(_searchBarInput))
                    {
                        _searchBarInput = _myServices.FirstCharToUpper(_searchBarInput);

                        _filteredLogs = _logEntryRepository.GetLogByLevel(_searchBarInput);
                        return true;

                    }
                    if (_myServices.IsNumberAndLetters(_searchBarInput))
                    {
                        return false;
                    }
                    return false;
                }
                if (_logEntryRepository.IsIdOkey(_parsedId) && !string.IsNullOrWhiteSpace(_id))
                {

                    if (_myServices.IsNumber(_id))
                    {

                        _filteredLogs = _logEntryRepository.GetLogbyId(_parsedId);
                        return true;
                    }

                    else
                    {
                        _filteredLogs = _logEntryRepository.GetLogByDate(_dateInput)
                            .Where(x => x.Id == _parsedId);
                        return true;
                    }
                }         
            return false;
        }


    }

}
