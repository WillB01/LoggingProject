using NectimaLogging.Controllers;
using NectimaLogging.Helpers;
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
        private IWeek _week; 
        private IEnumerable<LogEntry> _test;
        

        public ChartService(ILogEntryRepository logEntryRepository, IWeek week)
        {
            _logEntryRepository = logEntryRepository;
            _week = week;
        }

        public int AmountOfExceptions()
        {
           _test = _logEntryRepository.GetAllLogs.Where(x => x.Date != null && x.Exception != "");
            //_test = _logEntryRepository.GetLogByDate("2018-03-13");

            return _test.Count(p => p.Exception != "");          
        }

        public IEnumerable<LogEntry> ExcetionsPerDay()
        {
            
            _test = _logEntryRepository.GetAllLogs.Where(x => x.Date != null && x.Exception != "");
            return _test;  
        }

        //public IEnumerable<LogEntry> GetThem()
        //{
            
            
            
            
        //}

        
       

    }

    public class ExceptionAndDate
    {
        public int NumberOfExetions { get; set; }
        public string Date { get; set; }
    }
}
