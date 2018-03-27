using NectimaLogging.Models;
using NectimaLogging.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NectimaLogging.Helpers
{
    public class SearchFilterLogic
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


        public SearchFilterLogic(ILogEntryRepository logEntryRepository, IMyServices myServices, Level levelInput, string id, string dateInput, string thread, string message, string searchBarInput)
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

            _filteredLogs = _levelInput == Level.Empty &&
               string.IsNullOrWhiteSpace(_id) && string.IsNullOrWhiteSpace(_dateInput) &&
               string.IsNullOrWhiteSpace(_thread) && string.IsNullOrWhiteSpace(_message) &&
               string.IsNullOrWhiteSpace(_searchBarInput) && string.IsNullOrWhiteSpace(_id) ? _logEntryRepository.GetAllLogs : null;

            _id = !string.IsNullOrWhiteSpace(_searchBarInput) ? _searchBarInput : _id;

            if (_filteredLogs == null)
                _filteredLogs = string.IsNullOrWhiteSpace(_id) ? _logEntryRepository.AdvancedSearchFilter(_levelInput, _dateInput, _thread, _message) : null;

            _parsedId = _myServices.ParseInputToInt(_id);
          
            if(_filteredLogs == null)
                _filteredLogs = _logEntryRepository.IsBiggerThenMaxId(_parsedId) || _parsedId == 0 ||
                _logEntryRepository.GetLogbyId(_parsedId) == null ? _filteredLogs : _filteredLogs = _logEntryRepository.GetLogbyId(_parsedId);

            if (_filteredLogs == null)
                _filteredLogs = _myServices.IsNumber(_searchBarInput) || !_myServices.IsLetters(_searchBarInput) 
                    ? null : _logEntryRepository.GetLogByLevel(_myServices.FirstCharToUpper(_searchBarInput));

            if (_filteredLogs == null)
                return false;

            return true;
        }


    }

}
