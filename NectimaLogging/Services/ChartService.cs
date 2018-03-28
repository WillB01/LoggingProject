using NectimaLogging.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NectimaLogging.Services
{
    public class ChartService
    {
        private ILogEntryRepository _logEntryRepository;

        public ChartService(ILogEntryRepository logEntryRepository)
        {
            _logEntryRepository = logEntryRepository;
        }


    }
}
