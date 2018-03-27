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

            if (_levelInput == Level.Empty &&
               string.IsNullOrWhiteSpace(_id) && string.IsNullOrWhiteSpace(_dateInput) &&
               string.IsNullOrWhiteSpace(_thread) && string.IsNullOrWhiteSpace(_message) &&
               string.IsNullOrWhiteSpace(_searchBarInput) && string.IsNullOrWhiteSpace(_id))
            {
                _filteredLogs = _logEntryRepository.GetAllLogs;
                return true;
            }

            _id = !string.IsNullOrWhiteSpace(_searchBarInput) ? _searchBarInput : _id;

            _filteredLogs = string.IsNullOrWhiteSpace(_id) ? _logEntryRepository.AdvancedSearchFilter(_levelInput, _dateInput, _thread, _message) : null;

            _parsedId = _myServices.ParseInputToInt(_id);


            if (_filteredLogs == null)
            {
                var test =
                _logEntryRepository.IsBiggerThenMaxId(_parsedId) ||
                _parsedId == 0 && _id != null && _dateInput == null ||
                _logEntryRepository.GetLogbyId(_parsedId) == null ? false : true;

                if (test)
                {
                    _filteredLogs = _logEntryRepository.GetLogbyId(_parsedId);
                    return true;
                }
                    

                test =              
                    _myServices.IsNumber(_searchBarInput)
                    ? false : _myServices.IsLetters(_searchBarInput);

                if (test)
                    _filteredLogs = _logEntryRepository.GetLogByLevel(_myServices.FirstCharToUpper(_searchBarInput));
                else
                {
                    return false;
                }
            }



            //var test = _logEntryRepository.IsBiggerThenMaxId(_parsedId) || _parsedId == 0 && _id != null && _dateInput == null || _logEntryRepository.GetLogbyId(_parsedId) == null
            //        && _filteredLogs != null ? true : _myServices.RemoveUnWantedChars(_searchBarInput) || _myServices.IsNumberAndLetters(_searchBarInput) || _myServices.IsLetters(_myServices.FirstCharToUpper(_searchBarInput));



            //test = _myServices.RemoveUnWantedChars(_searchBarInput) || _myServices.IsNumberAndLetters(_searchBarInput) ? false : _myServices.IsLetters(_myServices.FirstCharToUpper(_searchBarInput));





            //if (test)
            //{
            //    if (_myServices.RemoveUnWantedChars(_searchBarInput))
            //    {
            //        return false;
            //    }



            //    if (_myServices.IsLetters(_searchBarInput))
            //    {
            //        _searchBarInput = _myServices.FirstCharToUpper(_searchBarInput);

            //        _filteredLogs = _logEntryRepository.GetLogByLevel(_searchBarInput);
            //        return true;

            //    }
            //    if (_myServices.IsNumberAndLetters(_searchBarInput))
            //    {
            //        return false;
            //    }
            //    return true;
            //}



            //if (_logEntryRepository.IsBiggerThenMaxId(_parsedId) ||
            //   _parsedId == 0 && _id != null && _dateInput == null || _logEntryRepository.GetLogbyId(_parsedId) == null)
            //{
            //        if (_myServices.RemoveUnWantedChars(_searchBarInput))
            //        {
            //            return false;
            //        }






            //        if (_myServices.IsLetters(_searchBarInput))
            //            {
            //                _searchBarInput = _myServices.FirstCharToUpper(_searchBarInput);

            //                _filteredLogs = _logEntryRepository.GetLogByLevel(_searchBarInput);
            //                return true;

            //            }
            //            if (_myServices.IsNumberAndLetters(_searchBarInput))
            //            {
            //                return false;
            //            }
            //            return false;
            //}




            //if (_logEntryRepository.IsIdOkey(_parsedId) && !string.IsNullOrWhiteSpace(_id))
            //    {

            //        if (_myServices.IsNumber(_id))
            //        {

            //            _filteredLogs = _logEntryRepository.GetLogbyId(_parsedId);
            //            return true;
            //        }

            //        else
            //        {
            //            _filteredLogs = _logEntryRepository.GetLogByDate(_dateInput)
            //                .Where(x => x.Id == _parsedId);
            //            return true;
            //        }
            //    }         
            return true;
        }


    }

}
