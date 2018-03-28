using NectimaLogging.Interface;
using NectimaLogging.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NectimaLogging.Services
{
    public class ChartService : IChartService
    {
        private ILogEntryRepository _logEntryRepository;

        public ChartService(ILogEntryRepository logEntryRepository)
        {
            _logEntryRepository = logEntryRepository;
        }

        public int AmountOfExceptions()
        {
            
            int divisor = _logEntryRepository.GetAllLogs.Count(p => p.Exception != "");
            
            return divisor;

            
        }
    }
}
