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
        private LogEntry _logEntry;

        private Level _level;
        private Level _levelInput;
        private string _id;
        private int _idParsed;
        private string _dateInput;
        private string _thread;
        private string _message;


        public AdvSearchHelper(ILogEntryRepository logEntryRepository)
        {
            _logEntryRepository = logEntryRepository;
        }

        public void SearchFilter(string prefix)
        {

        }



    }
}